using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace ManagerApplication
{
    public partial class AddTable : Form
    {

        private Tables _table;
        public AddTable(Tables table = null)
        {
            InitializeComponent();

            InitHallsComboBox();

            if (table != null)
            {
                _table = table;
                txtTitle.Text = "Редактирование данных";
                txtName.Text = _table.table_name;
                txtCount.Text = _table.table_place.ToString();
                cmbHall.SelectedValue = Convert.ToInt32(table.table_hall_id);
                //table.table_hall_id = Convert.ToInt32(cmbHall.SelectedValue);
            }
            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private async void InitHallsComboBox()
        {
            var halls = await new Hall().OnLoadHallAsync();
            cmbHall.DataSource = halls;
            cmbHall.DisplayMember = "hall_name";
            cmbHall.ValueMember = "hall_id";
            cmbHall.SelectedIndex = 0;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtCount.Text != "")
            {
                var count = Convert.ToInt32(txtCount.Text);
                var hallId = Convert.ToInt32(cmbHall.SelectedValue);
                try
                {
                    if (IsIdentityTable(txtName.Text, hallId))
                    {
                        if (_table != null)
                        {
                            _table.table_name = txtName.Text;
                            _table.table_place = count;
                            _table.table_hall_id = hallId;

                            if (await new Tables().OnUpdateAsync(_table))
                            {
                                Dialog.Info("Данные успешно обновлены!");
                                DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            var tables = new Tables()
                            {
                                table_name = txtName.Text,
                                table_date = DateTime.Now.ToShortDateString(),
                                table_time = DateTime.Now.ToShortTimeString(),
                                table_place = count,
                                table_hall_id = hallId,
                                table_status = 1
                            };
                            if (await new Tables().OnInsertAsync(tables))
                            {
                                Dialog.Info("Данные успешно добавлены!");
                                DialogResult = DialogResult.OK;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Dialog.Error(ex.Message.ToString());
                }
            }
            else
            {
                Dialog.Error("Заполните все обязательные поля!");
                if (string.IsNullOrEmpty(txtName.Text.ToString()))
                {
                    FocusTo(txtName);
                }
                else
                {
                    FocusTo(txtCount);
                }
            }
        }

        private bool IsIdentityTable(string text, int hallId)
        {
            var checkTable = new Tables().ChecKTableAsync(text, hallId);
            if (checkTable == null)
            {
                return true;
            }
            else
            {
                Dialog.Error("В этом зале существует стол с таким же наименованием!");
                FocusTo(txtName);
                return false;
            }


        }

        private void FocusTo(TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
            //textBox.se();
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbHall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !cmbHall.DroppedDown)
            {
                cmbHall.DroppedDown = true;
                e.Handled = true;
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

