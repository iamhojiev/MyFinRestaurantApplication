using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Collections.Generic;
using Type = ManagerApplication.Model.Type;

namespace ManagerApplication
{
    public partial class PagePosting : Form
    {

        // List<Model.Type> volumes = new List<Model.Type>();
        private List<Stock> stocks = new List<Stock>();
        private Stock myStock;
        public PagePosting(Stock stock = null)
        {
            InitializeComponent();
            InitForm(stock);
        }

        private async void InitForm(Stock stock)
        {
            stocks = await new Stock().OnLoadAsync();
            cmbName.DataSource = stocks;
            cmbName.DisplayMember = "stock_name";
            cmbName.ValueMember = "stock_id";
            cmbName.SelectedIndex = 0;

            var types = await new PostingType().OnLoadAsync();
            cmbType.DataSource = types;
            cmbType.DisplayMember = "post_type_name";
            cmbType.ValueMember = "post_type_id";


            if (stock != null)
            {
                cmbName.SelectedValue = stock.stock_id;
                myStock = stock;
            }
        }

        private void TxtName_SelectionChanged(object sender, EventArgs e)
        {
            //cmbType.SelectedValue = stocks[cmbName.SelectedIndex].stock_value;
            myStock = stocks[cmbName.SelectedIndex];
            txtTotal.Text = myStock.stock_count.ToString();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtAddTotal.Text != "")
            {
                try
                {
                    var total = Convert.ToDouble(txtAddTotal.Text);

                    if (myStock.stock_count < total)
                    {
                        Dialog.Error("На складе нет такого количества для списания!");
                        return;
                    }

                    myStock.stock_count -= total;
                    await new Stock().OnUpdateCountAsync(myStock);

                    CreatePosting();

                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message);
                }
            }
            else
                Dialog.Error("Вы не ввели количество!");
        }

        private async void CreatePosting()
        {
            var total = Convert.ToDouble(txtAddTotal.Text);
            var postingType = Convert.ToInt32(cmbType.SelectedValue);

            var posting = new Posting()
            {
                post_date = MyDate.DateFormat(),
                post_stock_id = myStock.stock_id,
                post_categories = postingType,
                post_value = total,
                post_type = myStock.stock_value,
                type = (Type)cmbType.SelectedItem,
                stock = (Stock)cmbName.SelectedItem,
            };
            await new Posting().OnInsertAsync(posting);

            if (postingType == 1) // возврат
            {
                var summa = myStock.stock_price * total;
                var comment = $"{myStock.stock_name} x {total}{myStock.StockTypeName}";
                await BalanceSystem.Instance.AddTransactionOperation(EnumTransactionType.Доход, summa, comment);
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

        private void BtnClose(object sender, EventArgs e)
        {
            Close();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !cmbName.DroppedDown)
            {
                cmbName.DroppedDown = true;
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