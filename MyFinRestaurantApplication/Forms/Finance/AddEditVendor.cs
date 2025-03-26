using System;
using System.Windows.Forms;
using ManagerApplication.Helper;
using ManagerApplication.Model;

namespace ManagerApplication
{
    public partial class AddEditVendor : Form
    {

        public AddEditVendor()
        {
            InitializeComponent();
           
        }

        private async void bnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtNumber.Text != "")
            {
                try
                {
                    var vendor = new Vendor()
                    {
                        vendor_name = txtName.Text,
                        vendor_phone = txtNumber.Text,
                        vendor_debt = 0,
                    };

                    if (await new Vendor().OnInsertAsync(vendor))
                    {
                        DialogResult = DialogResult.OK;
                    }

                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message.ToString());
                }
            }
            else
            {
                Dialog.Error("Заполните все поле");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

