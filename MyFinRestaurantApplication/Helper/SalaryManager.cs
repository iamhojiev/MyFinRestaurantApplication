using ManagerApplication.Model;
using System;

namespace ManagerApplication.Helper
{
    public static class SalaryManager
    {
        public static bool CheckForMonthlyPayment(User user)
        {
            // Проверяем, прошел ли месяц с последней выплаты
            if (DateTime.TryParse(user.user_last_payment, out DateTime lastPaymentDate))
            {
                if ((DateTime.Now - lastPaymentDate).Days >= 30)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckForDailyPayment(User user)
        {
            // Проверяем, прошел ли месяц с последней выплаты
            if (DateTime.TryParse(user.user_last_payment, out DateTime lastPaymentDate))
            {
                if ((DateTime.Now - lastPaymentDate).Days >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        public static async void AddToUserBalance(User user, double amount, string description)
        {
            user.user_earnings += amount;
            await new User().OnUpdateAsync(user);

            CreateSalaryLog(user.user_id, amount, description);
        }

        public static async void GiveUserSalary(User user, double amount, string description)
        {
            user.user_earnings += amount;
            user.user_last_payment = DateTime.Now.ToShortDateString();
            await new User().OnUpdateAsync(user);

            CreateSalaryLog(user.user_id, amount, description);
        }

        private static async void CreateSalaryLog(int userId, double amount, string description)
        {
            await BalanceSystem.Instance.AddSalaryOperation(EnumSalaryLogType.Начисление, amount, userId, description);
        }
    }
}
