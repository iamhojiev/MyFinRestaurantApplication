using ManagerApplication.Properties;
using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Collections.Generic;

namespace ManagerApplication.UserControls
{
    public partial class UC_HallAndTables : UserControl
    {
        private User myUser;
        private Guna2Button focusedAddBtn;
        private Guna2Button focusedDeleteBtn;
        private Guna2Button focusedEditBtn;

        public UC_HallAndTables()
        {
            InitializeComponent();
            dgvHall.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvKitchen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            metroTabControl1.SelectedIndex = 1;
            metroTabControl1.SelectedIndex = 0;
            InitCmbHall();
            UpdateGridTable();
            UpdateGridHall();
            UpdateGridKitchen();

        }

        private async void InitCmbHall()
        {
            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);
            List<Hall> halls = new List<Hall>()
            {
                new Hall { hall_name = "Показать все" }
            };
            halls.AddRange(await new Hall().OnLoadHallAsync());
            cmbHall.DataSource = halls;
            cmbHall.DisplayMember = "hall_name";
            cmbHall.ValueMember = "hall_id";
        }

        private void BtnAddTable_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddTable();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateGridTable();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Tables)dgvTables.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.table_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Tables().OnDeleteAsync(aaa))
                            UpdateGridTable();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали столик");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void UpdateGridTable()
        {
            BindingSource bs = new BindingSource();

            var halls = await new Hall().OnLoadHallAsync();
            var tables = await new Tables().OnLoadAsync();
            tables = tables.Where(u => u.table_id > 0).ToList();
            if (cmbHall.SelectedIndex > 0)
            {
                tables = tables.Where(u => u.table_hall_id == Convert.ToInt32(cmbHall.SelectedValue)).ToList();
            }
            var tablesStatus = await new TableStatus().OnLoadAsync();

            foreach (var i in tables)
            {
                i.hall = halls.Where(u => u.hall_id == i.table_hall_id).FirstOrDefault();
                i.tables_status = tablesStatus.Where(u => u.table_st_id == i.table_status).FirstOrDefault();
                bs.Add(i);
            }
            dgvTables.DataSource = bs;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Tables)dgvTables.SelectedRows[0].DataBoundItem;

                    var window = new AddTable(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateGridTable();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали столик!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Tables)dgvTables.SelectedRows[0].DataBoundItem;

                    var window = new PageTableInfo(aaa);
                    window.ShowDialog();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали столик!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnStatus_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Tables)dgvTables.SelectedRows[0].DataBoundItem;

                    var window = new ChangeTableStatus(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateGridTable();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали столик!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnAddHall_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddHall();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateGridHall();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEditHall_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Hall)dgvHall.SelectedRows[0].DataBoundItem;

                    var window = new AddHall(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateGridHall();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали зал!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteHall_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Hall)dgvHall.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.hall_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Hall().OnDeleteAsync(aaa))
                            UpdateGridHall();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали зал!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void UpdateGridHall()
        {
            BindingSource bs = new BindingSource();

            var halls = await new Hall().OnLoadHallAsync();
            var hallTypes = new HallType().OnLoad();
            foreach (var i in halls)
            {
                i.hallType = hallTypes.Where(t => t.type_id == i.hall_type).FirstOrDefault();
                bs.Add(i);
            }
            dgvHall.DataSource = bs;
        }

        private void BtnAddKitchen_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddKitchen();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateGridKitchen();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEditKitchen_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Kitchen)dgvKitchen.SelectedRows[0].DataBoundItem;

                    var window = new AddKitchen(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateGridKitchen();
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

        private async void BtnDeleteKitchen_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Kitchen)dgvKitchen.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.kitchen_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Kitchen().OnDeleteAsync(aaa))
                            UpdateGridKitchen();
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

        private async void UpdateGridKitchen()
        {
            BindingSource bs = new BindingSource();

            var halls = await new Kitchen().OnLoadAsync();

            foreach (var i in halls)
            {
                bs.Add(i);
            }
            dgvKitchen.DataSource = bs;
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentTab();
        }

        public void UpdateCurrentTab()
        {
            var myTab = metroTabControl1.SelectedTab;

            switch (myTab.Name)
            {
                case "pageTables":
                    focusedAddBtn = btnAddTable;
                    focusedEditBtn = btnEditTable;
                    focusedDeleteBtn = btnDeleteTable;
                    break;
                case "pageHalls":
                    focusedAddBtn = btnAddHall;
                    focusedEditBtn = btnEditHall;
                    focusedDeleteBtn = btnDeleteHall;
                    break;
                case "pageKitchens":
                    focusedAddBtn = btnAddKitchen;
                    focusedEditBtn = btnEditKitchen;
                    focusedDeleteBtn = btnDeleteKitchen;
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oemplus || keyData == Keys.Add)
            {
                focusedAddBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.OemMinus || keyData == Keys.Delete || keyData == Keys.Subtract)
            {
                focusedDeleteBtn?.PerformClick();
                return true;
            }

            if (keyData == Keys.E || keyData == Keys.B || keyData == Keys.F2)
            {
                focusedEditBtn?.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cmbHall_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridTable();
        }
    }
}
