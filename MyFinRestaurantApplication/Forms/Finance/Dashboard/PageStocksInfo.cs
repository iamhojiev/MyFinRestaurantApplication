using ManagerApplication.Model;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageStocksInfo : Form
    {
        private int pokupnoeId = 2;
        public PageStocksInfo()
        {
            InitializeComponent();
            dgvIngredient.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            UpdateGridIngredient();
            UpdateGridPokupnoe();
        }

        private async void UpdateGridPokupnoe()
        {
            BindingSource bs = new BindingSource();

            var stocks = await new Stock().OnLoadAsync();
            stocks = stocks.Where(st => st.stock_category == pokupnoeId).Where(u => u.stock_count > 0).ToList();
            var volumes = await new Type().OnLoadAsync();
            var category = await new StockCategory().OnLoadAsync();
            double pokupnoeSumma = 0.0;
            foreach (var i in stocks)
            {
                i.type = volumes.Where(u => u.type_id == i.stock_value).FirstOrDefault();
                i.stockCategory = category.Where(u => u.st_cat_id == i.stock_category).FirstOrDefault();
                pokupnoeSumma += i.Sum;
                bs.Add(i);
            }
            txtPokupnoeSum.Text = $"Сумма: {pokupnoeSumma}";
            dgvPokupnoe.DataSource = bs;
        }

        private async void UpdateGridIngredient()
        {
            BindingSource bs = new BindingSource();

            var stocks = await new Stock().OnLoadAsync();
            stocks = stocks.Where(u => u.stock_count > 0).ToList();
            var volumes = await new Type().OnLoadAsync();
            var category = await new StockCategory().OnLoadAsync();
            double ingredientSumma = 0.0;
            foreach (var i in stocks)
            {
                i.type = volumes.Where(u => u.type_id == i.stock_value).FirstOrDefault();
                i.stockCategory = category.Where(u => u.st_cat_id == i.stock_category).FirstOrDefault();
                ingredientSumma += i.Sum;
                bs.Add(i);
            }
            txtIngredientSum.Text = $"Сумма: {ingredientSumma}";
            dgvIngredient.DataSource = bs;
        }
    }
}
