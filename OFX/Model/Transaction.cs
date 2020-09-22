using System;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Model
{
    public class Transaction
    {
        public DateTime DatePosted;
        public string Description;
        public string TransactionAmount;
        public string TransactionId;
        public string TransactionType;
        public string MEMO { get; set; }
    }
}