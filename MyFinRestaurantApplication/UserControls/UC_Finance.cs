using System;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using ManagerApplication.Properties;
using System.Collections.Generic;
using System.Globalization;
using Guna.UI2.WinForms;

namespace ManagerApplication.UserControls
{
    public partial class UC_Finance : UserControl
    {

        private User myUser;
        private Guna2Button focusedAddBtn;
        private Guna2Button focusedDeleteBtn;
        private Guna2Button focusedEditBtn;

        public UC_Finance()
        {
            InitializeComponent();

            dgvEntry.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPosting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSalary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBudget.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            metroTabControl1.SelectedIndex = 1;
            metroTabControl1.SelectedIndex = 0;
            InitUser();
            InitBudgetFilterComboBoxes();
            InitSalaryFilterComboBoxes();
            UpdateBudgetTxt();
            //  UpdateUserSalary();
            string currentMonthName = DateTime.Now.ToString("MMMM", new CultureInfo("ru-RU"));
            txtMonth1.Text = txtMonth2.Text = $"За {currentMonthName}";

        }

        private async void InitUser()
        {
            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);
        }

        private async void InitSalaryFilterComboBoxes()
        {
            List<Role> roles = new List<Role>
            {
                new Role(){ role_name = "Показать все", },
            };
            roles.AddRange(await new Role().OnLoadAsync());
            cmbRole.DataSource = roles;
            cmbRole.DisplayMember = "role_name";
            cmbRole.ValueMember = "role_id";
            cmbRole.SelectedIndex = 0;

            cmbSalaryType.Items.Clear();
            cmbSalaryType.Items.Add("Показать все");
            cmbSalaryType.Items.Add("Ежедневная");
            cmbSalaryType.Items.Add("Ежемесячная");
            cmbSalaryType.Items.Add("Процентная");
            cmbSalaryType.SelectedIndex = 0;

            cmbSort.SelectedIndexChanged += cmbSalaryFilter_SelectedIndexChanged;
            cmbRole.SelectedIndexChanged += cmbSalaryFilter_SelectedIndexChanged;
            cmbSalaryType.SelectedIndexChanged += cmbSalaryFilter_SelectedIndexChanged;
        }

        private async void InitBudgetFilterComboBoxes()
        {
            cmbTypeFilter.Items.Clear();
            cmbTypeFilter.Items.Add("Показать все");
            cmbTypeFilter.Items.Add("Доход");
            cmbTypeFilter.Items.Add("Расход");
            cmbTypeFilter.SelectedIndex = 0;

            var incomings = await new IncomeCategory().OnLoadAsync();
            var spends = await new SpendCategory().OnLoadAsync();
            cmbSourceFilter.Items.Clear();
            cmbSourceFilter.Items.Add("Показать все");
            foreach (var spend in spends)
            {
                cmbSourceFilter.Items.Add(spend.category_name);
            }
            foreach (var income in incomings)
            {
                cmbSourceFilter.Items.Add(income.category_name);
            }
            cmbSourceFilter.Items.Add("Возврат");
            cmbSourceFilter.SelectedIndex = 0;

            cmbSourceFilter.SelectedIndexChanged += CmbBudgetFilter_SelectedIndexChanged;
            cmbTypeFilter.SelectedIndexChanged += CmbBudgetFilter_SelectedIndexChanged;
        }

        private async void UpdatePosting()
        {
            BindingSource bs = new BindingSource();

            var posting = await new Posting().OnLoadAsync();
            var stocks = await new Stock().OnLoadAsync();
            var posTypes = await new PostingType().OnLoadAsync();
            var types = await new Model.Type().OnLoadAsync();

            foreach (var i in posting)
            {
                i.stock = stocks.Where(u => u.stock_id == i.post_stock_id).FirstOrDefault();
                i.posting_type = posTypes.Where(u => u.post_type_id == i.post_categories).FirstOrDefault();
                i.type = types.Where(u => u.type_id == i.post_type).FirstOrDefault();

                bs.Add(i);
            }
            dgvPosting.DataSource = bs;
        }

        private void btnEarnings_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (User)dgvSalary.SelectedRows[0].DataBoundItem;

                    var window = new PageEarningsInfo(aaa);
                    window.ShowDialog();
                    UpdateUserSalaryRange();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали работника!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (User)dgvSalary.SelectedRows[0].DataBoundItem;

