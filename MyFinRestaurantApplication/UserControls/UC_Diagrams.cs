using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace ManagerApplication.UserControls
{
    public partial class UC_Diagrams : UserControl
    {
        private Dictionary<string, double> workersData;
        private Dictionary<string, double> prodsData;
        private Dictionary<string, double> doxodData;
        private Dictionary<string, double> budgetData;
        private Dictionary<string, double> clientsData;

        private DateTime startDate, endDate;
        public UC_Diagrams()
        {
            InitializeComponent();
            InitYearComboBox();
            dgvTopWorkers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBudget.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTopProduct.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            startDate = DateTime.Today;
            endDate = DateTime.Today;
            metroTabControl.SelectedIndex = 0;
            cmbPeriod.SelectedIndex = 6;
        }

        private void InitYearComboBox()
        {
            int currentYear = DateTime.Now.Year;
            int[] years = Enumerable.Range(currentYear - 2, 5).ToArray();
            cmbDoxodYear.DataSource = cmbClientsYear.DataSource = years;
            cmbDoxodYear.SelectedItem = cmbClientsYear.SelectedItem = currentYear;
            cmbDoxodYear.SelectedIndexChanged += cmbDoxodYear_SelectedIndexChanged;
            cmbClientsYear.SelectedIndexChanged += cmbClientsYear_SelectedIndexChanged;
        }

        private async void SetBudgetData()
        {
            if (budgetData != null) budgetData.Clear();
            else budgetData = new Dictionary<string, double>();

            BindingSource bs = new BindingSource();

            List<TransactionLog> logs;

            if (rbDoxodBudget.Checked)
                logs = await new TransactionLog().OnLoadIncomeTransactionsAsync();
            else
                logs = await new TransactionLog().OnLoadSpendTransactionsAsync();

            foreach (var i in logs)
            {
                SetData(budgetData, i.transaction_description, i.transaction_amount);
            }
            bs.DataSource = budgetData.ToList();
            dgvBudget.DataSource = bs;
            dgvBudget.Columns[0].HeaderText = "Источник";
            dgvBudget.Columns[1].HeaderText = "Cумма";
            SetupBudgetChart();
        }

        private void SetupBudgetChart()
        {
            var creatorChart = new ChartCreator();
            string label;
            //string name;
            label = "Cумма";
            if (rbDoxodBudget.Checked)
            {
                txtBudgetInfo.Text = "Статистика по доходу";
            }
            else
            {
                txtBudgetInfo.Text = "Статистика по расходу";
            }

            creatorChart.ChartHorizontalBar(chartBudgetHBar, budgetData, label);
            creatorChart.ChartBar(chartBudgetBar, budgetData, label);
            creatorChart.ChartLine(chartBudgetLine, budgetData, label);

        }

        private void btnRefreshWorkersDiagram_Click(object sender, EventArgs e)
        {
            SetWorkersData();
        }

        private void CmbPeriodIndexChange(object sender, EventArgs e)
        {
            switch (cmbPeriod.SelectedIndex)
            {
                case 0:
                    startDate = endDate.AddDays(-1);
                    break;
                case 1:
                    startDate = endDate.AddDays(-7);
                    break;
                case 2:
                    startDate = endDate.AddMonths(-1);
                    break;
                case 3:
                    startDate = endDate.AddMonths(-3);
                    break;
                case 4:
                    startDate = endDate.AddMonths(-6);
                    break;
                case 5:
                    startDate = endDate.AddYears(-1);
                    break;
                case 6:
                    startDate = endDate.AddYears(-10);
                    break;
            }
            SetWorkersData();
        }

        private async void SetDoxodData()
        {
            if (doxodData != null) doxodData.Clear();
            else doxodData = new Dictionary<string, double>();

            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            foreach (var month in monthNames)
            {
                if (!string.IsNullOrEmpty(month))
                    doxodData[month] = 0;
            }

            var products = await new Product().OnLoadAsync();
            var orderDetails = new OrderDetails();

            var currentYear = Convert.ToInt32(cmbDoxodYear.SelectedItem);
            List<Order> orders = await new Order().OnLoadAsync();
            orders = orders.Where(order => DateTime.Parse(order.order_date).Year == currentYear).ToList();

            var revenue = rbRevenue.Checked;
            foreach (var i in orders)
            {
                var month = DateTime.Parse(i.order_date).ToString("MMMM");
                if (revenue)
                {
                    var rasxod = 0.0;
                    i.orderDetails = await orderDetails.OnSelectOrderDetailsAsync(i.order_main);
                    foreach (var q in i.orderDetails)
                    {
                        q.product = products.Where(u => u.prod_id == q.details_prod).FirstOrDefault();
                        rasxod += (q.product.prod_start_price * q.details_count);
                    }
                    SetData(doxodData, month, i.order_price - rasxod - i.order_price_waiter);
                }
                else
                {
                    SetData(doxodData, month, i.order_price);
                }
            }
            SetupDoxodChart();
        }

        private async void SetClientsData()
        {
            if (clientsData != null) clientsData.Clear();
            else clientsData = new Dictionary<string, double>();

            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            foreach (var month in monthNames)
            {
                if (!string.IsNullOrEmpty(month))
                    clientsData[month] = 0;
            }

            var currentYear = Convert.ToInt32(cmbClientsYear.SelectedItem);
            List<Order> orders = await new Order().OnLoadAsync();
            orders = orders.Where(or => or.order_delivery == (int)EnumOrderType.Default && DateTime.Parse(or.order_date).Year == currentYear)
            .ToList();

            foreach (var i in orders)
            {
                var month = DateTime.Parse(i.order_date).ToString("MMMM");
                SetData(clientsData, month, i.order_guest);
            }
            SetupClientsChart();
        }

        private async void SetProductsData()
        {
            if (prodsData != null) prodsData.Clear();
            else prodsData = new Dictionary<string, double>();

            var products = await new Product().OnLoadAsync();
            var kitchen = await new Kitchen().OnLoadAsync();
            BindingSource bs = new BindingSource();
            OrderDetails dbDetails = new OrderDetails();

            var byProduct = rbProduct.Checked;
            foreach (var product in products)
            {
                product.kitchen = kitchen.FirstOrDefault(u => u.kitchen_id == product.prod_kitchen);
                product.orderDetails = await dbDetails.OnSelectAllProductDetailsAsync(product.prod_id);

                if (byProduct)
                    SetData(prodsData, product.prod_name, product.ProdBuy);
                else
                    SetData(prodsData, product.kitchen.kitchen_name, product.ProdBuy);
            }

            // Сортируем данные по количеству продаж в порядке убывания
            var sortedData = prodsData.OrderByDescending(kvp => kvp.Value).ToList();

            bs.DataSource = sortedData;
            dgvTopProduct.DataSource = bs;
            dgvTopProduct.Columns[0].HeaderText = byProduct ? "Продукт" : "Кухня";
            dgvTopProduct.Columns[1].HeaderText = "Количество продаж";

            SetupProdsChart();
        }


        private async void SetWorkersData()
        {
            if (workersData != null) workersData.Clear();
            else workersData = new Dictionary<string, double>();

            // Получаем список всех пользователей.
            var users = await new User().OnAllUserAsync();
            BindingSource bs = new BindingSource();

            // Переменная для хранения даты смены.
            DateTime myDate;

            // Проверяем, какие данные нужно отобразить: сумму или количество.
            var bySumma = rbSumma.Checked;

            // Перебираем всех пользователей.
            foreach (var user in users)
            {
                // Инициализируем переменные для подсчета суммы и количества.
                double summa = 0.0;
                int check = 0;

                // Получаем все смены для пользователя.
                var shifts = await new Shift().OnLoadAsync();
                shifts = shifts.Where(s => s.shift_user_id == user.user_id).ToList();

                // Перебираем все смены и суммируем данные.
                foreach (var shift in shifts)
                {
                    myDate = DateTime.Parse(shift.shift_date_open);
                    if (myDate >= startDate && myDate < endDate.AddDays(1))
                    {
                        await shift.LoadOrdersAsync();
                        summa += shift.OrdersSum;
                        check += shift.OrdersCount;
                    }
                }

                UserMetrics userMetric = new UserMetrics()
                {
                    summa_total = summa,
                    check_count = check,
                    user_name = user.user_name
                };

                if (bySumma)
                    SetData(workersData, user.user_name, summa);
                else
                    SetData(workersData, user.user_name, check);
            }
            bs.DataSource = workersData.ToList();
            dgvTopWorkers.DataSource = bs;
            dgvTopWorkers.Columns[0].HeaderText = "Работник";
            dgvTopWorkers.Columns[1].HeaderText = bySumma ? "Сумма чеков" : "Количество чеков";
            SetupWorkersChart();
        }

        private void SetupDoxodChart()
        {
            var creatorChart = new ChartCreator();
            var label = "Cумма продаж";
            txtDoxodInfo.Text = "Месячные финансовые показатели";

            creatorChart.ChartLine(chartDoxodLine, doxodData, label);
            creatorChart.ChartBar(chartDoxodBar, doxodData, label);
            creatorChart.ChartHorizontalBar(chartDoxodHBar, doxodData, label);
        }

        private void SetupClientsChart()
        {
            var creatorChart = new ChartCreator();
            var label = "Количество гостей";
            txtClientsInfo.Text = "Статистика обслуживания клиентов";

            creatorChart.ChartLine(chartClientsLine, clientsData, label);
            creatorChart.ChartBar(chartClientsBar, clientsData, label);
            creatorChart.ChartHorizontalBar(chartClientsHBar, clientsData, label);
        }

        private void SetupProdsChart()
        {
            var creatorChart = new ChartCreator();
            string label;
            if (rbProduct.Checked)
            {
                txtProductsInfo.Text = "Статистика по блюдам";
                label = "Кол-во продаж";
            }
            else
            {
                txtProductsInfo.Text = "Статистика по кухням";
                label = "Кол-во продаж";
            }

            creatorChart.ChartLine(chartTopProdsLine, prodsData, label);
            creatorChart.ChartBar(chartTopProdsBar, prodsData, label);
            creatorChart.ChartHorizontalBar(chartTopProdsHBar, prodsData, label);
        }

        private void SetupWorkersChart()
        {
            var creatorChart = new ChartCreator();
            string label;
            if (rbSumma.Checked)
            {
                txtTopWorkersInfo.Text = "Статистика общая сумма чеков по официантам";
                label = "Cумма";
            }
            else
            {
                txtTopWorkersInfo.Text = "Статистика количество чеков по официантам";
                label = "Кол-во чеков";
            }
            creatorChart.ChartLine(chartWorkersLine, workersData, label);
            creatorChart.ChartBar(chartWorkersBar, workersData, label);
            creatorChart.ChartHorizontalBar(chartWorkersHBar, workersData, label);
        }

        private void SetData(Dictionary<string, double> dictionary, string key, double value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value; // Если ключ уже есть в словаре, добавляем к существующему значению
            }
            else
            {
                dictionary[key] = value;
            }
        }

        private void rbSumma_CheckedChanged(object sender, EventArgs e)
        {
            ChartWorkersReset();
            SetWorkersData();
        }

        private void ChartWorkersReset()
        {
            chartWorkersBar.Datasets.Clear();
            chartWorkersHBar.Datasets.Clear();
            chartWorkersLine.Datasets.Clear();
        }

        private void rbProduct_CheckedChanged(object sender, EventArgs e)
        {
            ChartProductsReset();
            SetProductsData();
        }

        private void ChartProductsReset()
        {
            chartTopProdsBar.Datasets.Clear();
            chartTopProdsHBar.Datasets.Clear();
            chartTopProdsLine.Datasets.Clear();
        }

        private void rbDoxod_CheckedChanged(object sender, EventArgs e)
        {
            ChartDoxodReset();
            SetDoxodData();
        }

        private void ChartDoxodReset()
        {
            chartDoxodBar.Datasets.Clear();
            chartDoxodHBar.Datasets.Clear();
            chartDoxodLine.Datasets.Clear();
        }

        private void rbBudget_CheckedChanged(object sender, EventArgs e)
        {
            ChartBudgetReset();
            SetBudgetData();
        }

        private void ChartBudgetReset()
        {
            chartBudgetBar.Datasets.Clear();
            chartBudgetHBar.Datasets.Clear();
            chartBudgetLine.Datasets.Clear();
        }

        private void btnRefreshProdDiagram_Click(object sender, EventArgs e)
        {
            ChartProductsReset();
            SetProductsData();
        }

        private void ChartClientsReset()
        {
            chartClientsBar.Datasets.Clear();
            chartClientsHBar.Datasets.Clear();
            chartClientsLine.Datasets.Clear();
        }

        private void btnRefreshClientsDiagram_Click(object sender, EventArgs e)
        {
            ChartClientsReset();
            SetClientsData();
        }

        private void btnRefreshBudgetDiagram_Click(object sender, EventArgs e)
        {
            ChartBudgetReset();
            SetBudgetData();
        }

        private void btnRefreshDoxodDiagram_Click(object sender, EventArgs e)
        {
            ChartDoxodReset();
            SetDoxodData();
        }

        private void cmbDoxodYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartDoxodReset();
            SetDoxodData();
        }

        private void cmbClientsYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartClientsReset();
            SetClientsData();
        }

        private void metroTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
        }

        public void UpdateCurrentTab()
        {
            var myTab = metroTabControl.SelectedTab;
            ResetAllCharts();
            switch (myTab.Name)
            {
                case "pageWorkers":
                    SetWorkersData();
                    break;
                case "pageProds":
                    SetProductsData();
                    break;
                case "pageDoxod":
                    SetDoxodData();
                    break;
                case "pageBudget":
                    SetBudgetData();
                    break;
                case "pageClients":
                    SetClientsData();
                    break;
            }
        }

        private void ResetAllCharts()
        {
            ChartWorkersReset();
            ChartProductsReset();
            ChartBudgetReset();
            ChartDoxodReset();
            ChartClientsReset();
        }
    }
}
