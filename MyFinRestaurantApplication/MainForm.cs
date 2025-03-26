using ManagerApplication.Properties;
using ManagerApplication.UserControls;
using ManagerApplication.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ManagerApplication.Helper;
using ManagerApplication;

namespace ManagerApplication
{
    public partial class MainForm : Form
    {
        UC_Accounting accounting;
        UC_Diagrams diagrams;
        UC_HallAndTables hallAndTables;
        UC_Product product;
        UC_Settings settings;
        UC_Directory directory;
        UC_Finance finance;

        private User myUser;
        public MainForm()
        {
            InitializeComponent();

            accounting = new UC_Accounting();
            SetUserControl(accounting);

            InitForm();

            CheckForUsersSalary();
            InitializeDumpForm();
        }

        private async void InitForm()
        {
            myUser = await new User().OnSelectUserAsync(Settings.Default.user_id);
            var roles = await new Role().OnLoadAsync();
            myUser.role = roles.Where(u => u.role_id == myUser.user_role).FirstOrDefault();

            nameLbl.Text = myUser.user_name;
            roleLbl.Text = myUser.UserRoleName;
            var year = DateTime.Now.Year;
            txtLogo.Text = string.Format("MYFIN Ресторан © {0}", year);
        }

        private void InitializeDumpForm()
        {
            FrmDumper dumper = (FrmDumper)Application.OpenForms["FrmDumper"];

            if (dumper == null)
            {
                dumper = new FrmDumper();
                dumper.Show();
            }
        }

        private async void CheckForUsersSalary()
        {
            var users = await new User().OnAllUserAsync();
            foreach (var user in users)
            {
                if (user.user_salary_type == (int)EnumSalaryType.Monthly)
                {
                    if (SalaryManager.CheckForMonthlyPayment(user))
                    {
                        var ot = DateTime.Parse(user.user_last_payment).ToShortDateString();
                        var res = $"{ot} - {DateTime.Now.ToShortDateString()}";
                        SalaryManager.GiveUserSalary(user, user.user_salary, res);
                    }
                }
            }
        }

        private void SetUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            mainContainer.Controls.Clear();
            mainContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Owner.Show();
            Owner.Refresh();
            Owner.Activate();
            this.Close();
        }

        private void btnAccounting_Click(object sender, EventArgs e)
        {
            if (accounting == null) accounting = new UC_Accounting();
            else accounting.UpdateCurrentTab();
            SetUserControl(accounting);
        }

        private void btnDiagrams_Click(object sender, EventArgs e)
        {
            if (diagrams == null) diagrams = new UC_Diagrams();
            else diagrams.UpdateCurrentTab();
            SetUserControl(diagrams);
        }

        private void btnHallAndTable_Click(object sender, EventArgs e)
        {
            if (hallAndTables == null) hallAndTables = new UC_HallAndTables();
            else hallAndTables.UpdateCurrentTab();
            SetUserControl(hallAndTables);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (product == null) product = new UC_Product();
            else product.UpdateGrid();
            SetUserControl(product);
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            if (directory == null) directory = new UC_Directory();
            else directory.UpdateCurrentTab();
            SetUserControl(directory);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (settings == null) settings = new UC_Settings();
            else settings.UpdateCurrentTab();
            SetUserControl(settings);
        }

        private void btnFinance_Click(object sender, EventArgs e)
        {
            if (finance == null) finance = new UC_Finance();
            else finance.UpdateCurrentTab();
            SetUserControl(finance);
        }
    }
}
