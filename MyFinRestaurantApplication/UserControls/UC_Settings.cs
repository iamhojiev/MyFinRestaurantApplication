using ManagerApplication.Helper;
using ManagerApplication.Model;
using System;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Properties;
using Guna.UI2.WinForms;
using System.Collections.Generic;
using ManagerApplication.Forms;
using System.Threading.Tasks;

namespace ManagerApplication.UserControls
{
    public partial class UC_Settings : UserControl
    {
        private User myUser;
        private string currency;
        private Guna2Button focusedAddBtn;
        private Guna2Button focusedDeleteBtn;
        private Guna2Button focusedEditBtn;

        public UC_Settings()
        {
            InitializeComponent();
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCassa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            metroTabControl1.SelectedIndex = 0;
            InitForm();
        }

        private async void InitForm()
        {
            var payment = await new Payment().OnLoadAsync();
            currency = await new Currency().OnGetCurrencyValueAsync();
            txtCurency.Text = currency.ToString();
            txtPercent.Text = payment.payment_percent.ToString();
            txtFix.Text = payment.payment_fix.ToString();

            if (payment.payment_type == (int)EnumCasheType.Fix) rbtnFix.Checked = true;
            else rbtnPercent.Checked = true;

            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);
            if (myUser.user_role == (int)EnumUserRole.SuperAdmin)
            {
                lblPass.Visible = true;
                txtZipPass.Visible = true;
                if (string.IsNullOrEmpty(Settings.Default.zip_password))
                {
                    Settings.Default.zip_password = "202004";
                    Settings.Default.Save();
                }
                txtZipPass.Text = Settings.Default.zip_password;
                BtnCheckUpdate.Visible = true;
            }

            if (!string.IsNullOrEmpty(Settings.Default.dump_path))
                txtPath.Text = Settings.Default.dump_path;

            InitComboBox();

            UpdateUserGrid();
            UpdateCassaGrid();
            UpdateGridCard();
        }

        private void InitComboBox()
        {
            var rates = new RateType().OnLoad();
            cmbPeriod.DataSource = rates;
            cmbPeriod.ValueMember = "rate_id";
            cmbPeriod.DisplayMember = "rate_name";
            cmbPeriod.SelectedValue = Settings.Default.rate_type;
            cmbPeriod.SelectedIndexChanged += CmbPeriod_SelectedIndexChanged;
        }

        private void CmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newRate = Convert.ToInt32(cmbPeriod.SelectedValue);
            var lastRate = Settings.Default.rate_type;

