using System;
using System.Collections.Generic;
using System.IO;
using MikeyFriedChicken.EmmaExportToBankImport.OFX.Model;

namespace MikeyFriedChicken.EmmaExportToBankImport
{
    /// <summary>
    /// Creates an OFX file from raw emma data
    /// </summary>
    public class OfxCreator
    {
        public static void CreateOfxFile(string name,
            List<List<string>> accountData,
            Dictionary<string, int> mapping,
            string outputPath,
            string prefix,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            Transactions transactions = new Transactions(AccountType.BankAccount);
            transactions.CURRENCY = "GBP";
            transactions.ACCOUNT_ID = name;

            Balance balance = new Balance();

            foreach (var rowData in accountData)
            {
                Transaction transaction = new Transaction();
                var dateString = rowData[mapping["Date"]];
                transaction.DatePosted = DateTime.Parse(dateString);

                if (fromDate.HasValue && toDate.HasValue)
                {
                    if (transaction.DatePosted < fromDate.Value || transaction.DatePosted > toDate.Value)
                    {
                        continue;
                    }
                }


                transaction.Description = rowData[mapping[EmmaColumns.Counterparty]];
                transaction.MEMO = $"{rowData[mapping[EmmaColumns.Merchant]]} {rowData[mapping[EmmaColumns.AdditionalDetails]]} {rowData[mapping[EmmaColumns.Notes]]}"
                    .Trim();
                transaction.TransactionId = rowData[mapping[EmmaColumns.Id]];
                transaction.TransactionAmount = rowData[mapping[EmmaColumns.Amount]];
                transaction.TransactionType = rowData[mapping[EmmaColumns.Type]];

                transactions.Add(transaction);
            }

            transactions.SetDatesFromData();

            var ofx = OFX.Raw.Ofx.Create(transactions, balance);

            var ofxData = ofx.ToOfxString();

            string path = Path.Combine(outputPath, $"{prefix}_{name}.ofx");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, ofxData);
        }
    }
}