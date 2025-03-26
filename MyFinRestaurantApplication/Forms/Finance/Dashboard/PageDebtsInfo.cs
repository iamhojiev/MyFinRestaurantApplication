using ManagerApplication.Model;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageDebtsInfo : Form
    {

        public PageDebtsInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var debts = await new Client().OnLoadDebtorsAsync();
            foreach (var i in debts)
            {
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }
    }
}
