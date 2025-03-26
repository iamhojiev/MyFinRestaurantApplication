using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageTableInfo : Form
    {

        private Tables myTable;
        public PageTableInfo(Tables table)
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            myTable = table;
            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();
            var orders = await new Order().OnSelectAsync(myTable.table_id);
            var user = await new User().OnAllUserAsync();
            var table = await new Tables().OnLoadAsync();
            var hall = await new Hall().OnLoadHallAsync();

            foreach (var i in orders)
            {
                i.user = user.Where(u => u.user_id == i.order_user).FirstOrDefault();
                i.tables = table.Where(u => u.table_id == i.order_table).FirstOrDefault();
                i.tables.hall = hall.Where(u => u.hall_id == i.tables.table_hall_id).FirstOrDefault();
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
