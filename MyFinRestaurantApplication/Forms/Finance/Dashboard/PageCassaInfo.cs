using ManagerApplication.Helper;
using ManagerApplication.Model;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageCassaInfo : Form
    {

        public PageCassaInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var cassa = await new Cassa().OnLoadAsync();

            foreach (Cassa cas in cassa) 
            {
                bs.Add(cas);
            }

            dgvMain.DataSource = bs;
        }
    }
}
