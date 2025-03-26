using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace ManagerApplication.Model
{
    [FirestoreData]
    public class FireStock
    {

        [FirestoreProperty]
        public string StockName { get; set; }

        [FirestoreProperty]
        public double StockCount { get; set; }

        [FirestoreProperty]
        public double StockPrice { get; set; }

        [FirestoreProperty]
        public string StockCategory { get; set; }

        [FirestoreProperty]
        public string StockType { get; set; }

        [FirestoreProperty]
        public string StockLocation { get; set; }


        // Конструктор без параметров (обязателен для Firestore)
        public FireStock()
        {
        }

        // Конструктор для преобразования Stock в FireStock
        public FireStock(Stock stock)
        {
            StockName = stock.stock_name;
            StockCount = stock.stock_count;
            StockPrice = stock.stock_price;
            StockLocation = "-";
            StockCategory = stock.stockCategory?.st_cat_name ?? "N/A";
            StockType = stock.type?.type_name ?? "N/A";
        }

        // Статический метод для преобразования List<Stock> в List<FireStock>
        public static List<FireStock> ConvertToFireStocks(List<Stock> stocks)
        {
            List<FireStock> fireStocks = new List<FireStock>();

            foreach (var stock in stocks)
            {
                fireStocks.Add(new FireStock(stock));
            }

            return fireStocks;
        }
    }
}
