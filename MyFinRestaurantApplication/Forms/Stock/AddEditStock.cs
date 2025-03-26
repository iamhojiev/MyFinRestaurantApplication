using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;

namespace ManagerApplication
{
    public partial class AddEditStock : Form
    {
        Stock myStock = null;
        public AddEditStock(Stock stock = null)
        {
            InitializeComponent();
            InitForm(stock);
            FocusToTxtName();
        }

        private void FocusToTxtName()
        {
            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private async void InitForm(Stock stock)
        {
            var volumes = await new Model.Type().OnLoadAsync();
            cmbType.DataSource = volumes;
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";
            cmbType.SelectedIndex = 0;

            var categories = await new StockCategory().OnLoadAsync();
            categories.Reverse();
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "st_cat_name";
            cmbCategory.ValueMember = "st_cat_id";
            cmbCategory.SelectedIndex = 0;

            if (stock != null)
            {
                myStock = stock;
                txtTitle.Text = "Изменение";
                txtName.Text = myStock.stock_name;
                txtPrice.Text = myStock.stock_price.ToString();
                txtPrice.ReadOnly = true;
                cmbCategory.SelectedValue = Convert.ToInt32(myStock.stock_category);
                cmbType.SelectedValue = Convert.ToInt32(myStock.stock_value);
            }
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                try
                {
                    var typeId = Convert.ToInt32(cmbType.SelectedValue);
                    var catId = Convert.ToInt32(cmbCategory.SelectedValue);
                    if (myStock == null)
                    {
                        var el = new Stock()
                        {
                            stock_name = txtName.Text,
                            stock_count = 0,
                            stock_value = typeId,
                            stock_price = Convert.ToDouble(txtPrice.Text),
                            stock_category = catId
                        };
                        await new Stock().OnInsertAsync(el);
                    }
                    else
                    {
                        myStock.stock_name = txtName.Text;
                        myStock.stock_value = typeId;
                        myStock.stock_category = catId;
                        myStock.stock_price = Convert.ToDouble(txtPrice.Text);

                        await new Stock().OnUpdateAsync(myStock);
                    }
                    Dialog.Info("Операция выполнена успешно!");

                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message);
                }
            }
            else
                Dialog.Error("Вы не указали название!!!!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }

        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cmbSender = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter && !cmbSender.DroppedDown)
            {
                cmbSender.DroppedDown = true;
                e.Handled = true;
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

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

