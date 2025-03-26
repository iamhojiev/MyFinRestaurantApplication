using ManagerApplication.Database;
using RestSharp;
using System.Collections.Generic;
using ManagerApplication.Properties;
using System.Threading.Tasks;

namespace ManagerApplication.Model
{
    public class Kitchen
    {
        public int kitchen_id { get; set; }
        public string kitchen_name { get; set; }
        public string kitchen_printer { get; set; }

        public List<Product> product { get; set; }

        private static RestClient client = new RestClient(DataSQL.URL + @"/kitchen");

        public async Task<List<Kitchen>> OnLoadAsync()
        {
            var req = new RestRequest("/load_kitchen.php");

            var res = await client.GetAsync(req);

            var source = DataSQL.Deserialize<Kitchen>(res.Content);

            return source;
        }

        public async Task<bool> OnDeleteAsync(Kitchen kitchen)
        {
            var req = new RestRequest("/delete_kitchen.php")
                .AddParameter("kitchen_id", kitchen.kitchen_id);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                await Debug.DebugInsertAsync($"Удалил кухню:\nНаименование:{kitchen.kitchen_name}", Settings.Default.user_id);
                return true;
            }
            return false;
        }

        public async Task<bool> OnUpdateAsync(Kitchen kitchen)
        {
            RestRequest req = new RestRequest("/update_kitchen.php")
                .AddParameter("kitchen_id", kitchen.kitchen_id)
                .AddParameter("kitchen_name", kitchen.kitchen_name)
                .AddParameter("kitchen_printer", kitchen.kitchen_printer);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                await Debug.DebugInsertAsync($"Изменил данные кухни:\nНаименование:{kitchen.kitchen_name}\nПринтер:{kitchen.kitchen_printer}", Settings.Default.user_id);
                return true;
            }
            return false;
        }

        public async Task<bool> OnInsertAsync(Kitchen kitchen)
        {
            var req = new RestRequest("/insert_kitchen.php")
                .AddParameter("kitchen_name", kitchen.kitchen_name)
                .AddParameter("kitchen_printer", kitchen.kitchen_printer);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                await Debug.DebugInsertAsync($"Добавил новую кухню:\nНаименование:{kitchen.kitchen_name}\nПринтер:{kitchen.kitchen_printer}", Settings.Default.user_id);
                return true;
            }
            return false;
        }
    }
}
