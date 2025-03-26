
using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class AddDelivery : Form
    {
        private Delivery delivery;

        public AddDelivery(Delivery delivery = null)
        {
            InitializeComponent();

            if (delivery != null)
            {
                this.delivery = delivery;
                txtTitle.Text = "Редактирование данных";
                txtName.Text = delivery.delivery_name;
                txtPrice.Text = delivery.delivery_price.ToString();
                txtDescription.Text = delivery.delivery_desc;
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
                    if (delivery != null)
                    {
                        delivery.delivery_name = txtName.Text;
                        delivery.delivery_price = Convert.ToDouble(txtPrice.Text);
                        delivery.delivery_desc = txtDescription.Text;

                        if (await new Delivery().OnUpdateAsync(delivery))
                        {
                            UpdateHallAndTable();

                            Dialog.Info("Данные успешно обновлены!");
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

        private async void UpdateHallAndTable()
        {
            int id = delivery.delivery_id *= -1;

            var hall = new Hall()
            {
                hall_id = id,
                hall_name = "Доставка",
                hall_price = delivery.delivery_price,
            };
            var table = new Tables()
            {
                table_id = id,
                table_name = delivery.delivery_name,
                table_hall_id = id,
                hall = hall
            };
            await new Hall().OnUpdateAsync(hall);
             await new Tables().OnInsertAsync(table);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
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

