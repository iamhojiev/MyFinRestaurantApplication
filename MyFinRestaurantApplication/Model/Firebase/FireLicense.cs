using System;
using Google.Cloud.Firestore;
using ManagerApplication.Helper;

namespace ManagerApplication.Model
{
    [FirestoreData]
    public class FireLicense
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = "";

        [FirestoreProperty]
        public string LicenseKey { get; set; } = "";

        [FirestoreProperty]
        public EnumLicenseType Type { get; set; }

        [FirestoreProperty]
        public DateTime ExpirationDate { get; set; }

        [FirestoreProperty]
        public bool IsActive { get; set; }

        // Для связи с клиентом
        [FirestoreProperty]
        public string CustomerId { get; set; } = "";
    }
}
