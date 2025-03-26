using System;
using System.Windows;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using ManagerApplication.Model;
using ManagerApplication.Helper;
using System.Collections.Generic;

namespace MyFinRestaurantApplication.DataBase
{
    public class FirestoreService
    {
        private FirestoreDb _firestoreLicenseDb;
        private FirestoreDb _firestoreAdminDb;

        private async Task SetupLicenseDb()
        {
            if(_firestoreLicenseDb == null)
            {
                try
                {
                    string path = Environment.CurrentDirectory + @"\myfinlicense-firebase-adminsdk.json";
                    FirestoreDbBuilder builder = new FirestoreDbBuilder
                    {
                        ProjectId = "myfinlicense",
                        CredentialsPath = path
                    };
                    _firestoreLicenseDb = await builder.BuildAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к Firestore: {ex.Message}");
                }
            }
        }

        private async Task SetupAdminDb()
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\restaurant-admin-app.json";
                FirestoreDbBuilder builder = new FirestoreDbBuilder
                {
                    ProjectId = "restaurant-admin-app-55d2e",
                    CredentialsPath = path
                };
                _firestoreAdminDb = await builder.BuildAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к Firestore: {ex.Message}");
            }
        }

        #region[LICENSE_DB]

        /// <summary>
        /// Обновляет существующую лицензию.
        /// </summary>
        public async Task UpdateFireLicenseAsync(FireLicense license)
        {
            await SetupLicenseDb();
            try
            {
                DocumentReference docRef = _firestoreLicenseDb.Collection("licenses").Document(license.Id);
                await docRef.SetAsync(license, SetOptions.MergeFields(nameof(license.CustomerId)));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления лицензии: {ex.Message}");
            }
        }

        /// <summary>
        /// Получает лицензию по её ID.
        /// </summary>
        public async Task<FireLicense> GetFireLicenseByIdAsync(string licenseId)
        {
            await SetupLicenseDb();
            try
            {
                DocumentReference docRef = _firestoreLicenseDb.Collection("licenses").Document(licenseId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    return snapshot.ConvertTo<FireLicense>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения лицензии: {ex.Message}");
            }
            return null;
        }


        /// <summary>
        /// Получает лицензии по указанным фильтрам (по LicenseKey или Type).
        /// </summary>
        public async Task<List<FireLicense>> GetFireLicensesByFilterAsync(string licenseKey = null, EnumLicenseType? type = null)
        {
            await SetupLicenseDb();
            try
            {
                var licenses = new List<FireLicense>();
                var tasks = new List<Task<QuerySnapshot>>();

                // Условие фильтрации по LicenseKey
                if (!string.IsNullOrEmpty(licenseKey))
                {
                    tasks.Add(_firestoreLicenseDb.Collection("licenses").WhereEqualTo("LicenseKey", licenseKey).GetSnapshotAsync());
                }

                // Условие фильтрации по Type
                if (type.HasValue)
                {
                    tasks.Add(_firestoreLicenseDb.Collection("licenses").WhereEqualTo("Type", type.Value).GetSnapshotAsync());
                }

                // Если не переданы фильтры, выбрасываем исключение
                if (tasks.Count == 0)
                {
                    throw new ArgumentException("Необходимо указать хотя бы один фильтр: licenseKey или type.");
                }

                // Выполняем запросы параллельно
                var snapshots = await Task.WhenAll(tasks);
                var seenIds = new HashSet<string>();

                // Обрабатываем полученные документы
                foreach (var snapshot in snapshots)
                {
                    foreach (var doc in snapshot.Documents)
                    {
                        if (doc.Exists && seenIds.Add(doc.Id))
                        {
                            licenses.Add(doc.ConvertTo<FireLicense>());
                        }
                    }
                }
                return licenses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации лицензий: {ex.Message}");
                return null;
            }
        }
    }
    #endregion
}