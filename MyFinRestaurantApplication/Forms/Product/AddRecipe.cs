using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddRecipe : Form
    {
        public delegate void StockAddedEventHandler(object sender, StockEventArgs e);
        public event StockAddedEventHandler StockAdded;

        private Stock stock = new Stock();
        private List<Stock> stocks = new List<Stock>();

        public AddRecipe()
        {
            InitializeComponent();

            InitForm();
        }

        private async void InitForm()
        {
            var volumes = await new Model.Type().OnLoadAsync();
            cmbType.DataSource = volumes;
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";

            stocks = await new Stock().OnLoadAsync();
            txtName.DisplayMember = "stock_name";
            txtName.ValueMember = "stock_id";
            foreach (var i in stocks)
            {
                i.type = volumes.Where(u => u.type_id == i.stock_value).FirstOrDefault();
                txtName.Items.Add(i.stock_name);
            }
            txtName.SelectedIndex = 0;
            txtName.Focus();
            txtName.Select();
        }

        private void TxtName_SelectionChanged(object sender, EventArgs e)
        {
            stock = stocks[txtName.SelectedIndex];
            cmbType.SelectedValue = stocks[txtName.SelectedIndex].stock_value;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var volume = Convert.ToDouble(txtTotal.Text);
                var recipe = new Stock()
                {
                    stock_id = stock.stock_id,
                    stock_name = stock.stock_name,
                    stock_value = stock.stock_value,
                    stock_count = volume,
                    stock_price = stock.stock_price,
                    type = stock.type
                };
                StockAdded?.Invoke(this, new StockEventArgs { NewStock = recipe });
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.Message.ToString());
            }
        }

        private void X_Click(object sender, EventArgs e)
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

        private void cmb_KeyDown(object sender, KeyEventArgs e)
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

