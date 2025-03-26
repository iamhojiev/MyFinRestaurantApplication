using ManagerApplication.Helper;
using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageIncomingsInfo : Form
    {

        public PageIncomingsInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var spendLogs = await new TransactionLog().OnLoadSpendTransactionsAsync();
            var users = await new User().OnAllUserAsync();

            for (int i = spendLogs.Count - 1; i >= 0; i--)
            {
                spendLogs[i].user = users.FirstOrDefault(u => u.user_id == spendLogs[i].transaction_user);
                bs.Add(spendLogs[i]);
            }

            dgvMain.DataSource = bs;
        }
    }
}
