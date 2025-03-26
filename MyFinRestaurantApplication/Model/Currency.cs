﻿using System.Threading.Tasks;
using ManagerApplication.Database;
using ManagerApplication.Properties;
using RestSharp;

namespace ManagerApplication.Model
{
    public class Currency
    {
        public string currency_value { get; set; }

        private static RestClient client = new RestClient(DataSQL.URL + @"/currency");

        public async Task<string> OnGetCurrencyValueAsync()
        {
            var currency = await new Currency().OnLoadAsync();
            return currency.currency_value;
        }

        public async Task<Currency> OnLoadAsync()
        {
            var req = new RestRequest("/load_currency.php");

            var res = await client.GetAsync(req);

            var source = DataSQL.Deserialize<Currency>(res.Content);

            return source[0];
        }

        public async Task<bool> OnUpdateAsync(Currency currency)
        {
            RestRequest req = new RestRequest("/update_currency.php")
                .AddParameter("currency_value", currency.currency_value);

            var res = await client.PostAsync(req);

            if (res.IsSuccessful)
            {
                var str = $"Обновил валюту: {currency.currency_value}";
                await Debug.DebugInsertAsync(str, Settings.Default.user_id);
                return true;
            }
            return false;
        }
    }
}
