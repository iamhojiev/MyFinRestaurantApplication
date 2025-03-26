using ManagerApplication.Database;
using ManagerApplication.Helper;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerApplication.Model
{
    public class Shift
    {
        public int shift_id { get; set; }
        public string shift_date_open { get; set; }
        public string shift_date_close { get; set; }
        public int shift_user_id { get; set; }
        public int shift_status { get; set; }
        public User user { get; set; }
        public List<Order> orders { get; set; }
        public string GetUserName { get { return user?.user_name; } }

        private static RestClient client = new RestClient(DataSQL.URL + @"/shift");

        public string OnStatus
        {
            get
            {
                return shift_status == (int)EnumShift.Close ? shift_date_close : "Смена открыта";
            }
        }

        private List<Order> shiftOrders;

        public int OrdersCount => shiftOrders?.Count ?? 0;
        public double OrdersSum => shiftOrders?.Sum(u => u.order_price) ?? 0;

        public async Task LoadOrdersAsync()
        {
            shiftOrders = await new Order().OnLoadShiftAsync(shift_id);
        }

        public async Task<List<Shift>> OnLoadAsync()
        {
            var req = new RestRequest("/load_shift.php");

            var res = await client.GetAsync(req);

            var source = DataSQL.Deserialize<Shift>(res.Content);

            return source;
        }

    }
}
