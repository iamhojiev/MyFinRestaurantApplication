using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ManagerApplication.Database;
using ManagerApplication.Helper;
using RestSharp;

namespace ManagerApplication.Model
{
    public class CassaLog : TransactionLog
    {
        public int transaction_cassa { get; set; } // Для связи с кассой
        public double transaction_cassa_balance { get; set; }
        public string transaction_cassa_description { get; set; }
        public EnumCassaOperationType transaction_cassa_operation { get; set; }  // Тип операции (Пополнение/Снятие)
        public EnumWithdrawalType? transaction_withdrawal_type { get; set; } // Тип снятия (только если CassaOperationType == Снятие)

        public Cassa cassa { get; set; }
        public Card card { get; set; }
        public string GetSourceString
        {
            get
            {
                switch (transaction_withdrawal_type)
                {
                    case EnumWithdrawalType.Безналичными:
                        return card?.card_name ?? "Неизвестная карта";
                    case EnumWithdrawalType.Наличными:
                        return cassa?.cassa_name ?? "Неизвестная касса";
                    default:
                        return "Неизвестный источник";
                }
            }
        }

        public string TransactionCassaOperationString =>
            Enum.IsDefined(typeof(EnumCassaOperationType), transaction_cassa_operation)
                ? transaction_cassa_operation.ToString()
                : "Неизвестная операция";

        public string TransactionWithdrawalTypeString =>
            transaction_withdrawal_type.HasValue && Enum.IsDefined(typeof(EnumWithdrawalType), transaction_withdrawal_type.Value)
                ? transaction_withdrawal_type.Value.ToString()
                : "Неизвестный тип снятия";

        public new async Task<List<CassaLog>> OnLoadTransactionsAsync()
        {
            var req = new RestRequest("/load_cassa_transactions.php");

            var res = await client.GetAsync(req);

            var source = DataSQL.Deserialize<CassaLog>(res.Content);

            return source;
        }
    }
}