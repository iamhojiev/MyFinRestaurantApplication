using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using ManagerApplication.Model;
using System.Collections.Generic;
using Type = ManagerApplication.Model.Type;

namespace ManagerApplication.Forms
{
    public partial class FormSyncToFirestore : Form
    {
        private FirestoreDb _firestoreDb;
        private List<Product> _products;
        private List<User> _users;
        private List<Hall> _halls;
        private List<Tables> _tables;
        private List<Type> _volumes;
        private List<StockCategory> _category;

        private readonly Order dbOrder = new Order();
        private readonly Stock dbStock = new Stock();
        private readonly OrderDetails dbDetails = new OrderDetails();

        public FormSyncToFirestore()
        {
            InitializeComponent();

            // Инициализация Firestore
            InitializeFirestore();
            InitializeDatas();
        }

        private void InitializeFirestore()
        {

        }

        // Метод для синхронизации заказов
        public async Task SyncOrdersAsync()
        {
            // Получаем несинхронизированные заказы из локальной базы
            var unsyncedOrders = await GetUnsyncedOrdersAsync();

            if (unsyncedOrders.Count > 0)
            {
                // Преобразуем List<Order> в List<FireOrder>
                List<FireOrder> fireOrders = FireOrder.ConvertToFireOrders(unsyncedOrders);

                foreach (var order in fireOrders)
                {
                    // Отправляем заказ в Firestore
                    bool isSuccess = await SendOrderToFirestoreAsync(order);

                    if (isSuccess)
                    {
                        // Обновляем статус is_synced в локальной базе
                        await dbOrder.UpdateAsSyncedAsync(order.OrderId);
                    }
                }
            }
        }

        // Получаем несинхронизированные заказы
        private async Task<List<Order>> GetUnsyncedOrdersAsync()
        {
            // Здесь используйте ваш метод для получения заказов с is_synced = 0
            var unsyncedOrders = await dbOrder.OnLoadUnsyncedAsync();

            foreach (Order order in unsyncedOrders)
            {
                order.user = _users.FirstOrDefault(u => u.user_id == order.order_user);
                order.tables = _tables.FirstOrDefault(u => u.table_id == order.order_table);
                order.tables.hall = _halls.FirstOrDefault(u => u.hall_id == order.tables.table_hall_id);

                order.orderDetails = await dbDetails.OnSelectOrderDetailsAsync(order.order_id);
                foreach (OrderDetails detail in order.orderDetails)
                {
                    detail.product = _products.FirstOrDefault(p => p.prod_id == detail.details_prod);
                }
            }
            return unsyncedOrders;
        }

        // Отправляем заказ в Firestore
        private async Task<bool> SendOrderToFirestoreAsync(FireOrder fireOrder)
        {
            try
            {
                // Создаем документ в Firestore с автоматически сгенерированным идентификатором
                DocumentReference docRef = _firestoreDb.Collection("orders").Document();
                await docRef.SetAsync(fireOrder);

                return true;
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Ошибка при отправке заказа: {ex.Message}");
                return false;
            }
        }


        // Метод для синхронизации Stock
        public async Task SyncStocksAsync()
        {
            var updatedStocks = await GetUnsyncedStocksAsync();

            if (updatedStocks.Count > 0)
            {
                List<FireStock> fireStocks = FireStock.ConvertToFireStocks(updatedStocks);

                foreach (var stock in fireStocks)
                {
                    bool isSuccess = await SendStockToFirestoreAsync(stock);
                    if (isSuccess)
                    {
                        // await dbStock.UpdateAsSyncedAsync(stock.StockId);
                    }
                }
            }
        }

        private async Task<List<Stock>> GetUnsyncedStocksAsync()
        {
            var stocks = await new Stock().OnLoadAsync();

            foreach (var i in stocks)
            {
                i.type = _volumes.FirstOrDefault(u => u.type_id == i.stock_value);
                i.stockCategory = _category.FirstOrDefault(u => u.st_cat_id == i.stock_category);
            }
            return stocks;
        }