                    var window = new PagePayInfo(aaa);
                    window.ShowDialog();
                    UpdateUserSalaryRange();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали работника!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void dgvUserSalary_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (User)dgvSalary.SelectedRows[0].DataBoundItem;

                    var window = new PagePayInfo(aaa);
                    window.ShowDialog();
                    UpdateUserSalaryRange();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали работника!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void dgvUserSalary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Проверка, что индекс колонки находится в допустимом диапазоне для cmbSort
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 7)
            {
                // Установка SelectedIndex для cmbSort на основе выбранной колонки
                cmbSort.SelectedIndex = e.ColumnIndex - 1;
            }
        }

        private void cmbSalaryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUserSalaryRange();
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            UpdateUserSalaryRange();
        }

        private void cmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dateDo = DateTime.Today;
            DateTime dateOt = GetStartDate(cmbPeriod.SelectedIndex, dateDo);

            datePickerDo.Value = dateDo;
            datePickerOt.Value = dateOt;

            UpdateUserSalaryRange();
        }

        private async void UpdateUserSalaryRange()
        {
            var dateOt = DateTime.Parse(datePickerOt.Text);
            var dateDo = DateTime.Parse(datePickerDo.Text);
            var logs = await new SalaryLog().OnLoadTransactionsAsync();

            var logsInRange = logs.Where(log =>
            DateTime.Parse(log.transaction_date) >= dateOt &&
            DateTime.Parse(log.transaction_date) <= dateDo.AddDays(1)).ToList();

            BindingSource bs = new BindingSource();

            var users = await new User().OnAllUserAsync();
            var roles = await new Role().OnLoadAsync();

            cmbPeriod.SelectedIndex = 0;

            if (cmbSalaryType.SelectedIndex > 0)
            {
                switch (cmbSalaryType.SelectedIndex)
                {
                    case 1:
                        users = users.Where(u => u.user_salary_type == (int)EnumSalaryType.Daily).ToList();
                        break;
                    case 2:
                        users = users.Where(u => u.user_salary_type == (int)EnumSalaryType.Monthly).ToList();
                        break;
                    case 3:
                        users = users.Where(u => u.user_salary_type == (int)EnumSalaryType.Percent).ToList();
                        break;
                }
            }

            if (cmbRole.SelectedIndex != 0)
            {
                var roleValue = Convert.ToInt32(cmbRole.SelectedValue);
                users = users.Where(u => u.user_role == roleValue).ToList();
            }

            if (cmbSort.SelectedIndex != 0)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 1:
                        users = UserListSorter.SortByProperty(users, u => u.user_salary, false);
                        break;
                    case 2:
                        users = UserListSorter.SortByProperty(users, u => u.user_earnings, false);
                        break;
                    case 3:
                        users = UserListSorter.SortByProperty(users, u => u.user_paid, false);
                        break;
                    case 4:
                        users = UserListSorter.SortByProperty(users, u => u.user_bonus, false);
                        break;
                    case 5:
                        users = UserListSorter.SortByProperty(users, u => u.user_fine, false);
                        break;
                    case 6:
                        users = UserListSorter.SortByProperty(users, u => u.UserBalance, false);
                        break;
                }
            }

            foreach (var i in users)
            {
                i.role = roles.FirstOrDefault(u => u.role_id == i.user_role);
                i.user_paid = 0;
                i.user_earnings = 0;
                i.user_bonus = 0;
                i.user_fine = 0;

                var myLogs = logsInRange.Where(log => log.transaction_salary_user == i.user_id);
                foreach (var log in myLogs)
                {
                    double amount = log.transaction_amount;
                    if (log.transaction_salary_type == EnumSalaryLogType.Оплата || log.transaction_salary_type == EnumSalaryLogType.Предоплата)
                    {
                        i.user_paid += amount;
                    }
                    else if (log.transaction_salary_type == EnumSalaryLogType.Начисление)
                    {
                        i.user_earnings += amount;
                    }
                    else if (log.transaction_salary_type == EnumSalaryLogType.Бонус)
                    {
                        i.user_bonus += amount;
                    }
                    else if (log.transaction_salary_type == EnumSalaryLogType.Штраф)
                    {
                        i.user_fine += amount;
                    }
                }

                bs.Add(i);
            }

            dgvSalary.DataSource = bs;
        }

        private DateTime GetStartDate(int selectedIndex, DateTime startDate)
        {
            switch (selectedIndex)
            {
                case 0: return startDate.AddYears(-10);
                case 1: return startDate.AddDays(-1);
                case 2: return startDate.AddDays(-7);
                case 3: return startDate.AddMonths(-1);
                case 4: return startDate.AddMonths(-3);
                case 5: return startDate.AddMonths(-6);
                case 6: return startDate.AddYears(-1);
                default: return startDate;
            }
        }

        private void BtnPlusBudget(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new IncomeTransactions();
                if (window.ShowDialog() == DialogResult.OK)
                {
                    Dialog.Info("Операция прошла успешно");
                    UpdateBudget();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnMinusBudget_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new SpendTransactions();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateBudget();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnDeleteBudget_Click(object sender, EventArgs e)
        {
            if (dgvBudget.SelectedRows.Count == 0)
            {
                Dialog.Error("Вы ничего не выбрали.");
                return;
            }

            //var selectedLog = (MoneyLog)dgvBudget.SelectedRows[0].DataBoundItem;

            //var message = $"Вы действительно хотите удалить запись №{selectedLog.money_id}?\n" +
            //              $"Комментария: {selectedLog.money_description}\n" +
            //              $"Источник: {selectedLog.money_source}\n" +
            //              $"Пользователь: {selectedLog.GetUserName}\n" +
            //              $"Тип операции: {selectedLog.MoneyType}\n" +
            //              $"Дата: {selectedLog.money_date}\n" +
            //              $"Сумма: {selectedLog.money_total:N2}";

            //if (MessageBox.Show(message, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //{
            //    return;
            //}

            //var cassa = await new Cassa().OnLoadAsync();
            //var logCassa = cassa[0];
            //if (await new MoneyLog().OnDeleteLogAsync(selectedLog.money_id))
            //{
            //    if (selectedLog.money_type == (int)EnumMoneyLog.Income)
            //    {
            //        logCassa.cassa_money += selectedLog.money_total;
            //    }
            //    else if (selectedLog.money_type == (int)EnumMoneyLog.Spend)
            //    {
            //        logCassa.cassa_money -= selectedLog.money_total;
            //    }
            //    await new Cassa().OnUpdateAsync(logCassa);
            //    Dialog.Info($"Запись №{selectedLog.money_id} успешно удален");
            //    UpdateBudget();
            //}
        }

        private void CmbBudgetFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBudget();
        }

        private async void UpdateBudget()
        {
            BindingSource bs = new BindingSource();

            var logs = await new TransactionLog().OnLoadBalanceTransactionsAsync();
            var users = await new User().OnAllUserAsync();

            var filter = cmbTypeFilter.SelectedIndex;
            switch (filter)
            {
                case 1:
                    logs = logs.Where(u => u.transaction_type == EnumTransactionType.Доход).ToList();
                    break;
                case 2:
                    logs = logs.Where(u => u.transaction_type == EnumTransactionType.Расход).ToList();
                    break;
            }

            //if (cmbSourceFilter.SelectedIndex != 0)
            //    logs = logs.Where(u => u.money_source.ToUpper().Contains(cmbSourceFilter.Text.ToString().ToUpper())).ToList();

            for (int i = logs.Count - 1; i >= 0; i--)
            {
                logs[i].user = users.FirstOrDefault(u => u.user_id == logs[i].transaction_user);
                bs.Add(logs[i]);
            }

            dgvBudget.DataSource = bs;
            UpdateBudgetTxt();
        }

        private async void UpdateBudgetTxt()
        {
            txtBalance.Text = (await BalanceSystem.Instance.GetCurrentBalance()).ToString();
        }

        private void MetroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
        }

        public void UpdateCurrentTab()
        {
            var myTab = metroTabControl1.SelectedTab;

            switch (myTab.Name)
            {
                case "pageBudget":
                    focusedAddBtn = btnPlusBalance;
                    focusedEditBtn = null;
                    focusedDeleteBtn = btnMinusBalance;
                    UpdateBudget();
                    break;
                case "pageDashboard":
                    focusedAddBtn = null;
                    focusedEditBtn = null;
                    focusedDeleteBtn = null;
                    timerDashboard.Start();
                    break;
                case "pageEntry":
                    focusedAddBtn = btnAddEntry;
                    focusedEditBtn = btnEditEntry;
                    focusedDeleteBtn = btnDeleteEntry;
                    UpdateEntryGrid();
                    break;
                case "pageSalary":
                    cmbPeriod.SelectedIndex = 1;
                    cmbPeriod.SelectedIndex = 0;
                    UpdatePosting();
                    break;
                case "pagePosting":
                    focusedAddBtn = null;
                    focusedEditBtn = null;
                    focusedDeleteBtn = null;
                    UpdatePosting();
                    break;
                default:
                    focusedAddBtn = null;
                    focusedEditBtn = null;
                    focusedDeleteBtn = null;
                    break;
            }
        }

        private void BtnAddEntry_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddEditEntry();
                if (window.ShowDialog() == DialogResult.OK)
                {
                    Dialog.Info("Приход успешно создан");
                    UpdateEntryGrid();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEditEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (myUser.user_role != 3)
                {
                    var aaa = (Entry)dgvEntry.SelectedRows[0].DataBoundItem;

                    var window = new AddEditEntry(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateEntryGrid();
                }
                else
                    Dialog.Error("Недостаточно прав доступа!");
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали прихода!");
                return;
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                if (dgvEntry.SelectedRows.Count == 0)
                {
                    Dialog.Error("Вы не выбрали прихода");
                    return;
                }
                var aaa = (Entry)dgvEntry.SelectedRows[0].DataBoundItem;

                string string_text = string.Format(
                    "Вы действительно хотите удалить '{0}'?\n" +
                    "Восстановить его будет невозможно!", aaa.GetEntryName);

                if (MessageBox.Show(
                    string_text, "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DeleteEntry(aaa);
                    UpdateEntryGrid();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void DeleteEntry(Entry myEntry)
        {
            if (await new Entry().OnDeleteAsync(myEntry))
            {
                await BalanceSystem.Instance.DeleteEntryOperations(myEntry.entry_id);
                var vendor = await new Vendor().OnSelectAsync(myEntry.entry_vendor);
                if (vendor != null)
                {
                    vendor.vendor_debt -= myEntry.GetEntryDebt;
                    await new Vendor().OnUpdateAsync(vendor);
                }
            }
            var details = await new EntryDetails().OnSelectDetailsByEntryAsync(myEntry.entry_id);
            var stocks = await new Stock().OnLoadAsync();

            Stock stock;
            double count;
            EntryDetails dbDetail = new EntryDetails();
            foreach (EntryDetails i in details)
            {
                stock = stocks.FirstOrDefault(u => u.stock_id == i.details_stock);
                if (stock != null)
                {
                    count = i.details_count;
                    stock.stock_count -= count;
                    await stock.OnUpdateAsync(stock);
                }
                await dbDetail.OnDeleteDetailsAsync(i.details_id);
            }
        }

        private async void UpdateEntryGrid()
        {
            BindingSource bs = new BindingSource();

            var entries = await new Entry().OnLoadAsync();
            var users = await new User().OnAllUserAsync();
            var vendors = await new Vendor().OnLoadAsync();

            foreach (var i in entries)
            {
                i.vendor = vendors.FirstOrDefault(u => u.vendor_id == i.entry_vendor);
                i.user = users.FirstOrDefault(u => u.user_id == i.entry_user);
                bs.Add(i);
            }
            dgvEntry.DataSource = bs;
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                if (dgvEntry.SelectedRows.Count == 0)
                {
                    Dialog.Error("Вы не выбрали прихода");
                    return;
                }

                var aaa = (Entry)dgvEntry.SelectedRows[0].DataBoundItem;
                var window = new PageEntryInfo(aaa);
                window.ShowDialog();
                UpdateEntryGrid();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void DgvEntry_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myUser.user_role != 3)
            {
                if (dgvEntry.SelectedRows.Count == 0)
                {
                    Dialog.Error("Вы не выбрали прихода");
                    return;
                }

                var aaa = (Entry)dgvEntry.SelectedRows[0].DataBoundItem;
                var window = new PageEntryInfo(aaa);
                window.ShowDialog();
                UpdateEntryGrid();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void btnBalanceInfo_Click(object sender, EventArgs e)
        {
            PageBalanceInfo pageBalanceInfo = new PageBalanceInfo();
            pageBalanceInfo.ShowDialog();
        }

        private void btnSpendsInfo_Click(object sender, EventArgs e)
        {
            PageSpendsInfo pageSpendsInfo = new PageSpendsInfo();
            pageSpendsInfo.ShowDialog();
        }

        private void btnIncomingsInfo_Click(object sender, EventArgs e)
        {
            PageIncomingsInfo pageIncomingsInfo = new PageIncomingsInfo();
            pageIncomingsInfo.ShowDialog();
        }

        private void btnMyDebtsInfo_Click(object sender, EventArgs e)
        {
            PageMyDebtsInfo pageMyDebtsInfo = new PageMyDebtsInfo();
            pageMyDebtsInfo.ShowDialog();
        }

        private void btnDebtsInfo_Click(object sender, EventArgs e)
        {
            PageDebtsInfo pageDebtsInfo = new PageDebtsInfo();
            pageDebtsInfo.ShowDialog();
        }

        private void btnStocksInfo_Click(object sender, EventArgs e)
        {
            PageStocksInfo pageStocksInfo = new PageStocksInfo();
            pageStocksInfo.ShowDialog();
        }

        private void btnCardsInfo_Click(object sender, EventArgs e)
        {
            PageCardsInfo pageCardsInfo = new PageCardsInfo();
            pageCardsInfo.ShowDialog();
        }

        private void btnCassaInfo_Click(object sender, EventArgs e)
        {
            PageCassaInfo pageCassaInfo = new PageCassaInfo();
            pageCassaInfo.ShowDialog();
        }

        private async void UpdateDashboard()
        {
            txtBalanceValue.Text = $"{await BalanceSystem.Instance.GetCurrentBalance():N2}";

            var cards = await new Card().OnLoadAsync();
            double cardsSum = cards.Sum(card => card.card_balance);
            txtCardsValue.Text = $"{cardsSum:N2}";

            var cassa = await new Cassa().OnLoadAsync();
            double cassaSum = cassa.Sum(c => c.cassa_money);
            txtCassaValue.Text = $"{cassaSum:N2}";

            var balanceLogs = await new TransactionLog().OnLoadBalanceTransactionsAsync();

            var incomesLog = balanceLogs.Where(log => log.transaction_type == EnumTransactionType.Доход);
            var incomeSum = incomesLog.Sum(log => log.transaction_amount);
            txtIncomingsValue.Text = $"{incomeSum:N2}";

            var spendsLog = balanceLogs.Where(log => log.transaction_type == EnumTransactionType.Расход);
            var spendsSum = spendsLog.Sum(log => log.transaction_amount);
            txtSpendsValue.Text = $"{spendsSum:N2}";

            var prixodLogs = await new EntryLog().OnLoadTransactionsAsync();
            var prixodSum = prixodLogs.Sum(log => log.transaction_amount);

            var currentSpendsLog = balanceLogs
                .Where(balanceLog => !prixodLogs
                .Any(prixodLog => prixodLog.transaction_id == balanceLog.transaction_id));

            //var currentSpendsLog = spendsLog.Where(log => log.money_source != "Приход" && !log.money_description.Contains("кассы")).ToList();
            var currentSpendsSum = spendsSum - prixodSum;

            var orders = await new Order().OnLoadAsync();
            double stocksSpendSum = 0.0;
            foreach (Order order in orders)
            {
                var complexData = await new DetailsProductComplex().OnLoadAllProdsAsync(order.order_main, true);
                foreach (var product in complexData.Products)
                {
                    stocksSpendSum += product.prod_start_price * product.prod_total;
                }
            }
            currentSpendsSum += stocksSpendSum;

            var allStocks = await new Stock().OnLoadAsync();
            var stocks = allStocks.Where(u => u.stock_count > 0);
            double stockSum = stocks.Sum(stock => stock.Sum);
            txtStocksValue.Text = $"{stockSum:N2}";

            var vendors = await new Vendor().OnLoadAsync();
            double myDebtSum = vendors.Sum(vendor => vendor.vendor_debt);
            txtMyDebtsValue.Text = $"{myDebtSum:N2}";

            var debtors = await new Client().OnLoadAsync();
            double debtSum = debtors.Sum(client => client.client_debt);
            txtDebtsValue.Text = $"{debtSum:N2}";

            int cashPaymentCount = orders.Where(or => or.order_payment == (int)EnumPaymentType.Money).Count();
            txtOrdersCashPayValue.Text = $"{cashPaymentCount}";
            int cardPaymentCount = orders.Where(or => or.order_payment == (int)EnumPaymentType.Card).Count();
            txtOrdersCardPayValue.Text = $"{cardPaymentCount}";
            int mixedPaymentCount = orders.Where(or => or.order_payment == (int)EnumPaymentType.Mixed).Count();
            txtOrdersMixPayValue.Text = $"{mixedPaymentCount}";

            int ordersCount = orders.Count();
            txtOrdersValue.Text = $"{ordersCount}";

            var allSalesSum = orders.Sum(order => order.order_price);
            txtSalesSumValue.Text = $"{allSalesSum:N2}";

            var details = await new OrderDetails().OnLoadAsync();
            int detailsCount = details.Count();
            txtSalesValue.Text = $"{detailsCount}";

            var deliveries = orders.Where(order => order.order_delivery == (int)EnumOrderType.Delivery);
            int deliveryCounts = deliveries.Count();
            txtDeliveryValue.Text = $"{deliveryCounts}";

            var guests = orders.Sum(order => order.order_guest);
            txtGuestsValue.Text = $"{guests}";

            var allSalaryLog = await new SalaryLog().OnLoadTransactionsAsync();
            var salarySum = allSalaryLog.Where(
                s => s.transaction_salary_type == EnumSalaryLogType.Оплата ||
                s.transaction_salary_type == EnumSalaryLogType.Предоплата)
                .Sum(s => s.transaction_amount);
            txtSalaryValue.Text = $"{salarySum:N2}";

            var currentDate = DateTime.Now.Date;
            var currentMonth = DateTime.Now.Month;

            List<Order> ordersToday = orders
                .Where(order => DateTime.Parse(order.order_date).Date == currentDate)
                .ToList();

            // Фильтруем заказы по текущему месяцу
            List<Order> ordersThisMonth = orders
                .Where(order => DateTime.Parse(order.order_date).Month == currentMonth)
                .ToList();

            List<TransactionLog> logsToday = currentSpendsLog
                .Where(log => DateTime.Parse(log.transaction_date).Date == currentDate)
                .ToList();

            List<TransactionLog> logsThisMonth = currentSpendsLog
                .Where(log => DateTime.Parse(log.transaction_date).Month == currentMonth)
                .ToList();

            //var currentIncomeSum = allSalesSum + incomeSum;
            var allRevenue = allSalesSum - currentSpendsSum;
            var allMargin = allSalesSum != 0 ? (allRevenue / allSalesSum) * 100 : 0;

            txtRevenueValue.Text = $"{allRevenue:N2}";
            txtMarginValue.Text = $"{allMargin:N2}%";

            var todaySalesSum = ordersToday.Sum(order => order.order_price);
            var todaySpendsSum = logsToday.Sum(log => log.transaction_amount);

            var todayRevenue = todaySalesSum - todaySpendsSum;
            var todayMargin = todaySalesSum != 0 ? (todayRevenue / todaySalesSum) * 100 : 0;

            txtTodayRevenueValue.Text = $"{todayRevenue:N2}";
            txtTodayMarginValue.Text = $"{todayMargin:N2}%";


            var monthSalesSum = ordersThisMonth.Sum(order => order.order_price);
            var monthSpendsSum = logsThisMonth.Sum(log => log.transaction_amount);

            var monthRevenue = monthSalesSum - monthSpendsSum;
            var monthMargin = monthSalesSum != 0 ? (monthRevenue / monthSalesSum) * 100 : 0;

            txtMonthRevenueValue.Text = $"{monthRevenue:N2}";
            txtMonthMarginValue.Text = $"{monthMargin:N2}%";
        }

        private void btnMarginInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Маржа чистой прибыли = (Чистая прибыль / Выручка от ресторана) * 100% \r\n*Этот показатель помогает оценить, какая доля дохода остается у компании после покрытия всех затрат.\r\n",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRevenueInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Чистая прибыль = Выручка от ресторана - Все расходы\r\n*Это ключевой показатель эффективности бизнеса.\r\n",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                focusedAddBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                focusedDeleteBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.E || keyData == Keys.B || keyData == Keys.F2)
            {
                focusedEditBtn?.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timerDashboard_Tick(object sender, EventArgs e)
        {
            timerDashboard.Stop();
            UpdateDashboard();
        }
    }
}
