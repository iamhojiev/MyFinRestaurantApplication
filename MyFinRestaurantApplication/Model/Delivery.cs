using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using ManagerApplication.Database;
using ManagerApplication.Properties;

namespace ManagerApplication.Model
{
    public class Delivery
    {
        public int delivery_id { get; set; }
        public string delivery_name { get; set; }
        public string delivery_desc { get; set; }
        public double delivery_price { get; set; }

        private static RestClient client = new RestClient(DataSQL.URL + @"/delivery");

        public async Task<List<Delivery>> OnLoadAsync()
        {
            var req = new RestRequest("/load_delivery.php");

            var res = await client.GetAsync(req);

            var source = DataSQL.Deserialize<Delivery>(res.Content);

            return source;
        }

        public async Task<bool> OnDeleteAsync(Delivery delivery)
        {
            var req = new RestRequest("/delete_delivery.php")
                .AddParameter("delivery_id", delivery.delivery_id);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                await Debug.DebugInsertAsync($"Удалил тип доставки:\nНаименование: {delivery.delivery_name}", Settings.Default.user_id);
                return true;
            }
            return false;
        }

        public async Task<bool> OnUpdateAsync(Delivery delivery)
        {
            RestRequest req = new RestRequest("/update_delivery.php")
                .AddParameter("delivery_name", delivery.delivery_name)
                .AddParameter("delivery_desc", delivery.delivery_desc)
                .AddParameter("delivery_price", DataSQL.ConvertDouble(delivery.delivery_price))
                .AddParameter("delivery_id", delivery.delivery_id);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                var str = $"Изменил данные доставки:\nНаименование: {delivery.delivery_name}\nСтоимость:{delivery.delivery_price}";
                await Debug.DebugInsertAsync(str, Settings.Default.user_id);
                return true;
            }
            return false;
        }

        public async Task<bool> OnInsertAsync(Delivery delivery)
        {
            RestRequest req = new RestRequest("/insert_delivery.php")
                .AddParameter("delivery_name", delivery.delivery_name)
                .AddParameter("delivery_price", DataSQL.ConvertDouble(delivery.delivery_price));

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                var str = $"Добавил новую доставку:\nНаименование: {delivery.delivery_name}";
                await Debug.DebugInsertAsync(str, Settings.Default.user_id);
                return true;
            }
            return false;
        }
    }
}
