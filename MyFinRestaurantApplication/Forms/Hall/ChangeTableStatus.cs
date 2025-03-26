using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Windows.Forms;

namespace ManagerApplication
{
    public partial class ChangeTableStatus : Form
    {
        private Tables _tables;
        public ChangeTableStatus(Tables tables)
        {
            InitializeComponent();

            _tables = tables;

            InitStatusComboBox(tables);

            txtDate.Text = tables.table_date;
            txtTime.Text = tables.table_time;
            cmbStatus.Focus();
            cmbStatus.Select();
            OnCheck();
        }

        private async void InitStatusComboBox(Tables tables)
        {
            var status = await new TableStatus().OnLoadAsync();
            cmbStatus.DataSource = status;
            cmbStatus.DisplayMember = "table_st_name";
            cmbStatus.ValueMember = "table_st_id";
            cmbStatus.SelectedValue = tables.table_status;
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            _tables.table_status = Convert.ToInt32(cmbStatus.SelectedValue);
            _tables.table_date = txtDate.Text;
            _tables.table_time = txtTime.Text;

            if (await new Tables().OnUpdateStatusAsync(_tables))
            {
                Dialog.Info("Вы успешно обновили данные!");
                DialogResult = DialogResult.OK;
            }
        }

        private void CmbStatus_SelectionChanged(object sender, EventArgs e)
        {
            OnCheck();
        }

        private void OnCheck()
        {
            if (cmbStatus.SelectedIndex == 1)
            {
                txtDate.Enabled = true;
                txtTime.Enabled = true;
            }
            else
            {
                txtDate.Enabled = false;
                txtTime.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !cmbStatus.DroppedDown)
            {
                cmbStatus.DroppedDown = true;
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

    