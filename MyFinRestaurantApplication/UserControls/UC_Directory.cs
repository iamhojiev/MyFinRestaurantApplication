using Guna.UI2.WinForms;
using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Type = ManagerApplication.Model.Type;

namespace ManagerApplication.UserControls
{
    public partial class UC_Directory : UserControl
    {
        private List<StockCategory> listCategory;
        private BindingSource bs = new BindingSource();
        private User myUser;
        private int cardIncomeId = 1;
        private int cassaIncomeId = 2;
        private int debtId = 1;
        private Guna2Button focusedAddBtn;
        private Guna2Button focusedDeleteBtn;
        private Guna2Button focusedEditBtn;

        public UC_Directory()
        {
            InitializeComponent();

            dgvProductCategory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvStockCategory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDelivery.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvIncomeCategory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSpendCategory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            metroTabControl1.SelectedIndex = 1;
            metroTabControl1.SelectedIndex = 0;
            InitForm();
        }

        private async void InitForm()
        {
            listCategory = await new StockCategory().OnLoadAsync();

            cmbCategory.Items.Add("Все категории");
            foreach (var i in listCategory)
                cmbCategory.Items.Add(i.st_cat_name);

            cmbCategory.SelectedIndex = 0;

            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);

            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            txtFind.TextChanged += TxtFind_TextChanged;
        }

        private async Task UpdateDelivery()
        {
            BindingSource bs = new BindingSource();

            var deliveries = await new Delivery().OnLoadAsync();

            foreach (var i in deliveries)
            {
                bs.Add(i);
            }
            dgvDelivery.DataSource = bs;
        }

        private async Task UpdateStocks()
        {
            bs.Clear();

            var stocks = await new Stock().OnLoadAsync();
            var volumes = await new Type().OnLoadAsync();
            var category = await new StockCategory().OnLoadAsync();

            var myCategory = cmbCategory.SelectedIndex;

            if (myCategory != 0 && myCategory != -1)
                stocks = stocks.Where(u => u.stock_category == listCategory[myCategory - 1].st_cat_id).ToList();

            if (txtFind.Text != "")
                stocks = stocks.Where(u => u.stock_name.ToUpper().Contains(txtFind.Text.ToString().ToUpper())).ToList();

            var priceTotal = 0.0;

            foreach (var i in stocks)
            {
                i.type = volumes.FirstOrDefault(u => u.type_id == i.stock_value);
                i.stockCategory = category.FirstOrDefault(u => u.st_cat_id == i.stock_category);
                bs.Add(i);

                priceTotal += i.Sum;
            }

            txtInfo.Text = string.Format("Количество наименований: {0}.   Общая стоимость: {1}", stocks.Count(), priceTotal);
            dgvStock.DataSource = bs;
        }

