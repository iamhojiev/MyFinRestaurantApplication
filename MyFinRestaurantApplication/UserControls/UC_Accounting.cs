using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static Google.Rpc.Context.AttributeContext.Types;


namespace ManagerApplication.UserControls
{
    public partial class UC_Accounting : UserControl
    {
        // Глобальные переменные для словарей
        private Dictionary<int, Tables> _tablesDict;
        private Dictionary<int, Hall> _hallsDict;
        private Dictionary<int, User> _usersDict;
        private Dictionary<int, Product> _productsDict;

        public UC_Accounting()
        {
            InitializeComponent();
            dgvCassaHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvIncoming.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvShiftHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTopProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTopWorkers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            metroTabControl1.SelectedIndex = 0;
            ApplyColumnSettings();
            InitForm();

            cmbPeriod.SelectedIndex = 0;
            //UpdateCurrentTab();
        }

        private async void InitForm()
        {
            List<User> users = new List<User>
            {
                new User() { user_name = "Показать все" },
            };
            users.AddRange(await new User().OnAllUserAsync());
            cmbUser.ValueMember = "user_id";
            cmbUser.DisplayMember = "user_name";
            cmbUser.DataSource = users;
            cmbUser.SelectedIndexChanged += AccountingFilter_SelectedIndexChanged;

            List<Tables> tables = new List<Tables>
            {
                new Tables(){ table_name = "Показать все", },
            };
            tables.AddRange(await new Tables().OnLoadAsync());
            cmbTable.ValueMember = "table_name";
            cmbTable.DisplayMember = "table_name";
            cmbTable.DataSource = tables;
            cmbTable.SelectedIndexChanged += AccountingFilter_SelectedIndexChanged;

            List<Hall> hall = new List<Hall>
            {
                new Hall() { hall_name = "Показать все", },
            };
            hall.AddRange(await new Hall().OnLoadAllAsync());
            cmbHall.ValueMember = "hall_name";
            cmbHall.DisplayMember = "hall_name";
            cmbHall.DataSource = hall;
            cmbHall.SelectedIndexChanged += AccountingFilter_SelectedIndexChanged;

            cmbUser2.ValueMember = "user_id";
            cmbUser2.DisplayMember = "user_name";
            cmbUser2.DataSource = users;

            List<Category> category = new List<Category>
            {
                new Category(){ category_name = "Все категории", },
            };
            category.AddRange(await new Category().OnLoadAsync());

            List<Kitchen> kitchen = new List<Kitchen>
            {
                new Kitchen(){ kitchen_name = "Все кухни", },
            };
            kitchen.AddRange(await new Kitchen().OnLoadAsync());

            cmbProdCategory.ValueMember = "category_id";
            cmbProdCategory.DisplayMember = "category_name";
            cmbProdKitchen.ValueMember = "kitchen_id";
            cmbProdKitchen.DisplayMember = "kitchen_name";
            cmbProdCategory.DataSource = category;
            cmbProdKitchen.DataSource = kitchen;
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
        }

        public void UpdateCurrentTab()
        {
            var myTab = metroTabControl1.SelectedTab;

            switch (myTab.Name)
            {
                case "pageIncoming":
                    UpdateIncoming();
                    break;
                case "pageTopProds":
                    UpdateTopProducts();
                    break;
                case "pageCassa":
                    UpdateCassa();
                    break;
                case "pageShift":
                    UpdateShiftHistory();
                    break;
                case "pageTopWorkers":
                    UpdateTopWorkers();
                    break;
            }
        }

        private void cmbPeriodIndexChange(object sender, EventArgs e)
        {
            DateTime endPeriod = DateTime.Today;
            DateTime startPeroiod = DateTime.Today;
            switch (cmbPeriod.SelectedIndex)
            {
                case 0:
                    startPeroiod = endPeriod.AddDays(-1);
                    break;
                case 1:
                    startPeroiod = endPeriod.AddDays(-7);
                    break;
                case 2:
                    startPeroiod = endPeriod.AddMonths(-1);
                    break;
                case 3:
                    startPeroiod = endPeriod.AddMonths(-3);
                    break;
                case 4:
                    startPeroiod = endPeriod.AddMonths(-6);
                    break;
                case 5:
                    startPeroiod = endPeriod.AddYears(-1);
                    break;
                case 6:
                    startPeroiod = otDate.MinDate;
                    endPeriod = otDate.MaxDate;
                    break;
            }
            otDate.Text = startPeroiod.ToString();
            doDate.Text = endPeriod.ToString();
            UpdateIncoming();
        }

