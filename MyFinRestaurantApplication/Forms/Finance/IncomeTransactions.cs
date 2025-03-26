using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class IncomeTransactions : Form
    {
        private int cardIncomeId = 1;
        private int cassaIncomeId = 2;
        private Cassa selectedCassa;
        private Card selectedCard;

        public IncomeTransactions()
        {
            InitializeComponent();

            LoadCategories();
            UpdateInfoTxt();
            txtSumma.Focus();
        }

        private async void LoadCategories()
        {
            var incomes = await new IncomeCategory().OnLoadAsync();
            cmbCategory.DataSource = incomes;
            cmbCategory.DisplayMember = "category_name";
            cmbCategory.ValueMember = "category_id";
            cmbCategory.SelectedIndex = -1;
            cmbCategory.SelectedIndexChanged += new EventHandler(cmbCategory_SelectedIndexChanged);
        }

        private void UpdateInfoTxt()
        {
            txtComment.ReadOnly = false;

            if (selectedCard != null)
            {
                txtInfo.Text = $"*Баланс карты ({selectedCard.card_name}): {selectedCard.card_balance}";
            }
            else if (selectedCassa != null)
            {
                txtInfo.Text = $"*Денег в кассе ({selectedCassa.cassa_name}): {selectedCassa.cassa_money}";
            }
            else
            {
                txtInfo.Text = string.Empty;
            }
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
            bool cassaTransaction = false;
            bool cardTransaction = false;

            if (selectedCassa != null)
            {
                cassaTransaction = await ProcessCassaTransaction(summa);
            }

            if (selectedCard != null)
            {
                cardTransaction = await ProcessCardTransaction(summa);
            }

            if (selectedCassa != null && !cassaTransaction ||
                selectedCard != null && !cardTransaction)
            {
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private async Task<bool> ProcessCassaTransaction(double summa)
        {
            if (selectedCassa.cassa_money < summa)
            {
                Dialog.Error("Недостаточно денег на выбранной кассе");
                return false;
            }

            selectedCassa.cassa_money -= summa;
            if (await new Cassa().OnUpdateAsync(selectedCassa))
            {
                await BalanceSystem.Instance.AddCassaOperation(EnumCassaOperationType.Снятие, EnumWithdrawalType.Наличными, summa, selectedCassa.cassa_money, selectedCassa.cassa_id, txtComment.Text.Trim());
            }

            return true;
        }

        private async Task<bool> ProcessCardTransaction(double summa)
        {
            if (selectedCard.card_balance < summa)
            {
                Dialog.Error("Недостаточно денег на выбранной карте");
                return false;
            }

            selectedCard.card_balance -= summa;
            if (await new Card().OnUpdateAsync(selectedCard))
            {
                await BalanceSystem.Instance.AddCassaOperation(EnumCassaOperationType.Снятие, EnumWithdrawalType.Безналичными, summa, selectedCard.card_balance, selectedCard.card_id, txtComment.Text.Trim());
            }

            return true;
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

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCassa = null;
            selectedCard = null;
            if (Convert.ToInt32(cmbCategory.SelectedValue) == cassaIncomeId)
            {
                CassaSelect cassaSelect = new CassaSelect();
                if (cassaSelect.ShowDialog() == DialogResult.OK)
                {
                    selectedCassa = cassaSelect.selectedCassa;
                }
                else
                {
                    cmbCategory.SelectedIndex = -1;
                }
            }
            else if (Convert.ToInt32(cmbCategory.SelectedValue) == cardIncomeId)
            {
                CardSelect cardSelect = new CardSelect();
                if (cardSelect.ShowDialog() == DialogResult.OK)
                {
                    selectedCard = cardSelect.selectedCard;
                }
                else
                {
                    cmbCategory.SelectedIndex = -1;
                }
            }
            UpdateInfoTxt();
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