        private async void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateStocks();
        }

        private async void TxtFind_TextChanged(object sender, EventArgs e)
        {
            await UpdateStocks();
        }

        private async void BtnAddStock_Click(object sender, EventArgs e)
        {
            var window = new AddEditStock();
            if (window.ShowDialog() == DialogResult.OK)
            {
                await UpdateStocks();
            }
        }

        private async void BtnEditStock_Click(object sender, EventArgs e)
        {
            try
            {
                var aaa = (Stock)dgvStock.SelectedRows[0].DataBoundItem;

                var window = new AddEditStock(aaa);
                if (window.ShowDialog() == DialogResult.OK)
                {
                    await UpdateStocks();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали товар");
                return;
            }
        }

        private async void BtnPlusMinusStock_Click(object sender, EventArgs e)
        {
            var aaa = (Stock)dgvStock.SelectedRows[0].DataBoundItem;
            var window = new PagePosting(aaa);
            if (window.ShowDialog() == DialogResult.OK)
            {
               await UpdateStocks();
            }
        }

        private async void BtnDeleteStock_Click(object sender, EventArgs e)
        {
            try
            {
                var aaa = (Stock)dgvStock.SelectedRows[0].DataBoundItem;

                string string_text = string.Format(
                    "Вы действительно хотите удалить '{0}'?\n" +
                    "Восстановить его будет невозможно!", aaa.stock_name);

                if (MessageBox.Show(
                    string_text, "Удаление",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (await new Stock().OnDeleteAsync(aaa))
                    {
                       await UpdateStocks();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали товар");
                return;
            }
        }

        private async void ExcelExportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Excel (*.csv)|*.csv|Все файлы (*.*)|*.*"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (saveFile.FileName != "")
                {
                    StreamWriter newFile = new StreamWriter(saveFile.FileName, false, Encoding.UTF8);

                    var volumes = await new Type().OnLoadAsync();


                    newFile.Write("Наименование" + ";");
                    newFile.Write("Объем" + ";");
                    newFile.Write("Тип" + ";");

                    newFile.WriteLine();

                    foreach (DataGridViewRow row in dgvStock.Rows)
                    {
                        if (row.DataBoundItem is Stock a)
                        {
                            a.type = volumes.Where(u => u.type_id == a.stock_value).FirstOrDefault();

                            newFile.Write(a.stock_name + ";");
                            newFile.Write(a.stock_count + ";");
                            newFile.Write(a.type.type_name + ";");

                            newFile.WriteLine();
                        }
                    }
                    newFile.Close();
                    Dialog.Info("Файл успешно создан!");
                }
            }
        }

        private async void BtnAddStockCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var addStockCategory = new AddStockCategory();
                if (addStockCategory.ShowDialog() == DialogResult.OK)
                {
                  await  UpdateStockCategory();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnEditStockCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (StockCategory)dgvStockCategory.SelectedRows[0].DataBoundItem;

                    var addStockCategory = new AddStockCategory(aaa);
                    if (addStockCategory.ShowDialog() == DialogResult.OK)
                    {
                        await UpdateStockCategory();
                    }

                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteStockCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (StockCategory)dgvStockCategory.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.st_cat_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new StockCategory().OnDeleteAsync(aaa))
                        {
                            await UpdateStockCategory();
                        }

                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async Task UpdateStockCategory()
        {
            BindingSource bs = new BindingSource();

            var halls = await new StockCategory().OnLoadAsync();

            foreach (var i in halls)
            {
                bs.Add(i);
            }
            dgvStockCategory.DataSource = bs;

        }

        private async void BtnAddProductCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var addProductCategory = new AddProductCategory();
                if (addProductCategory.ShowDialog() == DialogResult.OK)
                {
                   await UpdateProductCategory();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteProductCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Category)dgvProductCategory.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.category_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Category().OnDeleteAsync(aaa))
                        {
                           await UpdateProductCategory();
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnEditProductCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Category)dgvProductCategory.SelectedRows[0].DataBoundItem;
                    var addProductCategory = new AddProductCategory(aaa);
                    if (addProductCategory.ShowDialog() == DialogResult.OK)
                    {
                       await UpdateProductCategory();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async Task UpdateProductCategory()
        {
            BindingSource bs = new BindingSource();

            var stockCategories = await new StockCategory().OnLoadAsync();
            var types = await new Type().OnLoadAsync();
            var categories = await new Category().OnLoadAsync();

            foreach (var i in categories)
            {
                i.st_category = stockCategories.Where(st => st.st_cat_id == i.category_stock).FirstOrDefault();
                i.type = types.Where(t => t.type_id == i.category_type).FirstOrDefault();
                bs.Add(i);
            }
            dgvProductCategory.DataSource = bs;
        }

        private async void BtnAddDelivery_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddDelivery();
                if (window.ShowDialog() == DialogResult.OK)
                {
                   await UpdateDelivery();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnEditDelivery_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Delivery)dgvDelivery.SelectedRows[0].DataBoundItem;

                    var window = new AddDelivery(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                    {
                       await UpdateDelivery();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали доставку!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteDelivery_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Hall)dgvDelivery.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.hall_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Hall().OnDeleteAsync(aaa))
                        {
                            await UpdateDelivery();
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали доставку!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
        }

        public async void UpdateCurrentTab()
        {
            var myTab = metroTabControl1.SelectedTab;

            switch (myTab.Name)
            {
                case "pageStocks":
                    focusedAddBtn = btnAddStock;
                    focusedEditBtn = btnEditStock;
                    focusedDeleteBtn = btnDeleteStock;
                    await UpdateStocks();
                    break;
                case "pageStockCat":
                    focusedAddBtn = null;
                    focusedEditBtn = btnEditStockCat;
                    focusedDeleteBtn = null;
                    await UpdateStockCategory();
                    break;
                case "pageProdCat":
                    focusedAddBtn = btnAddProdCat;
                    focusedEditBtn = btnEditProdCat;
                    focusedDeleteBtn = btnDeleteProdCat;
                    await UpdateProductCategory();
                    break;
                case "pageDelivery":
                    focusedAddBtn = btnAddDelivery;
                    focusedEditBtn = btnEditDelivery;
                    focusedDeleteBtn = btnDeleteDelivery;
                    await UpdateDelivery();
                    break;
                case "pageIncomeCategory":
                    focusedAddBtn = btnAddIncomeCategory;
                    focusedEditBtn = btnEditIncomeCategory;
                    focusedDeleteBtn = btnDeleteIncomeCategory;
                    await UpdateIncome();
                    break;
                case "pageSpendCategory":
                    focusedAddBtn = btnAddSpendCategory;
                    focusedEditBtn = btnEditSpendCategory;
                    focusedDeleteBtn = btnDeleteSpendCategory;
                    await UpdateSpend();
                    break;
            }
        }

        private void dgvStock_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (myUser.user_role != 3)
                {
                    var st = (Stock)dgvStock.SelectedRows[0].DataBoundItem;
                    var window = new PageStockEntrySpend(st);
                    window.Show();

                }
                else
                    Dialog.Error("Недостаточно прав доступа!");
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали прихода!");
                return;
            }
        }


        private async void btnAddIncomeCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddIncomeCategory();
                if (window.ShowDialog() == DialogResult.OK)
                {
                   await UpdateIncome();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void btnEditIncomeCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (IncomeCategory)dgvIncomeCategory.SelectedRows[0].DataBoundItem;
                    if (aaa.category_id == cardIncomeId || aaa.category_id == cassaIncomeId)
                    {
                        Dialog.Error("Данная категория не может быть изменена или удалена");
                        return;
                    }
                    var window = new AddIncomeCategory(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                    {
                       await UpdateIncome();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали категорию!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void btnDeleteIncomeCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (IncomeCategory)dgvIncomeCategory.SelectedRows[0].DataBoundItem;
                    if (aaa.category_id == cardIncomeId || aaa.category_id == cassaIncomeId)
                    {
                        Dialog.Error("Данная категория не может быть изменена или удалена");
                        return;
                    }
                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.category_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new IncomeCategory().OnDeleteAsync(aaa))
                        {
                           await UpdateIncome();
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async Task UpdateIncome()
        {
            BindingSource bs = new BindingSource();

            var categories = await new IncomeCategory().OnLoadAsync();

            foreach (var i in categories)
            {
                bs.Add(i);
            }
            dgvIncomeCategory.DataSource = bs;
        }

        private async void btnAddSpendCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddSpendCategory();
                if (window.ShowDialog() == DialogResult.OK)
                {
                   await UpdateSpend();
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void btnEditSpendCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (SpendCategory)dgvSpendCategory.SelectedRows[0].DataBoundItem;
                    if (aaa.category_id == debtId)
                    {
                        Dialog.Error("Данная категория не может быть изменена или удалена");
                        return;
                    }
                    var window = new AddSpendCategory(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                    {
                       await UpdateSpend();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали категорию!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void btnDeleteSpendCategory_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (SpendCategory)dgvSpendCategory.SelectedRows[0].DataBoundItem;
                    if (aaa.category_id == debtId)
                    {
                        Dialog.Error("Данная категория не может быть изменена или удалена");
                        return;
                    }
                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.category_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new SpendCategory().OnDeleteAsync(aaa.category_id))
                        {
                           await UpdateSpend();
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали элемент!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }


        private async Task UpdateSpend()
        {
            BindingSource bs = new BindingSource();

            var categories = await new SpendCategory().OnLoadAsync();

            foreach (var i in categories)
            {
                bs.Add(i);
            }
            dgvSpendCategory.DataSource = bs;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                focusedAddBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                focusedDeleteBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.E || keyData == Keys.B || keyData == Keys.F2)
            {
                focusedEditBtn?.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