        private void OnEndDatePick(object sender, EventArgs e)
        {
            UpdateIncoming();
        }

        private async Task InitializeDataDictionaries()
        {
            if (_usersDict != null) return;

            // Параллельная загрузка данных
            var tablesTask = new Tables().OnLoadAsync();
            var hallsTask = new Hall().OnLoadAllAsync();
            var usersTask = new User().OnAllUserAsync();
            var productsTask = new Product().OnLoadAsync();

            await Task.WhenAll(tablesTask, hallsTask, usersTask, productsTask);

            // Инициализация словарей
            _tablesDict = (await tablesTask).ToDictionary(t => t.table_id);
            _hallsDict = (await hallsTask).ToDictionary(h => h.hall_id);
            _usersDict = (await usersTask).ToDictionary(u => u.user_id);
            _productsDict = (await productsTask).ToDictionary(p => p.prod_id);
        }


        private async void UpdateIncoming()
        {
            //var totalStopwatch = Stopwatch.StartNew();
            //var sw = new Stopwatch();

            //sw.Restart();
            await InitializeDataDictionaries();
            //Console.WriteLine($"InitializeDictionaries: {sw.ElapsedMilliseconds}ms");
            //sw.Stop();
            BindingSource bs = new BindingSource();

            var orders = new List<Order>();

            //sw.Restart();
            var startDate = DateTime.Parse(otDate.Text);
            var endDate = DateTime.Parse(doDate.Text);
            //sw.Stop();
            //Console.WriteLine($"Parse dates: {sw.ElapsedMilliseconds}ms");

            //sw.Restart();
            if (cmbUser.SelectedIndex == 0)
                orders = await new Order().OnLoadByDescAsync();
            else
            {
                var myUser = Convert.ToInt32(cmbUser.SelectedValue);
                orders = await new Order().OnSelectUserByDescAsync(myUser);
            }
            //sw.Stop();
            //Console.WriteLine($"Load orders: {sw.ElapsedMilliseconds}ms (Count: {orders.Count})");

            //sw.Restart();
            // Фильтрация по статусу
            if (cmbStatus.SelectedIndex > 0)
            {
                var statusFilter = cmbStatus.SelectedIndex == 1
                    ? (int)EnumOrderStatus.Paid
                    : (int)EnumOrderStatus.NotPaid;
                orders = orders.Where(u => u.order_status == statusFilter).ToList();
            }
            //sw.Stop();
            //Console.WriteLine($"Status filter: {sw.ElapsedMilliseconds}ms");

            //sw.Restart();
            int selectedTableId = (cmbTable.SelectedItem as Tables)?.table_id ?? 0;
            int selectedHallId = (cmbHall.SelectedItem as Hall)?.hall_id ?? 0;

            // Оптимизированная фильтрация по дате
            orders = orders.Where(order =>
            {
                var orderDate = DateTime.Parse(order.order_date).Date;
                return orderDate >= startDate && orderDate <= endDate;
            }).ToList();

            orders = orders.Select(i =>
            {
                if (_tablesDict.TryGetValue(i.order_table, out var table))
                {
                    i.tables = table;
                    if (_hallsDict.TryGetValue(table.table_hall_id, out var hallItem))
                    {
                        i.tables.hall = hallItem;
                    }
                }
                if (_usersDict.TryGetValue(i.order_user, out var user))
                {
                    i.user = user;
                }
                return i;
            })
            .Where(i => (selectedTableId == 0 || i.tables?.table_id == selectedTableId) &&
                        (selectedHallId == 0 || i.tables?.hall?.hall_id == selectedHallId))
            .ToList();

            //sw.Stop();
            //Console.WriteLine($"Date/table/hall filtering: {sw.ElapsedMilliseconds}ms");

            var orderDetails = new OrderDetails();
            var priceTotal = 0.0;
            var discountTotal = 0.0;
            var rasxod = 0.0;
            var waiterMoney = 0.0;

            //sw.Restart();
            // Пакетная загрузка всех OrderDetails
            var orderIds = orders.Select(o => o.order_main).ToList();
            var allOrderDetails = await orderDetails.OnSelectOrderDetailsBatchAsync(orderIds); // Нужно реализовать этот метод
            var detailsLookup = allOrderDetails.ToLookup(d => d.details_order);

            foreach (var i in orders)
            {
                i.orderDetails = detailsLookup[i.order_main].ToList();

                priceTotal += i.order_price + i.order_discount;
                discountTotal += i.order_discount;
                waiterMoney += i.order_price_waiter;

                foreach (var q in i.orderDetails)
                {
                    if (_productsDict.TryGetValue(q.details_prod, out var product))
                    {
                        q.product = product;
                        rasxod += product.prod_start_price * q.details_count;
                    }
                }

                bs.Add(i);
            }
            //sw.Stop();
            //Console.WriteLine($"Order details processing: {sw.ElapsedMilliseconds}ms");

            //sw.Restart();
            dgvIncoming.DataSource = bs;
            txtInfo.Text = $"Количество чеков: {dgvIncoming.Rows.Count}   Общая стоимость: {priceTotal}   Скидки: {discountTotal}   Потрачено: {rasxod}   Официантам: {waiterMoney}   Чистая прибыль: {priceTotal - discountTotal - rasxod - waiterMoney}";
            //sw.Stop();
            //Console.WriteLine($"UI update: {sw.ElapsedMilliseconds}ms");

            //Console.WriteLine($"Total time: {totalStopwatch.ElapsedMilliseconds}ms");
        }