            if (newRate != lastRate)
            {
                Settings.Default.rate_type = newRate;
                Settings.Default.Save();
                Dialog.Info("Частота резерва успешно изменен!");
            }
        }

        private async void BtnSavePassword_Click(object sender, EventArgs e)
        {
            var myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);

            if (myUser.user_password == txtOldPass.Text.ToString())
            {
                if (txtNewPass.Text.ToString() == txtReloadPass.Text.ToString())
                {
                    myUser.user_password = txtNewPass.Text.ToString();

                    if (await new User().OnUpdateAsync(myUser))
                    {
                        Dialog.Info("Пароль успешно изменен!");
                        ResetTextBoxes();
                    }
                }
                else
                    Dialog.Error("Пароли не совпадают!");
            }
            else
                Dialog.Error("Неверно указан старый пароль!");
        }

        private void ResetTextBoxes()
        {
            txtNewPass.Text = string.Empty;
            txtOldPass.Text = string.Empty;
            txtReloadPass.Text = string.Empty;
            txtOldPass.PlaceholderText = "Старый пароль";
            txtNewPass.PlaceholderText = "Новый пароль";
            txtReloadPass.PlaceholderText = "Повторите пароль";
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPercent.Text == "")
                    txtPercent.Text = "0";

                if (txtFix.Text == "")
                    txtFix.Text = "0";

                var payment = await new Payment().OnLoadAsync();
                var percent = Convert.ToDouble(txtPercent.Text);
                var fix = Convert.ToDouble(txtFix.Text);
                payment.payment_percent = percent;
                payment.payment_fix = fix;

                payment.payment_type = rbtnFix.Checked ? (int)EnumCasheType.Fix : (int)EnumCasheType.Percent;

                var currency = await new Currency().OnLoadAsync();
                currency.currency_value = txtCurency.Text;

                await new Payment().OnUpdateAsync(payment);
                await new Currency().OnUpdateAsync(currency);
                Dialog.Info("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                Dialog.Error(ex.Message.ToString());
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите директорию";
                openFileDialog.CheckFileExists = false;
                openFileDialog.CheckPathExists = true;
                openFileDialog.FileName = $"MyRestaurantDump.sql";
                openFileDialog.Filter = "Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = openFileDialog.FileName;
                    Settings.Default.dump_path = openFileDialog.FileName;
                    Settings.Default.Save();
                    Dialog.Info("Путь успешно обновлен.");
                }
            }
        }

        private async void BtnClearDB_Click(object sender, EventArgs e)
        {
            string string_text = string.Format(
                "Внимание! Данную операцию невозможно будет отменить, все данные в Базе Данных будут очищены!\n" +
                "Вы действительно хотите удалить все данные с БД?");

            if (MessageBox.Show(
                string_text, "Очистка Базы данных!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (await new User().ClearAllDataAsync())
                {
                    Dialog.Info("Все данные успешно удалены!");
                }
            }
        }

        private async void UpdateUserGrid()
        {
            BindingSource bs = new BindingSource();

            List<User> users;
            if (myUser.user_role == (int)EnumUserRole.SuperAdmin)
                users = await new User().OnAllUserAsync(true);
            else
                users = await new User().OnAllUserAsync();

            var roles = await new Role().OnLoadAsync(true);

            foreach (var i in users)
            {
                i.role = roles.Where(u => u.role_id == i.user_role).FirstOrDefault();
                bs.Add(i);
            }
            dgvMain.DataSource = bs;
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                var addEditPersonal = new AddEditPersonal();
                if (addEditPersonal.ShowDialog() == DialogResult.OK)
                    UpdateUserGrid();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                try
                {
                    var aaa = (User)dgvMain.SelectedRows[0].DataBoundItem;

                    var addEditPersonal = new AddEditPersonal(aaa);
                    if (addEditPersonal.ShowDialog() == DialogResult.OK)
                        UpdateUserGrid();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали сотрудника");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                try
                {
                    var aaa = (User)dgvMain.SelectedRows[0].DataBoundItem;
                    var select = aaa.user_name;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.user_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new User().OnDeleteUserAsync(aaa.user_id))
                            UpdateUserGrid();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали сотрудника");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void btnAddCassa_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                var addEditCassa = new AddCassa();
                if (addEditCassa.ShowDialog() == DialogResult.OK)
                    UpdateCassaGrid();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void btnEditCassa_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                try
                {
                    var aaa = (Cassa)dgvCassa.SelectedRows[0].DataBoundItem;
                    var addEditCassa = new AddCassa(aaa);
                    if (addEditCassa.ShowDialog() == DialogResult.OK)
                        UpdateCassaGrid();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали кассу");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void btnDeleteCassa_Click(object sender, EventArgs e)
        {
            if (myUser.user_role <= 1)
            {
                try
                {
                    var aaa = (Cassa)dgvCassa.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.cassa_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Cassa().OnDeleteCassa(aaa))
                            UpdateCassaGrid();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали кассу");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void UpdateCassaGrid()
        {
            BindingSource bs = new BindingSource();
            var cassa = await new Cassa().OnLoadAsync();
            double summa = 0.0;
            foreach (var i in cassa)
            {
                bs.Add(i);
                summa += i.cassa_money;
            }
            dgvCassa.DataSource = bs;

            txtCassaSum.Text = $"Всего: {summa}";
        }

        private void BtnAddCard_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                var window = new AddCard();
                if (window.ShowDialog() == DialogResult.OK)
                    UpdateGridCard();
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnEditCard_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Card)dgvCards.SelectedRows[0].DataBoundItem;

                    var window = new AddCard(aaa);
                    if (window.ShowDialog() == DialogResult.OK)
                        UpdateGridCard();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали доставку!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private async void BtnDeleteCard_Click(object sender, EventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Card)dgvCards.SelectedRows[0].DataBoundItem;

                    string string_text = string.Format(
                        "Вы действительно хотите удалить '{0}'?\n" +
                        "Восстановить его будет невозможно!", aaa.card_name);

                    if (MessageBox.Show(
                        string_text, "Удаление",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (await new Card().OnDeleteAsync(aaa))
                            UpdateGridCard();
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

        private async void UpdateGridCard()
        {
            BindingSource bs = new BindingSource();

            var cards = await new Card().OnLoadAsync();
            double summa = 0.0;
            foreach (var i in cards)
            {
                summa += i.card_balance;
                bs.Add(i);
            }
            dgvCards.DataSource = bs;

            txtCardSum.Text = $"Всего: {summa}";
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
                case "pageUsers":
                    focusedAddBtn = btnAddUser;
                    focusedEditBtn = btnEditUser;
                    focusedDeleteBtn = btnDeleteUser;
                    break;
                case "pageCassa":
                    focusedAddBtn = btnAddCassa;
                    focusedEditBtn = btnEditCassa;
                    focusedDeleteBtn = btnDeleteCassa;
                    break;
                case "pageCards":
                    focusedAddBtn = btnAddCard;
                    focusedEditBtn = btnEditCard;
                    focusedDeleteBtn = btnDeleteCard;
                    break;
                default:
                    focusedAddBtn = null;
                    focusedEditBtn = null;
                    focusedDeleteBtn = null;
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

        private void txtZipPass_Leave(object sender, EventArgs e)
        {
            Settings.Default.zip_password = txtZipPass.Text;
            Settings.Default.Save();
        }

        private void dgvCassa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myUser.user_role != 3)
            {
                try
                {
                    var aaa = (Cassa)dgvCassa.SelectedRows[0].DataBoundItem;
                    var cassaLog = new PageCassaLog(aaa);
                    cassaLog.Show();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Dialog.Error("Вы не выбрали кассу!");
                    return;
                }
            }
            else
                Dialog.Error("Недостаточно прав доступа!");
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdate();
        }

        private async void CheckForUpdate()
        {
            var isConnected = await NetworkHelper.IsInternetAvailableAsync();
            if (!isConnected)
            {
                MessageBox.Show("Нет доступа к интернету.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var updater = new UpdateForm();
            if (updater.CheckForUpdates())
            {
                if (MessageBox.Show("Доступно обновление. Установить?", "Обновление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    updater.UpdateApplication();
                    updater.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("У вас актуальная версия.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

