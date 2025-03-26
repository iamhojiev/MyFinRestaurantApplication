using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Collections.Generic;
using ManagerApplication.DataBase;

namespace ManagerApplication
{
    public partial class AddEditProduct : Form
    {
        private readonly List<Stock> stockList = new List<Stock>();
        private Product _product;

        public AddEditProduct(Product product = null)
        {
            InitializeComponent();

            InitForm(product);
        }

        private async void InitForm(Product product)
        {
            var types = await new StockCategory().OnLoadAsync();
            cmbType.DisplayMember = "st_cat_name";
            cmbType.ValueMember = "st_cat_id";
            cmbType.DataSource = types;
            cmbType.SelectedIndex = 0;

            var volumes = await new Model.Type().OnLoadAsync();
            cmbTypeVolume.DataSource = volumes;
            cmbTypeVolume.DisplayMember = "type_name";
            cmbTypeVolume.ValueMember = "type_id";
            cmbTypeVolume.SelectedIndex = 0;

            var kitchen = await new Kitchen().OnLoadAsync();
            cmbKitchen.DataSource = kitchen;
            cmbKitchen.DisplayMember = "kitchen_name";
            cmbKitchen.ValueMember = "kitchen_id";
            cmbKitchen.SelectedIndex = 0;

            txtCookingMinutes.Text = "0";
            bool printerCheck = await new CassaSettings().IsPrinterCookingOutputAsync();
            txtCookingMinutes.ReadOnly = !printerCheck;
            if (product != null)
            {
                _product = product;

                cmbType.SelectedValue = product.prod_type;
                cmbKitchen.SelectedValue = product.prod_kitchen;
                cmbTypeVolume.SelectedValue = product.prod_value;
              //  InitCategoryComboBox(product.prod_type);

                txtName.Text = product.prod_name;
                txtSalePrice.Text = product.prod_price.ToString();
                txtCookingMinutes.Text = product.prod_cooking_minutes.ToString();
                txtTitle.Text = "Изменение данных";

                var imageName = product.prod_name + product.prod_id + ".png";
                var image = ImageServerIO.GetProductImage(imageName);
                if (image != null)
                    pictureBox1.Image = image;

                var recipes = await new Recipe().OnSelectProdRecipeAsync(product.prod_id);
                var stocks = await new Stock().OnLoadAsync();

                Stock stock;
                Stock recipe;
                double count;

                foreach (Recipe i in recipes)
                {
                    stock = stocks.Where(u => u.stock_id == i.recipe_stock).FirstOrDefault();
                    stock.type = volumes.Where(u => u.type_id == i.recipe_value).FirstOrDefault();

                    count = i.recipe_count;

                    recipe = new Stock()
                    {
                        stock_id = stock.stock_id,
                        stock_name = stock.stock_name,
                        stock_value = stock.stock_value,
                        stock_count = count,
                        stock_price = stock.stock_price,
                        type = stock.type
                    };
                    stockList.Add(recipe);
                }
                txtName.Focus();
                txtName.Select();
                txtName.SelectAll();

                UpdateStocksInGrid(stockList);
            }
        }

        private async void InitCategoryComboBox(int typeValue)
        {
            var categories = await new Category().OnLoadAsync(typeValue);
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";

            if(_product != null)
            {
                cmbCategory.SelectedValue = _product.prod_category;
            }
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
        }

