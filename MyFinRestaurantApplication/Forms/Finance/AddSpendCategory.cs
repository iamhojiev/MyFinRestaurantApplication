using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddSpendCategory : Form
    {
        private SpendCategory category;

        public AddSpendCategory(SpendCategory sendedCategory = null)
        {
            InitializeComponent();

            category = sendedCategory;

            if (category != null)
            {
                txtTitle.Text = "Редактирование данных";
                txtName.Text = category.category_name;
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
                    if (category != null)
                    {
                        category.category_name = txtName.Text;

                        if (await new SpendCategory().OnInsertAsync(category))
                        {
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var aaa = new SpendCategory()
                        {
                            category_name = txtName.Text,
                        };
                        if (await new SpendCategory().OnInsertAsync(aaa))
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

    