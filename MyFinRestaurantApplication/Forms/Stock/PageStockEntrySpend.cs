using ManagerApplication.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class PageStockEntrySpend : Form
    {
        private Stock myStock;

        public PageStockEntrySpend(Stock stock)
        {
            InitializeComponent();
            dgvStockEntry.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStockSpends.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            myStock = stock;

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await UpdateSpendGrid();
            await UpdateEntryGrid();
        }

        private async Task UpdateSpendGrid()
        {
            BindingSource bs = new BindingSource();

            // Загружаем данные
            var recipesTask = new Recipe().OnSelectStockInRecipeAsync(myStock.stock_id);
            var ordersTask = new Order().OnLoadAsync();
            var prodsTask = new Product().OnLoadAsync();

            await Task.WhenAll(recipesTask, ordersTask, prodsTask);

            var recipes = recipesTask.Result;
            var orders = ordersTask.Result;
            var prods = prodsTask.Result;
            var dbDetail = new OrderDetails();

            double priceTotal = 0.0;
            double itemCount = 0.0;

            foreach (var recipe in recipes)
            {
                // Безопасное присваивание с обработкой null
                recipe.product = prods.FirstOrDefault(p => p.prod_id == recipe.recipe_product);

                if (recipe.product == null)
                {
                    // Логируем или пропускаем элемент, если продукт отсутствует
                    continue;
                }

                var details = await dbDetail.OnSelectAllProductDetailsAsync(recipe.product.prod_id);

                foreach (var detail in details)
                {
                    var order = orders.FirstOrDefault(o => o.order_main == detail.details_order);
                    var stockPrice = (detail.details_count * recipe.recipe_count) * myStock.stock_price;

                    var st = new StockSpendMetrics()
                    {
                        prod_name = recipe.product.prod_name,
                        prod_total = detail.details_count,
                        order_num = order?.OrderNum ?? "N/A", // Проверка на null
                        stock_total = detail.details_count * recipe.recipe_count,
                        stock_price = stockPrice,
                        date = order?.order_date ?? "N/A", // Проверка на null
                    };

                    priceTotal += stockPrice;
                    itemCount += st.stock_total;

                    bs.Add(st);
                }
            }

            txtSpendSum.Text = $"Общее количество: {itemCount} | Суммарная стоимость: {priceTotal:N2} смн.";
            dgvStockSpends.DataSource = bs;
        }


        private async Task UpdateEntryGrid()
        {
            BindingSource bs = new BindingSource();

            // Загружаем данные параллельно
            var stocksTask = new Stock().OnLoadAsync();
            var entrysTask = new Entry().OnLoadAsync();
            var volumesTask = new Type().OnLoadAsync();
            var detailsTask = new EntryDetails().OnSelectDetailsByStockAsync(myStock.stock_id);

            await Task.WhenAll(stocksTask, entrysTask, volumesTask, detailsTask);

            var stocks = stocksTask.Result;
            var entrys = entrysTask.Result;
            var volumes = volumesTask.Result;
            var details = detailsTask.Result;

            double priceTotal = 0.0;
            double itemCount = 0;

            foreach (var detail in details)
            {
                // Безопасное получение данных
                detail.stock = stocks.FirstOrDefault(s => s.stock_id == detail.details_stock);
                detail.entry = entrys.FirstOrDefault(e => e.entry_id == detail.details_entry);
                detail.type = volumes.FirstOrDefault(s => s.type_id == detail.details_value);

                priceTotal += detail.details_price * detail.details_count;
                itemCount += detail.details_count;

                bs.Add(detail);
            }

            dgvStockEntry.DataSource = bs;
            txtEntrySum.Text = $"Общее количество: {itemCount} | Суммарная стоимость: {priceTotal:N2} смн.";
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
