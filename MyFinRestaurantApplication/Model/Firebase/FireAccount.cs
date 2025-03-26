using System.Collections.Generic;
using Google.Cloud.Firestore;
using ManagerApplication.Helper;

namespace ManagerApplication.Model
{
    [FirestoreData]
    public class FireAccount
    {
        [FirestoreProperty]
        public int AccountId { get; set; }

        [FirestoreProperty]
        public string AccountName { get; set; }

        [FirestoreProperty]
        public double AccountBalance { get; set; }

        [FirestoreProperty]
        public EnumMoneyType AccountMoneyType { get; set; }

        [FirestoreProperty]
        public string AccountLocation { get; set; } = "-";

        // Parameterless constructor required by Firestore
        public FireAccount()
        {
        }

        // Constructor to convert Cassa to FireCassa
        public FireAccount(Cassa cassa)
        {
            AccountId = cassa.cassa_id;
            AccountName = cassa.cassa_name;
            AccountBalance = cassa.cassa_money;
            AccountMoneyType = EnumMoneyType.Наличный;
            AccountLocation = "Zarafshon";
        }

        // Static method to convert List<Cassa> to List<FireCassa>
        public static List<FireAccount> ConvertToFireCassas(List<Cassa> cassas)
        {
            List<FireAccount> fireCassas = new List<FireAccount>();

            foreach (var cassa in cassas)
            {
                fireCassas.Add(new FireAccount(cassa));
            }

            return fireCassas;
        }
    }
}
