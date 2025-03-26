using System;
using System.Windows.Forms;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using ManagerApplication.Properties;

namespace ManagerApplication
{
    public partial class PageAuthorization : Form
    {
        public PageAuthorization()
        {
            InitializeComponent();
            InitializeFormAsync();
        }

        private void InitializeFormAsync()
        {
            InitDateLabel();
            txtPass.Focus();
            txtPass.Select();
            txtPass.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LogIn.PerformClick();
                }
            };
        }

        private void InitDateLabel()
        {
            var dateTime = DateTime.Now;
            string dateToday = $"{dateTime.Day}.{dateTime.Month:D2}.{dateTime.Year}";
            string nowTime = $"{dateTime.Hour:D2}:{dateTime.Minute:D2}";
            string weekDay = WeekParse(dateTime.DayOfWeek.ToString());
            lblDate.Text = dateToday + " / " + nowTime + Environment.NewLine + weekDay;
        }

        private string WeekParse(string week)
        {
            switch (week)
            {
                case "Monday":
                    return "Понедельник";
                case "Tuesday":
                    return "Вторник";
                case "Wednesday":
                    return "Среда";
                case "Thursday":
                    return "Четверг";
                case "Friday":
                    return "Пятница";
                case "Saturday":
                    return "Суббота";
                case "Sunday":
                    return "Воскресенье";
                default:
                    return "";
            }
        }

        private async void LogIn_Click(object sender, EventArgs e)
        {
            var pass = txtPass.Text;

            var user = await new User().OnSelectPasswordAsync(pass);

            if (user != null)
            {
                if (user.user_role != (int)EnumUserRole.Admin
                    && user.user_role != (int)EnumUserRole.Manager
                    && user.user_role != (int)EnumUserRole.SuperAdmin)
                {
                    await Debug.DebugInsertAsync(string.Format("Неудачная попытка входа: Пароль: {0}", pass));
                    Dialog.Error("Доступно только для администраторов и менеджеров!");
                    return;
                }

                txtPass.Text = "";

                Settings.Default.user_id = user.user_id;
                Settings.Default.Save();

                var mainForm = new MainForm()
                {
                    Owner = this
                };
                mainForm.Show();
                Hide();
            }
            else
            {
                await Debug.DebugInsertAsync(string.Format("Неудачная попытка входа: Пароль: {0}", pass));
                Dialog.Error("Пароль введён неверно!");
            }
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtPass.Text += '1';
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtPass.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtPass.Text += '3';
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtPass.Text += '4';
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtPass.Text += '5';
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtPass.Text += '6';
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtPass.Text += '7';
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtPass.Text += '8';
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtPass.Text += '9';
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtPass.Text = "";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtPass.Text += '0';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.Length > 0)
                txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.Length - 1);
        }

        private void linkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
         //   ShowLicenseDialog();
        }

        //private static void ShowLicenseDialog()
        //{
        //    newForm licenseForm = new newForm();
        //    licenseForm.ShowDialog();
        //}
    }
}
