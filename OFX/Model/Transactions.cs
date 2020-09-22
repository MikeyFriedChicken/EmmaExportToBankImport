using System;
using System.Collections.Generic;
using System.Linq;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Model
{
    public class Transactions
    {
        public List<Transaction> Items = new List<Transaction>();

        public Transactions(AccountType accountType)
        {
            ACCOUNT_TYPE = accountType;
        }

        public string CURRENCY { get; set; }
        public DateTime END_DATE { get; set; }
        public string ACCOUNT_ID { get; set; }
        public DateTime START_DATE { get; set; }

        public AccountType ACCOUNT_TYPE { get; set; }

        public void Add(string transactionType, string transactionAmount, string description, DateTime datePosted,
            string memo)
        {
            var transaction = new Transaction();
            transaction.TransactionType = transactionType;
            transaction.DatePosted = datePosted;
            transaction.TransactionAmount = transactionAmount;
            transaction.TransactionId = datePosted.ToString("yyyyMMdd" + Items.Count);
            transaction.Description = description;
            transaction.MEMO = memo;

            Items.Add(transaction);
        }

        public void Add(Transaction transaction)
        {
            Items.Add(transaction);
        }

        public void SetDatesFromData()
        {
            if (Items.Count > 0)
            {
                START_DATE = Items.Min(x => x.DatePosted);
                END_DATE = Items.Max(x => x.DatePosted);
            }
        }
    }
}