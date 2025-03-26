using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.DataBase;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace ManagerApplication
{
    public partial class AddProductCategory : Form
    {
        private Category _category;

        public AddProductCategory(Category category = null)
        {
            InitializeComponent();
            InitForm(category);
        }

        private async void InitForm(Category category)
        {
            var categories = await new StockCategory().OnLoadAsync();
            categories.Reverse();
            cmbStockCategory.DataSource = categories;
            cmbStockCategory.DisplayMember = "st_cat_name";
            cmbStockCategory.ValueMember = "st_cat_id";
            cmbStockCategory.SelectedIndex = 0;

            var volumes = await new Model.Type().OnLoadAsync();
            cmbType.DataSource = volumes;
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";
            cmbType.SelectedIndex = 0;

            if (category != null)
            {
                _category = category;
                txtTitle.Text = "Редактирование данных";
                txtName.Text = _category.category_name;
                cmbStockCategory.SelectedValue = Convert.ToInt32(category.category_stock);
                cmbType.SelectedValue = Convert.ToInt32(category.category_type);
                var imageName = _category.category_name + _category.category_id + ".png";
                var image = ImageServerIO.GetProductImage(imageName);
                if (image != null)
                    pictureBox1.Image = image;
            }

            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                try
                {
                    var stockCategory = Convert.ToInt32(cmbStockCategory.SelectedValue);
                    var typeId = Convert.ToInt32(cmbType.SelectedValue);
                    if (_category != null)
                    {
                        _category.category_name = txtName.Text;
                        _category.category_stock = stockCategory;
                        _category.category_type = typeId;
                        if (await new Category().OnUpdateAsync(_category))
                        {
                            var imageName = _category.category_name + _category.category_id;
                            ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName);
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var cat = new Category()
                        {
                            category_name = txtName.Text,
                            category_stock = stockCategory,
                            category_type = typeId,
                        };
                        if (await new Category().OnInsertAsync(cat))
                        {
                            var lastCat = await new Category().OnSelectLastAsync();
                            var imageName = lastCat.category_name + lastCat.category_id;
                            ImageServerIO.SavePngImageToServer(pictureBox1.Image, imageName);
                            Dialog.Info("Данные успешно добавлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message.ToString());
                }
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            var iconsFolderPath = Path.Combine(Application.StartupPath, "icons");
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "Png files(*.png) | *.png;",
                InitialDirectory = iconsFolderPath,

            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fd.FileName);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                btnSave.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

