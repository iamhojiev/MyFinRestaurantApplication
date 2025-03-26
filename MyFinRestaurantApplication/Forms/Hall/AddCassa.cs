using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddCassa : Form
    {

        private Cassa _cassa;
        public AddCassa(Cassa cassa = null)
        {
            InitializeComponent();

            if (cassa != null)
            {
                txtTitle.Text = "Редактирование данных";
                txtName.Text = cassa.cassa_name;
                txtName.ReadOnly = true;
                _cassa = cassa;
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
                    if (_cassa != null)
                    {
                        _cassa.cassa_name = txtName.Text;
                        if (await new Cassa().OnUpdateAsync(_cassa))
                        {
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var newCassa = new Cassa()
                        {
                            cassa_name = txtName.Text,
                            cassa_money = 0,
                        };
                        if (await new Cassa().OnInsertAsync(newCassa))
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

