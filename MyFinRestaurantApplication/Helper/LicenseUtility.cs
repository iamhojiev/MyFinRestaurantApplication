using System;
using ManagerApplication.Model;
using ManagerApplication.Properties;

namespace ManagerApplication.Helper
{
    public class LicenseUtility
    {
        public static string GenerateLicenseValue(License license)
        {
            // Формируем строку данных для хеширования
            var licenseData = $"{MyHard.GetHardwareId()};{license.license_type};{license.license_last_updated};{license.license_expiry_date};";

            return Hash.GetHash(licenseData); // Возвращаем хеш-значение
        }

        public static bool IsLicenseValid(License license, out string reason)
        {
            reason = string.Empty; // Инициализируем строку причины

            // Проверяем дату истечения лицензии
            if (DateTime.TryParse(license.license_expiry_date, out DateTime expiryDate))
            {
                if (DateTime.Now > expiryDate)
                {
                    reason = "Лицензия больше не действительна. Для продолжения работы обновите лицензию.";
                    return false;
                }
            }
            else
            {
                reason = "Неверный формат даты истечения лицензии.";
                return false;
            }

            // Проверяем дату последнего входа
            if (DateTime.TryParse(Settings.Default.last_entry, out DateTime lastEntry))
            {
                if (DateTime.Now > lastEntry)
                {
                    Settings.Default.last_entry = DateTime.Now.ToString();
                    Settings.Default.Save();
                }
                else
                {
                    reason = "Системное время вашего компьютера было изменено." +
                        " Пожалуйста, верните их в актуальное состояние для корректной работы лицензии.";
                    return false;
                }
            }
            else
            {
                Settings.Default.last_entry = DateTime.Now.ToString();
                Settings.Default.Save();
            }

            // Проверяем значение лицензии
            var licenseValue = GenerateLicenseValue(license);
            if (licenseValue != license.license_value)
            {
                reason = "Обнаружены изменения в содержании лицензионного соглашения. " +
                    "Пожалуйста, восстановите оригинальную версию лицензии для продолжения использования приложения.";
                return false;
            }

            return true; // Лицензия действительна
        }
    }
}