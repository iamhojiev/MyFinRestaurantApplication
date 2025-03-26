using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageCassaLog : Form
    {
        private Cassa myCassa;

        public PageCassaLog(Cassa cassa)
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            myCassa = cassa;
            UpdateGrid();
        }

        public async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();
            var cassaLogs = (await new CassaLog().OnLoadTransactionsAsync()).Where(log => log.transaction_cassa == myCassa.cassa_id);
            var users = await new User().OnAllUserAsync();

            foreach (var i in cassaLogs)
            {
                i.user = users.Where(u => u.user_id == i.transaction_user).FirstOrDefault();
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