        private void UpdateStocksInGrid(List<Stock> data)
        {
            dgvRecipe.DataSource = null;
            dgvRecipe.DataSource = data;
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            var price = 0.0;

            foreach (var stock in stockList)
            {
                price += stock.Sum;
            }
            txtProdPrice.Text = price.ToString();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var window = new PageAddStock())
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

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Dialog.Error("Вы не указали название!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSalePrice.Text))
            {
                Dialog.Error("Вы не указали цену продажи!");
                return;
            }

            if (cmbType.SelectedItem == null)
            {
                Dialog.Error("Вы не выбрали тип!");
                return;
            }

            if (cmbTypeVolume.SelectedItem == null)
            {
                Dialog.Error("Вы не выбрали объем!");
                return;
            }

            if (cmbKitchen.SelectedItem == null)
            {
                Dialog.Error("Вы не выбрали кухню!");
                return;
            }

            if (cmbCategory.SelectedItem == null)
            {
                Dialog.Error("Вы не выбрали категорию!");
                return;
            }

            var isPokupnoy = false;
            var selectedCat = (StockCategory)cmbType.SelectedItem;
            if (selectedCat.st_cat_name == "Покупное") isPokupnoy = true;

            if (isPokupnoy && string.IsNullOrWhiteSpace(txtProdPrice.Text))
            {
                Dialog.Error("Вы не указали себестоимость!");
                return;
            }

            if (dgvRecipe.Rows.Count == 0 && !isPokupnoy)
            {
                Dialog.Error("Вы не выбрали ингридиенты!");
                return;
            }
            
            var salePrice = Convert.ToDouble(txtSalePrice.Text);
            var prodPrice = Convert.ToDouble(txtProdPrice.Text);
            var cookingMinutes = Convert.ToInt32(txtCookingMinutes.Text);
            var volumeType = Convert.ToInt32(cmbTypeVolume.SelectedValue);
            var kitchenId = Convert.ToInt32(cmbKitchen.SelectedValue);
            var category = Convert.ToInt32(cmbCategory.SelectedValue);
            var type = Convert.ToInt32(cmbType.SelectedValue);

            if (_product == null)
            {
                var prod = new Product()
                {
                    prod_name = txtName.Text,
                    prod_price = salePrice,
                    prod_cooking_minutes = cookingMinutes,
                    prod_category = category,
                    prod_type = type,
                    prod_value = volumeType,
                    prod_start_price = prodPrice,
                    prod_kitchen = kitchenId,
                    kitchen = (Kitchen)cmbKitchen.SelectedItem,
                    category = (Category)cmbCategory.SelectedItem,
                };
                if (await new Product().OnInsertAsync(prod))
                {
                    var _prod = await new Product().OnSelectLastAsync();
                    var imageName = _prod.prod_name + _prod.prod_id;
                    ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName, true);

                    if (isPokupnoy)
                    {
                        var stock = new Stock()
                        {
                            stock_name = txtName.Text,
                            stock_count = 0,
                            stock_price = prodPrice,
                            stock_category = type,
                            stock_value = volumeType,
                        };
                        await new Stock().OnInsertAsync(stock);
                        var st = await new Stock().OnSelectLastAsync();
                        st.stock_count = 1;
                        stockList.Add(st);
                    }

                    MakeRecipe(_prod);
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                _product.prod_name = txtName.Text;
                _product.prod_price = salePrice;
                _product.prod_cooking_minutes = cookingMinutes;
                _product.prod_category = category;
                _product.prod_type = type;
                _product.prod_value = volumeType;
                _product.prod_start_price = prodPrice;
                _product.prod_kitchen = kitchenId;
                _product.kitchen = (Kitchen)cmbKitchen.SelectedItem;
                _product.category = (Category)cmbCategory.SelectedItem;

                if (await new Product().OnUpdateAsync(_product))
                {
                    var imageName = _product.prod_name + _product.prod_id;
                    ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName, true);
                    if (await new Recipe().OnDeleteProdAsync(_product.prod_id))
                    {
                        MakeRecipe(_product);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private async void MakeRecipe(Product _prod)
        {
            Recipe recipe;

            foreach (var stock in stockList)
            {
                recipe = new Recipe()
                {
                    recipe_stock = stock.stock_id,
                    recipe_count = stock.stock_count,
                    recipe_value = stock.stock_value,
                    recipe_product = _prod.prod_id,
                };
                await new Recipe().OnInsertAsync(recipe);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRecipe.CurrentCell != null)
            {
                int index = dgvRecipe.CurrentCell.RowIndex;
                stockList.RemoveAt(index);
                UpdateStocksInGrid(stockList);
            }
            else
            {
                Dialog.Error("Вы не выбрали ингридиенты!");
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "Image files(*.jpeg *.jpg *.png)|*.jpeg;*.jpg;*.png;"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Image originalImage = Image.FromFile(fd.FileName);

                // Преобразуем изображение в квадрат
                Image squareImage = ImageServerIO.ResizeToSquare(originalImage, 200); // 100 - размер квадрата по умолчанию
                pictureBox1.Image = squareImage;

                // Освобождаем оригинальное изображение
                originalImage.Dispose();
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

        private void cmbTypeVolume_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cmbSender = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter && !cmbSender.DroppedDown)
            {
                cmbSender.DroppedDown = true;
                e.Handled = true;
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var typeId = Convert.ToInt32(cmbType.SelectedValue);
            var blyudoCheck = typeId != (int)EnumStockType.Pokupnoe;
            txtProdPrice.ReadOnly = blyudoCheck;
            btnAdd.Enabled = blyudoCheck;
            btnDelete.Enabled = blyudoCheck;
           
            InitCategoryComboBox(typeId);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var productCategory = (Category)cmbCategory.SelectedItem;
            cmbTypeVolume.SelectedValue = productCategory.category_type;
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
                case "txtName":
                    return txtSalePrice;
                case "txtSalePrice":
                    if (!txtProdPrice.ReadOnly) return txtProdPrice;
                    else return txtName;
                case "txtProdPrice":
                    return txtName;
                default:
                    return txtName;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                btnSave.PerformClick();
                return true;
            }

            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                var type = Convert.ToInt32(cmbType.SelectedValue);
                var isPokupnoe = type == (int)EnumStockType.Pokupnoe;

                if (!isPokupnoe)
                {
                    btnAdd.PerformClick();
                    return true;
                }
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                var type = Convert.ToInt32(cmbType.SelectedValue);
                var isPokupnoe = type == (int)EnumStockType.Pokupnoe;

                if (!isPokupnoe)
                {
                    btnDelete.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

