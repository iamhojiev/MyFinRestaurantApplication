namespace ManagerApplication.Helper
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class NetworkHelper
    {
        public static async Task<bool> IsInternetAvailableAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Отправляем GET-запрос на Google
                    HttpResponseMessage response = await client.GetAsync("http://www.google.com");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
