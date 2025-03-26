using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.Properties;
using System;
using System.Windows.Forms;


namespace ManagerApplication
{
    public partial class AddEditPersonal : Form
    {
        private User userToEdit;
        private User myUser;

        public AddEditPersonal(User userEdit = null)
        {
            InitializeComponent();

            InitForm(userEdit);
        }

        private async void InitForm(User userEdit)
        {
            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);

            var roles = myUser.user_role == (int)EnumUserRole.SuperAdmin
                ? await new Role().OnLoadAsync(true)
                : await new Role().OnLoadAsync();
            cmbRole.DataSource = roles;
            cmbRole.DisplayMember = "role_name";
            cmbRole.ValueMember = "role_id";
            cmbRole.SelectedIndex = 0;

            var kitchens = await new Kitchen().OnLoadAsync();
            cmbKitchen.DataSource = kitchens;
            cmbKitchen.DisplayMember = "kitchen_name";
            cmbKitchen.ValueMember = "kitchen_id";
            cmbKitchen.SelectedIndex = 0;

            if (userEdit != null)
            {
                userToEdit = userEdit;
                if (myUser.user_role != (int)EnumUserRole.SuperAdmin)
                {
                    txtPassword.ReadOnly = true;
                    txtPassword.Text = "***********";
                }
                else
                {
                    txtPassword.Text = userEdit.user_password;
                }
                txtTitle.Text = "Редактирование пользователя";
                txtName.Text = userToEdit.user_name;
                cmbRole.SelectedValue = userEdit.user_role;
                cmbSalary.SelectedIndex = userEdit.user_salary_type;
                txtSalary.Text = string.Format("{0:F2}", userEdit.user_salary);
            }
            txtName.Focus();
            txtName.Select();
            txtName.SelectAll();
        }

        private void CloseBtn(object sender, EventArgs e)
        {
            Close();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                var check = await new User().OnSelectPasswordAsync(txtPassword.Text);
                if (check == null)
                {
                    if (userToEdit != null)
                    {
                        userToEdit.user_name = txtName.Text;
                        userToEdit.user_password = txtPassword.Text;
                        userToEdit.user_role = Convert.ToInt32(cmbRole.SelectedValue);
                        userToEdit.user_salary = Convert.ToDouble(txtSalary.Text);
                        userToEdit.user_kitchen = Convert.ToInt32(cmbKitchen.SelectedValue);
                        userToEdit.user_salary_type = cmbSalary.SelectedIndex;

                        if (await new User().OnUpdateAsync(userToEdit))
                        {
                            Dialog.Info("Данные успешно обновлены!");
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        var user = new User()
                        {
                            user_name = txtName.Text,
                            user_password = txtPassword.Text,
                            user_role = Convert.ToInt32(cmbRole.SelectedValue),
                            user_kitchen = Convert.ToInt32(cmbKitchen.SelectedValue),
                            user_salary = Convert.ToDouble(txtSalary.Text),
                            user_last_payment = DateTime.Now.ToShortDateString(),
                            user_salary_type = cmbSalary.SelectedIndex
                        };
                        if (await new User().OnInsertAsync(user))
                        {
                            Dialog.Info("Данные успешно добавлены!");
                            DialogResult = DialogResult.OK;
                        }

                    }
                }
                else
                    Dialog.Error("Пароль должен быть уникальным! Данный пароль уже существует!");
            }
            else
                Dialog.Error("Поле 'Имя' является обязательным для заполнение!");
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // Заменяем введённый символ на десятичный разделитель текущей культуры
                e.KeyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }

        private void cmb_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cmbSender = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter && !cmbSender.DroppedDown)
            {
                cmbSender.DroppedDown = true;
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