        // Отправка данных в Firestore
        private async Task<bool> SendStockToFirestoreAsync(FireStock fireStock)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection("stocks").Document();
                await docRef.SetAsync(fireStock);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке товара {fireStock.StockName}: {ex.Message}");
                return false;
            }
        }

    //    private async void SendLockDatas()
    //    {
    //        // Первый список для локации "Airoport"
    //        List<FireStock> airportStocks = new List<FireStock>
    //{
    //    new FireStock {StockName = "Картошка", StockCount = 100, StockPrice = 50, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Морковь", StockCount = 150, StockPrice = 30, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Лук", StockCount = 200, StockPrice = 25, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Чеснок", StockCount = 80, StockPrice = 40, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Помидоры", StockCount = 120, StockPrice = 60, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Огурцы", StockCount = 90, StockPrice = 55, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Перец", StockCount = 110, StockPrice = 70, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Капуста", StockCount = 130, StockPrice = 35, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Баклажаны", StockCount = 70, StockPrice = 80, StockCategory = "Ингредиенты", StockType =  "шт.", StockLocation = "Airoport" },
    //    new FireStock { StockName = "Зелёный лук", StockCount = 160, StockPrice = 45, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Airoport" }
    //};

    //        foreach (var stock in airportStocks)
    //        {
    //            await SendStockToFirestoreAsync(stock);
    //        }

    //        // Второй список для локации "Zarafshon"
    //        List<FireStock> zarafshonStocks = new List<FireStock>
    //{
    //    new FireStock { StockName = "Картошка", StockCount = 120, StockPrice = 50, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Морковь", StockCount = 140, StockPrice = 30, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Лук", StockCount = 160, StockPrice = 25, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Чеснок", StockCount = 70, StockPrice = 40, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Помидоры", StockCount = 130, StockPrice = 60, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Огурцы", StockCount = 100, StockPrice = 55, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Перец", StockCount = 115, StockPrice = 70, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Капуста", StockCount = 125, StockPrice = 35, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Баклажаны", StockCount = 85, StockPrice = 80, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" },
    //    new FireStock { StockName = "Зелёный лук", StockCount = 150, StockPrice = 45, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Zarafshon" }
    //};

    //        foreach (var stock in zarafshonStocks)
    //        {
    //            await SendStockToFirestoreAsync(stock);
    //        }

    //        // Третий список для локации "Vodanasos"
    //        List<FireStock> vodanasosStocks = new List<FireStock>
    //{
    //    new FireStock { StockName = "Картошка", StockCount = 110, StockPrice = 50, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Морковь", StockCount = 180, StockPrice = 30, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Лук", StockCount = 170, StockPrice = 25, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Чеснок", StockCount = 90, StockPrice = 40, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Помидоры", StockCount = 140, StockPrice = 60, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Огурцы", StockCount = 130, StockPrice = 55, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Перец", StockCount = 105, StockPrice = 70, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Капуста", StockCount = 135, StockPrice = 35, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Баклажаны", StockCount = 75, StockPrice = 80, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" },
    //    new FireStock { StockName = "Зелёный лук", StockCount = 145, StockPrice = 45, StockCategory = "Ингредиенты", StockType = "шт.", StockLocation = "Vodanasos" }
    //};

    //        foreach (var stock in vodanasosStocks)
    //        {
    //            await SendStockToFirestoreAsync(stock);
    //        }
    //    }


        private async void InitializeDatas()
        {
            _products = await new Product().OnLoadAsync();
            _users = await new User().OnAllUserAsync();
            _halls = await new Hall().OnLoadAllAsync();
            _tables = await new Tables().OnLoadAsync();
            _volumes = await new Type().OnLoadAsync();
            _category = await new StockCategory().OnLoadAsync();

            timer1.Start();
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            await SyncOrdersAsync();
          //  await SyncStocksAsync();
            timer1.Start();
        }
    }
}
