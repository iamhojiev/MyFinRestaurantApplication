using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagerApplication.UserControls
{
    public partial class UC_Product : UserControl
    {
        private User myUser;

        public UC_Product()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            InitForm();
            UpdateGrid();
        }

        private async void InitForm()
        {
            List<Category> category = new List<Category>
            {
                new Category(){ category_name = "Все категории", },
            };
            category.AddRange(await new Category().OnLoadAsync());

            List<Kitchen> kitchen = new List<Kitchen>
            {
                new Kitchen(){ kitchen_name = "Все кухни", },
            };
            kitchen.AddRange( await new Kitchen().OnLoadAsync());

            cmbCategory.ValueMember = "category_id";
            cmbCategory.DisplayMember = "category_name";
            cmbKitchen.ValueMember = "kitchen_id";
            cmbKitchen.DisplayMember = "kitchen_name";
            cmbCategory.DataSource = category;
            cmbKitchen.DataSource = kitchen;

            cmbCategory.SelectedIndex = 0;
            cmbKitchen.SelectedIndex = 0;


            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);
        }

        private void BtnExcel_Click(object sender, EventArgs e)
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

                    newFile.Write("Наименование" + ";");
                    newFile.Write("Стоимость" + ";");
                    newFile.Write("Тип" + ";");

                    newFile.WriteLine();

                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        if (row.DataBoundItem is Product a)
                        {
                            newFile.Write(a.prod_name + ";");
                            newFile.Write(a.prod_price + ";");
                            newFile.Write(a.type.type_name + ";");

                            newFile.WriteLine();
                        }
                    }

                    newFile.Close();
                    Dialog.Info("Файл успешно создан!");
                }
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Product)dgvMain.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.prod_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Product().OnDeleteAsync(aaa))
                        {
                            await new Recipe().OnDeleteProdAsync(aaa.prod_id);
                            UpdateGrid();
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


        public async void UpdateGrid()
        {
            BindingSource bs = new BindingSource();

            var products = await new Product().OnLoadAsync();
            var types = await new Model.Type().OnLoadAsync();
            var category = await new Category().OnLoadAsync();
            var kitchen = await new Kitchen().OnLoadAsync();

            var myCategory = cmbCategory.SelectedIndex;
            var myKitchen = cmbKitchen.SelectedIndex;

            var myCategorySelectedIndex = cmbCategory.SelectedIndex;
            var myKitchenSelectedIndex = cmbKitchen.SelectedIndex;

            if (myCategorySelectedIndex != 0)
            {
                var myCategoryValue = Convert.ToInt32(cmbCategory.SelectedValue);
                products = products.Where(u => u.prod_category == myCategoryValue).ToList();
            }

            if (myKitchenSelectedIndex != 0)
            {
                var myKitchenValue = Convert.ToInt32(cmbKitchen.SelectedValue);
                products = products.Where(u => u.prod_kitchen == myKitchenValue).ToList();
            }

            if (txtFind.Text != "")
                products = products.Where(u => u.prod_name.ToUpper().Contains(txtFind.Text.ToString().ToUpper())).ToList();

            foreach (var i in products)
            {
                i.type = types.Where(u => u.type_id == i.prod_value).FirstOrDefault();
                i.category = category.Where(u => u.category_id == i.prod_category).FirstOrDefault();
                i.kitchen = kitchen.Where(u => u.kitchen_id == i.prod_kitchen).FirstOrDefault();
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var aaa = (Product)dgvMain.SelectedRows[0].DataBoundItem;
                var window = new SeeRecipe(aaa);
                window.ShowDialog();
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали товар!");
                return;
            }
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var categories = await new Category().OnLoadAsync();
                if (categories.Count <= 0)
                {
                    //Добавление блюда возможно только после создания категории.
                    Dialog.Error("Перейдите в раздел \"Справочник\" и добавьте первую запись в разделе \"Категория блюд\"");
                    return;
                }
                var kitchen = await new Kitchen().OnLoadAsync();
                if (kitchen.Count <= 0)
                {
                    Dialog.Error("Перейдите в раздел \"Столики/Залы\" и добавьте первую запись в разделе \"Кухни\"");
                    return;
                }
                var window = new AddEditProduct();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateGrid();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var aaa = (Product)dgvMain.SelectedRows[0].DataBoundItem;

                var addEditProduct = new AddEditProduct(aaa);
                if (addEditProduct.ShowDialog() == DialogResult.OK)
                    UpdateGrid();
            }
            catch (ArgumentOutOfRangeException)
            {
                Dialog.Error("Вы не выбрали товар!");
                return;
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void cmbKitchen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                btnAddProd.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                btnDeleteProd.PerformClick();
                return true;
            }

            if (keyData == Keys.E || keyData == Keys.B || keyData == Keys.F2)
            {
                btnEditProduct.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