        private void ShiftFiltresChanged(object sender, EventArgs e)
        {
            UpdateShiftHistory();
        }

        private async void UpdateShiftHistory()
        {
            BindingSource bs = new BindingSource();

            // Параллельная загрузка всех данных
            var loadShiftsTask = new Shift().OnLoadAsync();
            var loadUsersTask = new User().OnAllUserAsync();
            var loadOrdersTask = new Order().OnLoadAsync();

            await Task.WhenAll(loadShiftsTask, loadUsersTask, loadOrdersTask);

            var shifts = await loadShiftsTask;
            var users = await loadUsersTask;
            var orders = await loadOrdersTask;

            // Фильтрация по пользователю
            if (cmbUser2.SelectedIndex != 0)
            {
                int userId = Convert.ToInt32(cmbUser2.SelectedValue);
                shifts = shifts.Where(u => u.shift_user_id == userId).ToList();
            }

            // Связываем данные
            foreach (var shift in shifts)
            {
                shift.user = users.FirstOrDefault(u => u.user_id == shift.shift_user_id);
                shift.shiftOrders = orders.Where(or => or.order_shift == shift.shift_id).ToList();
            }

            dgvShiftHistory.DataSource = shifts;
        }

        private void cmbProdKitchen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTopProducts();
        }

        private async void UpdateTopProducts()
        {
            List<Product> prodList = new List<Product>();

            var products = await new Product().OnLoadAsync();
            var types = await new Model.Type().OnLoadAsync();
            var category = await new Category().OnLoadAsync();
            var kitchen = await new Kitchen().OnLoadAsync();

            if (cmbProdCategory.SelectedIndex != 0)
            {
                var myCategoryValue = Convert.ToInt32(cmbProdCategory.SelectedValue);
                products = products.Where(u => u.prod_category == myCategoryValue).ToList();
            }

            if (cmbProdKitchen.SelectedIndex != 0)
            {
                var myKitchenValue = Convert.ToInt32(cmbProdKitchen.SelectedValue);
                products = products.Where(u => u.prod_kitchen == myKitchenValue).ToList();
            }

            //   if (txtFind.Text != "")
            //       products = products.Where(u => u.prod_name.ToUpper().Contains(txtFind.Text.ToString().ToUpper())).ToList();

            OrderDetails dbOrderDetails = new OrderDetails();
            foreach (var i in products)
            {
                i.type = types.FirstOrDefault(u => u.type_id == i.prod_value);
                i.category = category.FirstOrDefault(u => u.category_id == i.prod_category);
                i.kitchen = kitchen.FirstOrDefault(u => u.kitchen_id == i.prod_kitchen);
                i.orderDetails = await dbOrderDetails.OnSelectAllProductDetailsAsync(i.prod_id);
                prodList.Add(i);
            }

            prodList.Sort((p1, p2) => p2.ProdBuy.CompareTo(p1.ProdBuy));
            dgvTopProduct.DataSource = prodList;
        }

