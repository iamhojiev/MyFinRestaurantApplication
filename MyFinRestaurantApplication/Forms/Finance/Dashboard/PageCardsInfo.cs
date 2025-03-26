using ManagerApplication.Helper;
using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageCardsInfo : Form
    {

        public PageCardsInfo()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var cards = await new Card().OnLoadAsync();

            foreach (Card card in cards) 
            {
                bs.Add(card);
            }

            dgvMain.DataSource = bs;
        }
    }
}
