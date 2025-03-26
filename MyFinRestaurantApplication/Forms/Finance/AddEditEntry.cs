using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Collections.Generic;
using ManagerApplication.Properties;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class AddEditEntry : Form
    {
        private List<Stock> stockList = new List<Stock>();
        private List<Stock> entryStocks = new List<Stock>();
        private Entry myEntry;
        private double fullPrice;

        public AddEditEntry(Entry entry = null)
        {
            InitializeComponent();

            InitForm(entry);
        }

        private async void InitForm(Entry entry)
        {
            await LoadVendors();
            if (entry != null)
            {
                myEntry = entry;
                fullPrice = myEntry.entry_summa;
                txtPay.Text = myEntry.entry_paid.ToString("F2");
                txtDebt.Text = myEntry.GetEntryDebt.ToString("F2");
                txtSumma.Text = myEntry.entry_summa.ToString("F2");
                txtComment.Text = myEntry.entry_comment;

                SetInComboBox(myEntry.entry_vendor);

                var details = await new EntryDetails().OnSelectDetailsByEntryAsync(myEntry.entry_id);
                var stocks = await new Stock().OnLoadAsync();
                var types = await new Model.Type().OnLoadAsync();

                Stock stock;
                Stock detailStock;
                double count;
                double price;

                foreach (EntryDetails i in details)
                {
                    stock = stocks.Where(u => u.stock_id == i.details_stock).FirstOrDefault();
                    stock.type = types.Where(u => u.type_id == i.details_value).FirstOrDefault();

                    count = i.details_count;
                    price = i.details_price;

                    detailStock = new Stock()
                    {
                        stock_id = stock.stock_id,
                        stock_name = stock.stock_name,
                        stock_value = stock.stock_value,
                        stock_count = count,
                        stock_price = price,
                        type = stock.type
                    };
                    stockList.Add(detailStock);
                    entryStocks.Add(detailStock);
                }

                UpdateStocksInGrid(stockList);

            }
            else
            {
                txtPay.Text = "0";
            }
        }

        private void SetInComboBox(int vendorId)
        {
            cmbVendor.SelectedValue = vendorId;
        }

        private async Task LoadVendors()
        {
            //cmbVendor.SelectedIndexChanged -= CmbVendor_SelectedIndexChanged;
            var vendors = new List<Vendor>
            {
                new Vendor { vendor_id = -1, vendor_name = "#Неизвестный поставщик" },
                new Vendor { vendor_name = "#Добавить нового поставщика" }
            };
            vendors.AddRange(await new Vendor().OnLoadAsync());

            cmbVendor.DataSource = vendors;
            cmbVendor.DisplayMember = "vendor_name";
            cmbVendor.ValueMember = "vendor_id";
            //  cmbVendor.SelectedIndexChanged += CmbVendor_SelectedIndexChanged;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var window = new PageAddStock(true))
            {
                window.Owner = this;
                window.StockAdded += OnStockAdded;
                window.ShowDialog();
            }
        }

        private void OnStockAdded(object sender, StockEventArgs e)
        {
            AddOrUpdateStock(stockList, e.NewStock);
        }

        public void AddOrUpdateStock(List<Stock> stocks, Stock newStock)
        {
            var existingStock = stocks.FirstOrDefault(s => s.stock_id == newStock.stock_id);
            if (existingStock != null)
            {
                // If stock exists, increment the count.
                existingStock.stock_count += newStock.stock_count;
            }
            else
            {
                // If stock doesn't exist, add it to the list.
                stocks.Add(newStock);
            }

            // Now call SetInDgvDetails to update DataGridView.
            UpdateStocksInGrid(stocks);
            UpdatePrice();
        }

        private void UpdateStocksInGrid(List<Stock> data)
        {
            dgvDetails.DataSource = null;
            dgvDetails.DataSource = data;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dgvDetails.Rows.Count == 0)
            {
                Dialog.Error("Вы не добавили товаров!");
                return;
            }

            SaveEntry();
        }

        // Сохранение записи о приходе
        private async void SaveEntry()
        {
            if (!double.TryParse(txtPay.Text, out double paid))
            {
                Dialog.Error("Некорректная сумма оплаты!");
                return;
            }

            var vendorId = Convert.ToInt32(cmbVendor.SelectedValue);
            var comment = txtComment.Text.Trim();

            if (myEntry == null)
            {
                var myVendor = await new Vendor().OnSelectAsync(vendorId);
                if (fullPrice > paid)
                {
                    myVendor.vendor_debt += fullPrice - paid;
                    await new Vendor().OnUpdateAsync(myVendor);
                }

                var entry = new Entry
                {
                    entry_vendor = vendorId,
                    entry_comment = comment,
                    entry_date = DateTime.Now.ToShortDateString(),
                    entry_summa = fullPrice,
                    entry_paid = paid,
                    entry_user = Settings.Default.user_id,
                };

                if (await new Entry().OnInsertAsync(entry, cmbVendor.Text))
                {
                    SaveDetails();
                    await UpdateStockQuantities(stockList);

                    if (paid > 0)
                    {
                        var lastEntry = await new Entry().OnSelectLastAsync();
                        await BalanceSystem.Instance.AddEntryOperation(paid, lastEntry.entry_id);
                    }
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                myEntry.entry_vendor = vendorId;
                myEntry.entry_summa = fullPrice;
                myEntry.entry_paid = paid;
                myEntry.entry_comment = txtComment.Text;

                if (await new Entry().OnUpdateAsync(myEntry))
                {
                    if (await new EntryDetails().OnDeleteDetailsByEntryAsync(myEntry.entry_id))
                    {
                        SaveDetails();
                        await UpdateStockQuantities(entryStocks, true);
                        await UpdateStockQuantities(stockList);

                        await BalanceSystem.Instance.DeleteEntryOperations(myEntry.entry_id);
                        await BalanceSystem.Instance.AddEntryOperation(paid, myEntry.entry_id);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private async void SaveDetails()
        {
            EntryDetails details;

            foreach (var stock in stockList)
            {
                var myEntry = await new Entry().OnSelectLastAsync();
                details = new EntryDetails()
                {
                    details_stock = stock.stock_id,
                    details_count = stock.stock_count,
                    details_price = stock.stock_price,
                    details_value = stock.stock_value,
                    details_entry = myEntry.entry_id,
                };
                await new EntryDetails().OnInsertAsync(details);
            }
        }

        private async void UpdateProductPrice(Stock newStock, double stockLastPrice)
        {
            var prods = await new Product().OnLoadAsync();
            Product dbProd = new Product();
            var myStockUsage = await new Recipe().OnSelectStockInRecipeAsync(newStock.stock_id);

            foreach (var recipe in myStockUsage)
            {
                var prod = prods.FirstOrDefault(p => p.prod_id == recipe.recipe_product);
                if (prod != null)
                {
                    var myStockInProd = await new Recipe().OnSelectAsync(newStock.stock_id, prod.prod_id);
                    if (myStockInProd != null)
                    {
                        var count = myStockInProd.recipe_count;
                        var priceDif = Math.Round((stockLastPrice * count) - (newStock.stock_price * count), 2);
                        prod.prod_start_price -= priceDif;
                        await dbProd.OnUpdateAsync(prod);
                    }
                }
            }
        }

        // Обновление списка stocks в базе данных
        private async Task UpdateStockQuantities(List<Stock> stockChanges, bool subtract = false)
        {
            var allStocks = await new Stock().OnLoadAsync();
            var stockUpdater = new Stock(); // Экземпляр для обновления акций в базе данных

            foreach (var currentStock in allStocks)
            {
                var matchingStock = stockChanges.FirstOrDefault(s => s.stock_id == currentStock.stock_id);

                if (matchingStock != null)
                {
                    var dbPrice = currentStock.stock_price;
                    // Обновляем количество акций в зависимости от флага 'subtract'
                    currentStock.stock_price = Math.Round(((matchingStock.stock_price * matchingStock.stock_count)
                        + (currentStock.stock_price * currentStock.stock_count)) / (matchingStock.stock_count + currentStock.stock_count), 2);
                    currentStock.stock_count += subtract ? -matchingStock.stock_count : matchingStock.stock_count;
                    //MessageBox.Show(currentStock.stock_price.ToString());
                    await stockUpdater.OnUpdateAsync(currentStock);

                    if (!subtract)
                    {
                        //   if (currentStock.stock_price != matchingStock.stock_price) //если цена изменился
                        //  {
                        UpdateProductPrice(currentStock, dbPrice);
                        //   }
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetails.CurrentCell != null)
            {
                int index = dgvDetails.CurrentCell.RowIndex;
                stockList.RemoveAt(index);
                UpdateStocksInGrid(stockList);
                UpdatePrice();
            }
            else
            {
                Dialog.Error("Вы не выбрали ингридиенты!");
            }
        }

        private void UpdatePrice()
        {
            fullPrice = 0.0; // Сброс полной цены перед пересчетом

            foreach (var stock in stockList)
            {
                fullPrice += stock.Sum; // Обновление полной цены в цикле
            }

            txtSumma.Text = fullPrice.ToString("F2"); // Обновление текста с полной ценой
            SetFullPayPrice();
            UpdateDebtPrice(); // Обновление долга
        }

        private void txtPay_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(txtPay.Text.Trim(), out double paid) || paid > fullPrice)
            {
                SetFullPayPrice(); // Установка текста оплаты равной полной цене, если введено некорректное значение
            }
            else
            {
                UpdateDebtPrice(); // Обновление долга только если значение корректно
            }
        }


        private void UpdateDebtPrice()
        {
            if (double.TryParse(txtPay.Text, out double paid))
            {
                txtDebt.Text = (fullPrice - paid).ToString("F2"); // Обновление текста долга
            }
        }

        private async void CmbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendor.SelectedIndex == 1) // если выбран пункт Доб
            {
                var window = new AddEditVendor();
                if (window.ShowDialog() == DialogResult.OK)
                {
                    await LoadVendors();
                    var lastVendor = await new Vendor().OnSelectLastAsync();
                    SetInComboBox(lastVendor.vendor_id);
                    Dialog.Info("Новый поставщик успешно добавлен");
                }
                else
                {
                    cmbVendor.SelectedIndex = 0;
                }
            }
            txtPay.ReadOnly = cmbVendor.SelectedIndex == 0;
        }

        private void SetFullPayPrice()
        {
            txtPay.Text = fullPrice.ToString("N2");
            txtPay.Focus();
            txtPay.SelectAll();
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CmbTypeVolume_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cmbSender = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter && !cmbSender.DroppedDown)
            {
                cmbSender.DroppedDown = true;
                e.Handled = true;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
            }

            if (keyData == Keys.Enter)
            {
                btnSave.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                btnDelete.PerformClick();
                return true;
            }

            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                btnAdd.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

