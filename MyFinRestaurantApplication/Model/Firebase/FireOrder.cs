using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace ManagerApplication.Model
{
    [FirestoreData]
    public class FireOrder
    {
        [FirestoreProperty]
        public int OrderId { get; set; } // Идентификатор заказа / Шиносномаи фармоиш

        [FirestoreProperty]
        public string OrderLocation { get; set; } // Локация заказа / Ҷойгиршавии фармоиш

        [FirestoreProperty]
        public string OrderTable { get; set; } // Название стола / Номи миз

        [FirestoreProperty]
        public string OrderHall { get; set; } // Название зала / Номи толор

        [FirestoreProperty]
        public string OrderUser { get; set; } // Имя пользователя, оформившего заказ / Номи корбар, ки фармоиш додааст

        [FirestoreProperty]
        public double OrderPrice { get; set; } // Общая стоимость заказа / Арзиши умумии фармоиш

        [FirestoreProperty]
        public double OrderDiscount { get; set; } // Скидка на заказ / Тахфиф ба фармоиш

        [FirestoreProperty]
        public double OrderHallPrice { get; set; } // Стоимость зала / Арзиши толор

        [FirestoreProperty]
        public double OrderWaiterPrice { get; set; } // Оплата официанта / Арзиши хизматрасонии пешхизмат

        [FirestoreProperty]
        public string OrderPayment { get; set; } // Способ оплаты / Тарзи пардохт

        [FirestoreProperty]
        public string OrderDate { get; set; } // Дата создания заказа / Санаи фармоиш

        [FirestoreProperty]
        public string OrderCloseDate { get; set; } // Дата закрытия заказа / Санаи анҷоми фармоиш

        [FirestoreProperty]
        public int OrderStatus { get; set; } // Статус заказа / Вазъи фармоиш

        [FirestoreProperty]
        public List<FireOrderDetail> OrderDetails { get; set; } // Список деталей заказа / Рӯйхати ҷузъиёти фармоиш

        public FireOrder()
        {
            OrderDetails = new List<FireOrderDetail>();
        }

        public FireOrder(Order order) : this()
        {
            OrderId = order.order_id;
            OrderLocation = "Zarafshon";
            OrderTable = order.tables?.table_name ?? "N/A";
            OrderHall = order.tables?.hall.hall_name ?? "N/A";
            OrderUser = order.user?.user_name ?? "N/A";
            OrderPrice = order.order_price;
            OrderDiscount = order.order_discount;
            OrderHallPrice = order.order_price_hall;
            OrderWaiterPrice = order.order_price_waiter;
            OrderPayment = order.OrderPayment;
            OrderDate = order.order_date;
            OrderCloseDate = order.order_close_date;
            OrderStatus = order.order_status;

            foreach (var detail in order.orderDetails)
            {
                OrderDetails.Add(new FireOrderDetail(detail));
            }
        }

        public static List<FireOrder> ConvertToFireOrders(List<Order> orders)
        {
            List<FireOrder> fireOrders = new List<FireOrder>();

            foreach (var order in orders)
            {
                fireOrders.Add(new FireOrder(order));
            }

            return fireOrders;
        }
    }

    [FirestoreData]
    public class FireOrderDetail
    {
        [FirestoreProperty]
        public string DetailProd { get; set; } // Название продукта / Номи маҳсулот

        [FirestoreProperty]
        public double DetailCount { get; set; } // Количество продукта / Миқдори маҳсулот

        [FirestoreProperty]
        public double DetailPrice { get; set; } // Цена продукта / Нархи маҳсулот

        [FirestoreProperty]
        public double DetailStartPrice { get; set; } // Начальная цена продукта / Нархи ибтидоии маҳсулот

        public FireOrderDetail()
        {
        }

        public FireOrderDetail(OrderDetails detail)
        {
            DetailProd = detail.product?.prod_name ?? "N/A";
            DetailCount = detail.details_count;
            DetailPrice = detail.product?.prod_price ?? 0;
            DetailStartPrice = detail.product?.prod_start_price ?? 0;
        }
    }
}
