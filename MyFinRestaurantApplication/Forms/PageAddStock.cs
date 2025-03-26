using System;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class PageAddStock : Form
    {
        public delegate void StockAddedEventHandler(object sender, StockEventArgs e);
        public event StockAddedEventHandler StockAdded;
        private Stock selectedStock;
        private bool allStocks = true;

        public PageAddStock(bool editPrice = false)
        {
            InitializeComponent();
            if (!editPrice)
            {
                txtPrice.ReadOnly = true;
                txtTotal.ReadOnly = true;
                allStocks = false;
            }
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            _ = UpdateStocks();
        }

        private async Task UpdateStocks()
        {
            BindingSource bs = new BindingSource();
            List<Stock> stocks = await new Stock().OnLoadAsync();
            if (!allStocks) 
                stocks = stocks.Where(st => st.stock_category != 2).ToList();
            
            var volumes = await new Model.Type().OnLoadAsync();
            var category = await new StockCategory().OnLoadAsync();

            if (txtFind.Text != "")
                stocks = stocks.Where(u => u.stock_name.ToUpper().Contains(txtFind.Text.ToString().ToUpper())).ToList();

            foreach (var i in stocks)
            {
                i.type = volumes.Where(u => u.type_id == i.stock_value).FirstOrDefault();
                i.stockCategory = category.Where(u => u.st_cat_id == i.stock_category).FirstOrDefault();
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var aaa = (Stock)dgvMain.SelectedRows[0].DataBoundItem;
                var volume = Convert.ToDouble(txtCount.Text);
                var price = Convert.ToDouble(txtPrice.Text);

                var stock = new Stock()
                {
                    stock_id = aaa.stock_id,
                    stock_name = aaa.stock_name,
                    stock_value = aaa.stock_value,
                    stock_count = volume,
                    stock_price = price,
                    type = aaa.type
                };
                StockAdded?.Invoke(this, new StockEventArgs { NewStock = stock });
                ResetFields();
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.Message.ToString());
            }
        }

        private void ResetFields()
        {
            txtFind.Text = string.Empty;
            txtFind.Focus();
            txtFind.Select();
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            var window = new AddEditStock();
            if (window.ShowDialog() == DialogResult.OK)
            {
                txtFind.Text = string.Empty;
                txtCount.Focus();
                txtCount.Select();

                await UpdateStocks();
                SelectLastRow();
            }
        }

        private void SelectLastRow()
        {
            if (dgvMain.Rows.Count > 0)
            {
                int lastIndex = dgvMain.Rows.Count - 1;
                dgvMain.Rows[lastIndex].Selected = true;
                dgvMain.FirstDisplayedScrollingRowIndex = lastIndex;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtCount_TextChanged(object sender, EventArgs e)
        {
            txtTotal.TextChanged -= txtTotal_TextChanged;
            CalculateTotalSumma();
            txtTotal.TextChanged += txtTotal_TextChanged;
        }

        private void CalculateTotalSumma()
        {
            bool priceParsed = double.TryParse(txtPrice.Text, out double price);
            bool countParsed = double.TryParse(txtCount.Text, out double count);

            if (!countParsed)
                count = 1;

            if (priceParsed && countParsed)
                txtTotal.Text = (price * count).ToString("N2");
            else
                txtTotal.Text = "Ошибка ввода";
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtCount.TextChanged -= TxtCount_TextChanged;
            CalculateCount();
            txtCount.TextChanged += TxtCount_TextChanged;
        }

        private void CalculateCount()
        {
            bool priceParsed = double.TryParse(txtPrice.Text, out double price);
            bool totalParsed = double.TryParse(txtTotal.Text, out double total);

            if (priceParsed && totalParsed)
                txtCount.Text = (total / price).ToString("N2");
            else
                txtCount.Text = "Ошибка ввода";
        }

        private async void TxtFind_TextChanged(object sender, EventArgs e)
        {
            await UpdateStocks();
        }

        private void DgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count > 0)
            {
                var selectedRow = dgvMain.SelectedRows[0];
                if (selectedRow.DataBoundItem is Stock aaa)
                {
                    selectedStock = aaa;
                    txtPrice.Text = selectedStock.stock_price.ToString();
                    txtCount.Text = "1";
                }
            }
        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Перемещаем логику пользовательского переключения вкладок сюда
                Control nextControl = GetNextControl(ActiveControl);
                if (nextControl != null)
                {
                    nextControl.Focus();
                    e.IsInputKey = true; // Указываем, что клавиша Tab является вводимой клавишей
                }
            }
        }

        // Этот метод определяет следующий элемент управления для фокуса
        private Control GetNextControl(Control currentControl)
        {
            // Здесь ваша логика определения следующего элемента управления
            // Например:
            switch (currentControl.Name)
            {
                case "txtFind":
                    return txtCount;
                case "txtCount":
                    return txtPrice;
                case "txtPrice":
                    return dgvMain;
                case "dgvMain":
                    return txtFind; // Loop back to the first control
                default:
                    return txtFind; // Loop back to the first control
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
            // Проверяем, что фокус находится на DataGridView
            if (dgvMain.ContainsFocus)
            {
                // Получаем индекс текущей выбранной строки
                int rowIndex = dgvMain.CurrentCell.RowIndex;

                // Перемещение вниз
                if (keyData == Keys.Down)
                {
                    // Проверяем, что это не последняя строка
                    if (rowIndex < dgvMain.Rows.Count - 1)
                    {
                        dgvMain.CurrentCell = dgvMain[0, rowIndex + 1];
                        return true; // Возвращаем true, чтобы указать, что клавиша обработана
                    }
                }
                // Перемещение вверх
                else if (keyData == Keys.Up)
                {
                    // Проверяем, что это не первая строка
                    if (rowIndex > 0)
                    {
                        dgvMain.CurrentCell = dgvMain[0, rowIndex - 1];
                        return true; // Возвращаем true, чтобы указать, что клавиша обработана
                    }
                }
            }

            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                btnSave.PerformClick();
                return true;
            }

            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                btnNew.PerformClick();
                return true;
            }

            // Вызываем базовую реализацию метода
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
