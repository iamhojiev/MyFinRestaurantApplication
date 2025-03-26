using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApplication
{
    public partial class PageEarningsInfo : Form
    {
        private User myUser;

        public PageEarningsInfo(User user)
        {
            InitializeComponent();
            Text = $"Начислении персонала {user.user_name}";
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            myUser = user;
            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var users = await new User().OnAllUserAsync();
            var logs = (await new SalaryLog().OnLoadTransactionsAsync()).Where(l =>
            l.transaction_salary_user == myUser.user_id &&
            l.transaction_salary_type != EnumSalaryLogType.Оплата &&
            l.transaction_salary_type != EnumSalaryLogType.Предоплата);

            foreach (var i in logs)
            {
                i.salary_user = myUser;
                i.user = users.FirstOrDefault(u => u.user_id == i.transaction_user);
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            UserEarnings paySalary = new UserEarnings(myUser);
            if (paySalary.ShowDialog() == DialogResult.OK)
            {
                UpdateGrid();
                Dialog.Info("Операция прошла успешно!");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 0)
            {
                Dialog.Error("Вы не выбрали платеж");
                return;
            }

            var aaa = (SalaryLog)dgvMain.SelectedRows[0].DataBoundItem;
            if (aaa.transaction_salary_type == EnumSalaryLogType.Начисление)
            {
                Dialog.Error("Платежи категории “Начисление” не подлежат удалению.");
                return;
            }
            if (MessageBox.Show(
                "Вы действительно хотите удалить платеж?", "Удаление",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = await UpdateUserFinance(aaa.transaction_amount, aaa.transaction_salary_type);
                if (success)
                {
                    await BalanceSystem.Instance.DeleteTransaction(aaa);
                    UpdateGrid();
                }
            }
        }

        private async Task<bool> UpdateUserFinance(double summa, EnumSalaryLogType type)
        {
            switch (type)
            {
                case EnumSalaryLogType.Оплата:
                    myUser.user_paid -= summa;
                    break;
                case EnumSalaryLogType.Бонус:
                    myUser.user_bonus -= summa;
                    break;
                case EnumSalaryLogType.Штраф:
                    myUser.user_fine -= summa;
                    break;
            }
            return await new User().OnUpdateAsync(myUser);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                btnAddEntry.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                btnDeleteEntry.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

