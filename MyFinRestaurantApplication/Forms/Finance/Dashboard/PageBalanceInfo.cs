using ManagerApplication.Helper;
using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageBalanceInfo : Form
    {

        public PageBalanceInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            var logsTask = new TransactionLog().OnLoadBalanceTransactionsAsync();
            var usersTask = new User().OnAllUserAsync();

            await Task.WhenAll(logsTask, usersTask); // Ждём завершения обоих задач параллельно

            var logs = await logsTask;
            var users = await usersTask;

            var userDictionary = users.ToDictionary(u => u.user_id);

            var enhancedLogs = logs
                .OrderByDescending(log => log.transaction_date)
                .Select(log =>
                {
                    log.user = userDictionary.TryGetValue(log.transaction_user, out var user) ? user : null;
                    return log;
                })
                .ToList();

            dgvMain.DataSource = new BindingSource { DataSource = enhancedLogs };
        }
    }
}
