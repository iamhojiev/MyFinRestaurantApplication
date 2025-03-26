using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageMyDebtsInfo : Form
    {

        public PageMyDebtsInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var vendors = await new Vendor().OnLoadDebtorsAsync();
            foreach (var i in vendors)
            {
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }
    }
}