        private async void UpdateTopWorkers()
        {
            // 1. Загружаем данные ПАРАЛЛЕЛЬНО
            var usersTask = new User().OnAllUserAsync();
            var ordersTask = new Order().OnLoadAsync();
            var shiftsTask = new Shift().OnLoadAsync();

            // Ждем завершения всех задач одновременно
            await Task.WhenAll(usersTask, ordersTask, shiftsTask);

            var users = await usersTask;
            var orders = await ordersTask;
            var shifts = await shiftsTask;

            // 2. Группируем заказы по сменам (shift_id → список заказов)
            var ordersGroupedByShift = orders
                .GroupBy(o => o.order_shift)
                .ToDictionary(g => g.Key, g => g.ToList());

            // 3. Создаем словарь: user_id → список его смен
            var shiftsByUser = shifts
                .GroupBy(s => s.shift_user_id)
                .ToDictionary(g => g.Key, g => g.ToList());

            // 4. Рассчитываем метрики для каждого пользователя
            var result = new List<UserMetrics>();

            foreach (var user in users)
            {
                double totalSum = 0;
                int totalOrders = 0;

                // Если у пользователя есть смены
                if (shiftsByUser.TryGetValue(user.user_id, out var userShifts))
                {
                    foreach (var shift in userShifts)
                    {
                        // Если в смене есть заказы
                        if (ordersGroupedByShift.TryGetValue(shift.shift_id, out var shiftOrders))
                        {
                            totalSum += shiftOrders.Sum(o => o.order_price); // Замените на ваше поле суммы
                            totalOrders += shiftOrders.Count;
                        }
                    }
                }

                result.Add(new UserMetrics
                {
                    user_name = user.user_name,
                    summa_total = totalSum,
                    check_count = totalOrders
                });
            }

            // 5. Обновляем интерфейс
            dgvTopWorkers.DataSource = result;
        }

        private async void UpdateCassa()
        {
            var cassaTransactions = await new CassaLog().OnLoadTransactionsAsync();
            var users = await new User().OnAllUserAsync();
            var cassas = await new Cassa().OnLoadAsync();
            var cards = await new Card().OnLoadAsync();

            foreach (var transaction in cassaTransactions)
            {
                transaction.user = users.FirstOrDefault(u => u.user_id == transaction.transaction_user);
                if (transaction.transaction_cassa != 0)
                {
                    transaction.cassa = cassas?.FirstOrDefault(c => c.cassa_id == transaction.transaction_cassa);
                }
                else if (transaction.transaction_card != 0)
                {
                    transaction.card = cards?.FirstOrDefault(c => c.card_id == transaction.transaction_card);
                }
            }
            dgvCassaHistory.DataSource = cassaTransactions;
        }

