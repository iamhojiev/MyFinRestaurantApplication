using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class PayEntry : Form
    {

        private Entry myEntry;

        public PayEntry(Entry entry)
        {
            myEntry = entry;
            InitializeComponent();
            UpdateInfoTxt();
            SetFocusToSummaTxt(myEntry.GetEntryDebt);
        }

        private void UpdateInfoTxt()
        {
            if (myEntry != null)
            {
                txtInfo.Text = $"Задолженность: {myEntry.GetEntryDebt}";
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtSumma.Text, out double summa))
            {
                if (summa > myEntry.GetEntryDebt)
                {
                    Dialog.Error("Сумма больше чем нужно");
                    return;
                }

                await PaidProcess(summa);
                await CreateEntryLog(summa);
                DialogResult = DialogResult.OK;
            }
            else
            {
                Dialog.Error("Введите корректную сумму.");
            }
        }

        private async Task CreateEntryLog(double summa)
        {
            await BalanceSystem.Instance.AddEntryOperation(summa, myEntry.entry_id, txtComment.Text.Trim());
        }

        private async Task PaidProcess(double summa)
        {
            myEntry.entry_paid += summa;
            await new Entry().OnPaidUpdateAsync(myEntry, summa);
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

        private void SetFocusToSummaTxt(double summa)
        {
            txtSumma.Text = summa.ToString();
            txtSumma.Focus();
            txtSumma.Select();
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