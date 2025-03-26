using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;

namespace ManagerApplication
{
    public partial class SpendTransactions : Form
    {

        public SpendTransactions()
        {
            InitializeComponent();

            LoadCategories();
            txtSumma.Focus();
        }

        private async void LoadCategories()
        {
            var spends = await new SpendCategory().OnLoadAsync();
            cmbCategory.DataSource = spends;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";
            cmbCategory.SelectedIndex = -1;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtSumma.Text, out double summa))
            {
                ProcessTransaction(summa);
            }
            else
            {
                Dialog.Error("Введите корректную сумму.");
            }
        }

        private async void ProcessTransaction(double summa)
        {
            string source = cmbCategory.Text;
            await BalanceSystem.Instance.AddTransactionOperation(EnumTransactionType.Расход, summa, source);

            DialogResult = DialogResult.OK;
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbHall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !cmbCategory.DroppedDown)
            {
                cmbCategory.DroppedDown = true;
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

