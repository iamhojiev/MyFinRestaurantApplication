using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class UserPayment : Form
    {

        private User myUser;
        private SalaryType selectedType;

        public UserPayment(User user)
        {
            InitializeComponent();
            myUser = user;

            InitTypeComboBox();
            SetFocusToSummaTxt();
        }

        private void InitTypeComboBox()
        {
            var types = new SalaryType().OnLoadPay();

            cmbType.DataSource = types;
            cmbType.DisplayMember = "type_name";
            cmbType.SelectedIndex = 0;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtSumma.Text, out double summa))
            {
                await UpdateUserFinance(summa);
                await CreateSalaryLog(selectedType.type, summa, myUser.user_id);
                DialogResult = DialogResult.OK;
            }
            else
            {
                Dialog.Error("Введите корректную сумму.");
            }
        }

        private async Task UpdateUserFinance(double summa)
        {
            myUser.user_paid += summa;
            await new User().OnUpdateAsync(myUser);
        }

        private async Task CreateSalaryLog(EnumSalaryLogType type, double summa, int userId)
        {
            await BalanceSystem.Instance.AddSalaryOperation(type, summa, userId, txtComment.Text.Trim());
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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }
        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetFocusToSummaTxt()
        {
            txtSumma.Focus();
            txtSumma.Select();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex != -1)
            {
                selectedType = (SalaryType)cmbType.SelectedItem;
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