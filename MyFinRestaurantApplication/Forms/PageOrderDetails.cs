using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagerApplication.Helper;
using ManagerApplication.Model;
using Type = ManagerApplication.Model.Type;

namespace ManagerApplication
{
    public partial class PageOrderDetails : Form
    {
        private readonly Order myOrder;
        private List<OrderDetails> myDetails;

        public PageOrderDetails(Order order)
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Text = $"Подробности заказа №{order.OrderNum}    " +
                   $"Официант: {order.GetUserName}    " +
                   $"Столик: {order.GetTableName}    " +
                   $"Зал: {order.GetHallName}    " +
                   $"Статус: {order.OrderStatus}    " +
                   $"Сумма: {order.order_price:N2}";
            myOrder = order;
            UpdateGrid();
        }

        private async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            myDetails = await new OrderDetails().OnSelectOrderDetailsAsync(myOrder.order_main);
            var prods = await new Product().OnLoadAsync();
            var types = await new Type().OnLoadAsync();

            foreach (var i in myDetails)
            {
                i.product = prods.Where(p => p.prod_id == i.details_prod).FirstOrDefault();
                i.product.type = types.Where(p => p.type_id == i.product.prod_value).FirstOrDefault();
                i.product.prod_total = i.details_count;
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 0)
            {
                Dialog.Error("Вы ничего не выбрали.");
                return;
            }

            var selectedDetail = (OrderDetails)dgvMain.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show(
                    $"Вы действительно хотите удалить '{selectedDetail.product.prod_name}' из заказа?", "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (await new OrderDetails().OnDeleteDetailCascadeAsync(selectedDetail.details_id))
            {
                Dialog.Info($"'{selectedDetail.product.prod_name}' успешно удален из заказа.");
                UpdateGrid();
            }
        }

        //private async void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    if (dgvMain.SelectedRows.Count == 0)
        //    {
        //        Dialog.Error("Вы ничего не выбрали.");
        //        return;
        //    }

        //    var selectedDetail = (OrderDetails)dgvMain.SelectedRows[0].DataBoundItem;

        //    if (MessageBox.Show(
        //            $"Вы действительно хотите удалить '{selectedDetail.product.prod_name}' из заказа?", "Удаление",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question) != DialogResult.Yes)
        //    {
        //        return;
        //    }

        //    if (await new OrderDetails().OnDeleteAsync(selectedDetail.details_id))
        //    {
        //        await UpdateProductStockAsync(selectedDetail.product);

        //        double priceToDeduct = CalculatePriceToDeduct(selectedDetail);

        //        var orderTransaction = await new Transactions().OnSelectOrderTransactionAsync(myOrder.order_main);
        //        orderTransaction.transaction_amount -= priceToDeduct;
        //        await new Transactions().OnUpdateTransactionAsync(orderTransaction);

        //        await UpdateTransactionBalance(orderTransaction, priceToDeduct);

        //        myOrder.order_price -= priceToDeduct;
        //        if (await new Order().OnUpdateAsync(myOrder))
        //        {
        //            Dialog.Info($"'{selectedDetail.product.prod_name}' успешно удален из заказа.");
        //            UpdateGrid();
        //        }
        //    }
        //}

        //private async Task UpdateTransactionBalance(Transactions orderTransaction, double priceToDeduct)
        //{
        //    if (orderTransaction.transaction_payment == (int)EnumPayment.Card)
        //    {
        //        var card = await new Card().OnSelectAsync(orderTransaction.transaction_cassa);
        //        card.card_balance -= priceToDeduct;
        //        await new Card().OnUpdateAsync(card);
        //    }
        //    else
        //    {
        //        var orderCassa = await new Cassa().OnSelectCassaAsync(orderTransaction.transaction_cassa);
        //        orderCassa.cassa_money -= priceToDeduct;
        //        await new Cassa().OnUpdateAsync(orderCassa);
        //    }
        //}

        //private double CalculatePriceToDeduct(OrderDetails orderDetails)
        //{
        //    return orderDetails.product.prod_price * orderDetails.product.prod_total;
        //}

        //private async Task UpdateProductStockAsync(Product product)
        //{
        //    var stocks = await new Stock().OnLoadAsync();
        //    var dbStock = new Stock();

        //    product.recipe = await new Recipe().OnSelectProductAsync(product.prod_id);

        //    foreach (var i in product.recipe)
        //    {
        //        i.stock = stocks.Where(u => u.stock_id == i.recipe_stock).FirstOrDefault();

        //        i.stock.stock_count += (i.recipe_count * product.prod_total);

        //        await dbStock.OnUpdateCountAsync(i.stock);
        //    }
        //}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

