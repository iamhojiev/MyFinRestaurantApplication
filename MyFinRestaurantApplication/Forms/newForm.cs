using ManagerApplication.Helper;
using ManagerApplication.Model;
using MyFinRestaurantApplication.DataBase;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFinRestaurantApplication.Forms
{
    public partial class newForm : Form
    {
        private License _myLicense;
        private FireLicense _fireLicense;

        // Инициализируем сервис в конструкторе
        private readonly FirestoreService _firestoreService = new FirestoreService();
            
        public newForm()
        {
            InitializeComponent();
        }

        private async void LicenseForm_Load(object sender, EventArgs e)
        {
            btnActiveLicense.Enabled = false;
            bool isConnected = await NetworkHelper.IsInternetAvailableAsync();

            if (isConnected)
            {
                await CheckLicense();
            }
            else
            {
                ShowError("Нет доступа к интернету.");
                DialogResult = DialogResult.Abort;
            }
            btnActiveLicense.Enabled = true;

        }

        private async Task CheckLicense()
        {
            // Получаем локальную лицензию асинхронно
            _myLicense = await new License().OnSelectAsync();

            //// Если лицензия отсутствует или недействительна, обновляем UI соответствующим сообщением
            //if (_myLicense == null || !LicenseUtility.IsLicenseValid(_myLicense, out _))
            //{
            //    _myLicense = null;
            //}

            // Проверяем облачную лицензию по ключу лицензии
            _fireLicense = await _firestoreService.GetFireLicenseByIdAsync(_myLicense.license_key);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_fireLicense != null && _fireLicense.IsActive && _fireLicense.ExpirationDate > DateTime.Now)
            {
                txtLicenseInfo.Text = "Лицензия активна!";
                txtExpiryDate.Visible = true;
                txtExpiryDate.Text = $"Срок действия: {_myLicense.license_expiry_date}";
                btnActiveLicense.Text = "Обновить";
                txtLicenseKey.Text = _fireLicense.LicenseKey;
            }
            else
            {
                txtLicenseInfo.Text = "Требуется активация лицензии!";
                txtExpiryDate.Visible = false;
            }
        }

        private async void BtnActiveLicense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseKey.Text))
            {
                ShowError("Введите лицензионный ключ");
                return;
            }

            btnActiveLicense.Enabled = false;
            // Удаляем дефисы или пробелы из введённого ключа
            string userKey = txtLicenseKey.Text.Replace("-", "").Trim();

            try
            {
                FireLicense cloudLicense = await _firestoreService.GetFireLicenseByIdAsync(userKey);

                if (cloudLicense == null)
                {
                    ShowError("Неверный ключ");
                    return;
                }

                if (_myLicense == null)
                {
                    // Активация новой лицензии
                    await ActivateNewLicense(cloudLicense);
                }
                else
                {
                    // Обновление существующей лицензии
                    await UpdateExistingLicense(cloudLicense);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка: {ex.Message}");
            }
            finally
            {
                btnActiveLicense.Enabled = true;
            }
        }

        private License ConvertCloudToLocalLicense(FireLicense cloudLicense)
        {
            return new License
            {
                license_key = cloudLicense.LicenseKey,
                license_last_updated = DateTime.Now.ToShortDateString(),
                license_expiry_date = cloudLicense.ExpirationDate.ToShortDateString(),
                license_type = cloudLicense.Type
            };
        }

        private async Task ActivateNewLicense(FireLicense cloudLicense)
        {
            // Проверяем, не активирована ли лицензия на другом устройстве
            if (!string.IsNullOrEmpty(cloudLicense.CustomerId))
            {
                ShowError("Этот лицензионный ключ уже активирован на другом устройстве!");
                txtLicenseKey.Text = string.Empty;
                return;
            }

            // Преобразование облачной лицензии в локальную
            var localLicense = ConvertCloudToLocalLicense(cloudLicense);

            // Генерация значения лицензии
            localLicense.license_value = LicenseUtility.GenerateLicenseValue(localLicense);
            await new License().OnInsertAsync(localLicense); // Сохраняем лицензию локально

            // Обновляем облачную лицензию привязкой к текущему устройству
            cloudLicense.CustomerId = MyHard.GetHardwareId();
            await _firestoreService.UpdateFireLicenseAsync(cloudLicense);

            ShowInfo("Активация успешна!");
            DialogResult = DialogResult.OK;
        }

        private async Task UpdateExistingLicense(FireLicense cloudLicense)
        {
            // Проверяем, закреплена ли лицензия за текущим устройством
            if (cloudLicense.CustomerId != MyHard.GetHardwareId())
            {
                ShowError("Этот лицензионный ключ уже активирован на другом устройстве!");
                txtLicenseKey.Text = string.Empty;
                return;
            }

            // Конвертируем дату из локальной лицензии и проверяем, требуется ли обновление
            if (DateTime.TryParse(_myLicense.license_expiry_date, out DateTime currentExpiration))
            {
                if (cloudLicense.ExpirationDate.Date > currentExpiration.Date || cloudLicense.Type != _myLicense.license_type)
                {
                    // Создаём обновлённую локальную лицензию на основании облачных данных
                    var updatedLicense = ConvertCloudToLocalLicense(cloudLicense);
                    updatedLicense.license_value = LicenseUtility.GenerateLicenseValue(updatedLicense);

                    // Обновляем локальную лицензию
                    await new License().OnUpdateAsync(updatedLicense);

                    ShowInfo("Лицензия успешно обновлена!");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    ShowWarning("Нет доступных обновлений для этой лицензии");
                }
            }
            else
            {
                ShowError("Неверный формат даты локальной лицензии.");
            }
        }

        private void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка лицензии!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarning(string warning)
        {
            MessageBox.Show(warning, "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtLicenseKey_TextChanged(object sender, EventArgs e)
        {
            // Удаление всех тире для упрощения обработки
            string text = txtLicenseKey.Text.Replace("-", string.Empty);

            // Форматируем строку с добавлением тире
            string formatted = string.Join("-", Enumerable.Range(0, text.Length / 4 + (text.Length % 4 > 0 ? 1 : 0))
                .Select(i => text.Substring(i * 4, Math.Min(4, text.Length - i * 4))));

            // Устанавливаем текст только если он изменился
            if (formatted != txtLicenseKey.Text)
            {
                int cursorPosition = txtLicenseKey.SelectionStart;
                txtLicenseKey.Text = formatted;

                // Устанавливаем курсор в правильное положение
                txtLicenseKey.SelectionStart = Math.Min(cursorPosition + (formatted.Length - text.Length), formatted.Length);
            }
        }

        private void txtLicenseKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && txtLicenseKey.SelectionStart > 0)
            {
                int cursorPosition = txtLicenseKey.SelectionStart;

                // Проверяем, не стоит ли курсор после тире
                if (txtLicenseKey.Text[cursorPosition - 1] == '-')
                {
                    txtLicenseKey.Text = txtLicenseKey.Text.Remove(cursorPosition - 2, 2);
                    txtLicenseKey.SelectionStart = cursorPosition - 2;
                    e.Handled = true;
                }
            }
        }
    }
}
