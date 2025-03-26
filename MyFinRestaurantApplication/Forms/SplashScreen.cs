using ManagerApplication.Helper;
using ManagerApplication.Model;
using ManagerApplication.Properties;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerApplication.Forms
{
    public partial class SplashScreen : Form
    {
        private readonly Random random = new Random();
        private bool isLicenseChecked = false; // Флаг для проверки лицензии

        private readonly int licenseCheckPoint; // Точка, на которой проверится лицензия

        public SplashScreen()
        {
            InitializeComponent();
            shadowForm.SetShadowForm(this);
            timerProgress.Start();
            licenseCheckPoint = random.Next(30, 70);

            InitVersionLabel();
            MigrateUserSettings();
            MigrateDb();
        }

        private void InitVersionLabel()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version currentVersion = assembly.GetName().Version;
            txtVersion.Text = $"Версия {currentVersion}";
        }

        private void MigrateUserSettings()
        {
            if (Settings.Default.SettingsUpgraded == false)
            {
                Settings.Default.Upgrade(); // Переносим старые настройки
                Settings.Default.SettingsUpgraded = true;
                Settings.Default.Save(); // Сохраняем изменения
            }
        }

        private async void MigrateDb()
        {
            await new User().OnMigrate();
        }

        private async Task CheckLicenseAsync()
        {
            var myLicense = await new License().OnSelectAsync();
            if (myLicense == null)
            {
                LicenseErrorShow("Лицензия не была найдена. Возможно, она истекла или отсутствует.");
                return;
            }

            if (!LicenseUtility.IsLicenseValid(myLicense, out string reason))
            {
                LicenseErrorShow(reason);
            }
        }

        private void LicenseErrorShow(string error)
        {
            MessageBox.Show(error, "Ошибка лицензии!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }

        private async void timerProgress_Tick(object sender, EventArgs e)
        {
            if (!isLicenseChecked && progresLoading.Value > licenseCheckPoint)
            {
                timerProgress.Stop();
                isLicenseChecked = true; // Отмечаем, что проверка уже прошла

              //  await CheckLicenseAsync();

                timerProgress.Start();
            }

            // Двигаем прогресс неравномерно
            if (progresLoading.Value < 100)
            {
                progresLoading.Value += random.Next(1, 5);
                timerProgress.Interval = random.Next(10, 100);
            }
            else
            {
                timerProgress.Stop();
                var window = new PageAuthorization();
                window.Show();
                Hide();
            }
        }
    }
}