        private void dgvIncoming_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var aaa = (Order)dgvIncoming.SelectedRows[0].DataBoundItem;
                var window = new PageOrderDetails(aaa);
                window.ShowDialog();
                UpdateIncoming();
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали заказа!");
                return;
            }
        }

        private void AccountingFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateIncoming();
        }

        private void btnConfigureColumns_Click(object sender, EventArgs e)
        {
            var settingsForm = new ColumnsSettingsForm(dgvIncoming); // Открываем форму настройки колонок
            settingsForm.ShowDialog();

            if (settingsForm.IsSettingsSaved)
            {
                SaveColumnSettings(settingsForm.GetColumnSettings()); // Сохраняем настройки
                ApplyColumnSettings(); // Применяем настройки
            }
        }

        private void SaveColumnSettings(Dictionary<string, bool> columnSettings)
        {
            var json = JsonConvert.SerializeObject(columnSettings);
            File.WriteAllText("columnSettings.json", json);
        }

        private void ApplyColumnSettings()
        {
            if (File.Exists("columnSettings.json"))
            {
                var json = File.ReadAllText("columnSettings.json");
                var columnSettings = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);

                foreach (var column in dgvIncoming.Columns.Cast<DataGridViewColumn>())
                {
                    if (columnSettings.ContainsKey(column.HeaderText))
                    {
                        column.Visible = columnSettings[column.HeaderText];
                    }
                }
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvIncoming.SelectedRows.Count == 0)
            {
                Dialog.Error("Вы ничего не выбрали.");
                return;
            }

            var selectedOrder = (Order)dgvIncoming.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show(
                    $"Вы действительно хотите удалить заказа №{selectedOrder.OrderNum}?", "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (await new Order().OnDeleteOrderCascadeAsync(selectedOrder.order_id))
            {
                Dialog.Info($"Заказ №{selectedOrder.OrderNum} успешно удален");
                UpdateIncoming();
            }
        }

        //private async void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    if (dgvIncoming.SelectedRows.Count == 0)
        //    {
        //        Dialog.Error("Вы ничего не выбрали.");
        //        return;
        //    }

        //    var selectedOrder = (Order)dgvIncoming.SelectedRows[0].DataBoundItem;

        //    if (MessageBox.Show(
        //            $"Вы действительно хотите удалить заказа №{selectedOrder.OrderNum}?", "Удаление",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question) != DialogResult.Yes)
        //    {
        //        return;
        //    }

        //    selectedOrder.orderDetails = await new OrderDetails().OnSelectOrderDetailsAsync(selectedOrder.order_main);
        //    var products = await new Product().OnLoadAsync();

        //    if (await new Order().OnDeleteOrderAsync(selectedOrder.order_id))
        //    {
        //        foreach (var detail in selectedOrder.orderDetails)
        //        {
        //            detail.product = products.FirstOrDefault(p => p.prod_id == detail.details_prod);

        //            if (await new OrderDetails().OnDeleteAsync(detail.details_id))
        //            {
        //                await UpdateProductStockAsync(detail.product);
        //            }
        //        }

        //        var orderTransaction = await new Transactions().OnSelectOrderTransactionAsync(selectedOrder.order_main);
        //        await new Transactions().OnDeleteTransactionAsync(orderTransaction.transaction_id);
        //        await UpdateTransactionBalance(orderTransaction, selectedOrder.order_price);

        //        Dialog.Info($"Заказ №{selectedOrder.OrderNum} успешно удален");
        //        UpdateIncoming();
        //    }
        //}

        //private async Task UpdateTransactionBalance(Transactions orderTransaction, double priceToDeduct)
        //{
        //    if (orderTransaction.transaction_payment == (int)EnumPayment.Card)
        //    {
        //        var card = await new Card().OnSelectAsync(orderTransaction.transaction_cassa);
        //        card.card_balance -= priceToDeduct;
        //        await new Card().OnUpdateAsync(card);
        //    }
        //    else
        //    {
        //        var orderCassa = await new Cassa().OnSelectCassaAsync(orderTransaction.transaction_cassa);
        //        orderCassa.cassa_money -= priceToDeduct;
        //        await new Cassa().OnUpdateAsync(orderCassa);
        //    }
        //}

        //private async Task UpdateProductStockAsync(Product product)
        //{
        //    var stocks = await new Stock().OnLoadAsync();
        //    var dbStock = new Stock();

        //    product.recipe = await new Recipe().OnSelectProductAsync(product.prod_id);

        //    foreach (var i in product.recipe)
        //    {
        //        i.stock = stocks.Where(u => u.stock_id == i.recipe_stock).FirstOrDefault();

        //        i.stock.stock_count += (i.recipe_count * product.prod_total);

        //        await dbStock.OnUpdateCountAsync(i.stock);
        //    }
        //}
    }
}