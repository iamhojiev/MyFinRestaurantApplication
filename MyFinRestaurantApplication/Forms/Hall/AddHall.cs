using System;
using System.Windows.Forms;
using ManagerApplication.Helper;
using ManagerApplication.Model;

namespace ManagerApplication
{
    public partial class AddHall : Form
    {
        private Hall _hall;
        private HallType selectedType;

        public AddHall(Hall hall = null)
        {
            InitializeComponent();

            InitializeComboBoxes();
            
            if (hall != null)
            {
                _hall = hall;
                txtTitle.Text = "Редактирование данных";
                txtPrice.Text = _hall.hall_price.ToString();
                txtName.Text = _hall.hall_name;
                SetInComboBox(_hall.hall_type);
            }

            SetFocus();
        }

        private void InitializeComboBoxes()
        {
            var hallTypes = new HallType().OnLoad();
            cmbType.DataSource = hallTypes;
            cmbType.DisplayMember = "type_name";
            cmbType.ValueMember = "type_id";
        }

        private void SetInComboBox(int id)
        {
            cmbType.SelectedValue = id;
        }

        private void SetFocus()
        {
            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex != -1)
            {
                selectedType = (HallType)cmbType.SelectedItem;

                switch (selectedType.type_id)
                {
                    case (int)EnumHallType.Free:
                        txtPriceInfo.Text = "Стоимость:";
                        txtPrice.Text = "0";
                        txtPrice.Enabled = false;
                        timePicker.Enabled = false;
                        break;
                    case (int)EnumHallType.Fixed:
                        txtPriceInfo.Text = "Стоимость:";
                        txtPrice.Enabled = true;
                        timePicker.Enabled = false;
                        break;
                    case (int)EnumHallType.TimeBased:
                        txtPriceInfo.Text = "Стоимость за час:";
                        txtPrice.Enabled = true;
                        timePicker.Enabled = true;
                        break;
                }
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                try
                {
                    var name = txtName.Text;
                    var price = Convert.ToDouble(txtPrice.Text);
                    var type = selectedType != null ? selectedType.type_id : 0;

                    var bonusMinutes = CalculateBonusMinutes();

                    if (_hall != null)
                    {
                        _hall.hall_name = name;
                        _hall.hall_price = price;
                        _hall.hall_type = type;
                        _hall.hall_bonus = bonusMinutes;

                        if (await new Hall().OnUpdateAsync(_hall))
                        {
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var hall = new Hall()
                        {
                            hall_name = txtName.Text,
                            hall_price = Convert.ToDouble(txtPrice.Text),
                            hall_type = type,
                            hall_bonus = bonusMinutes,
                        };
                        if (await new Hall().OnInsertAsync(hall, cmbType.Text))
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

        private int CalculateBonusMinutes()
        {
            var selectedTime = timePicker.Value;
            var startDateTime = new DateTime(2000, 1, 1, 0, 0, 0);

            TimeSpan timeDiff = selectedTime - startDateTime;

            return (int)timeDiff.TotalMinutes;
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

