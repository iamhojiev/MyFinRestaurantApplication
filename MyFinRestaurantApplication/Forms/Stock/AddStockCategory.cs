using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddStockCategory : Form
    {
        private StockCategory _stCategory;
        public AddStockCategory(StockCategory stCategory = null)
        {
            InitializeComponent();

            if (stCategory != null)
            {
                _stCategory = stCategory;
                txtTitle.Text = "Редактирование данных";
                txtName.Text = _stCategory.st_cat_name;
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
                    if (_stCategory != null)
                    {
                        _stCategory.st_cat_name = txtName.Text;

                        if (await new StockCategory().OnUpdateAsync(_stCategory))
                        {
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var hall = new StockCategory()
                        {
                            st_cat_name = txtName.Text,
                        };
                        if (await new StockCategory().OnUpdateAsync(hall))
                        {
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

