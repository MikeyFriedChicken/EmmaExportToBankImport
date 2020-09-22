using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MikeyFriedChicken.EmmaExportToBankImport.OFX.Model;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "OFX")]
    public class Ofx
    {
        [XmlElement(ElementName = "SIGNONMSGSRSV1")]
        public Signonmsgsrsv1 SIGNONMSGSRSV1 { get; set; }


        [XmlElement(ElementName = "BANKMSGSRSV1")]
        public Bankmsgsrsv1 BANKMSGSRSV1 { get; set; }


        [XmlElement(ElementName = "CREDITCARDMSGSRSV1")]
        public Creditcardmsgsrsv1 CREDITCARDMSGSRSV1 { get; set; }


        public string ToOfxString()
        {
            var xsSubmit = new XmlSerializer(typeof(Ofx));

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    return PrintXML(sww.ToString()); // Your XML
                }
            }
        }

        public static string PrintXML(string xml)
        {
            string result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(xml);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string formattedXml = sReader.ReadToEnd();

                result = formattedXml;
            }
            catch (XmlException)
            {
                // Handle the exception
            }

            mStream.Close();
            writer.Close();

            return result;
        }

        public static Ofx Create(Transactions transactions, Balance balance)
        {
            var ofx = new Ofx();

            ofx.SIGNONMSGSRSV1 = new Signonmsgsrsv1();
            ofx.SIGNONMSGSRSV1.SONRS = new Sonrs();
            ofx.SIGNONMSGSRSV1.SONRS.STATUS = new Status();
            ofx.SIGNONMSGSRSV1.SONRS.STATUS.CODE = "0";
            ofx.SIGNONMSGSRSV1.SONRS.STATUS.SEVERITY = "INFO";
            ofx.SIGNONMSGSRSV1.SONRS.DTSERVER = DateTime.UtcNow.ToString("yyyyMMddHHmmss"); // 20180704 12 48 49
            ofx.SIGNONMSGSRSV1.SONRS.LANGUAGE = "ENG";
            ofx.SIGNONMSGSRSV1.SONRS.INTU_BID = "00000"; //00508 What is this

            if (transactions.ACCOUNT_TYPE == AccountType.CreditCard)
                PopulateCreditCard(transactions, balance, ofx);
            else
                PopulateBankAccount(transactions, balance, ofx);

            return ofx;
        }

        private static void PopulateCreditCard(Transactions transactions, Balance balance, Ofx ofx)
        {
            ofx.CREDITCARDMSGSRSV1 = new Creditcardmsgsrsv1();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS = new Ccstmttrnrs();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.TRNUID = "1";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.STATUS = new Status();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.STATUS.CODE = "0";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.STATUS.CODE = "INFO";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS = new Ccstmtrs();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.CURDEF = transactions.CURRENCY; // GBP
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.CCACCTFROM = new Ccacctfrom();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.CCACCTFROM.ACCTID =
                transactions.ACCOUNT_ID; // creditcardnymvers
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.BANKTRANLIST = new Banktranlist();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.BANKTRANLIST.DTSTART =
                transactions.START_DATE.ToString("yyyyMMdd"); //"20180611";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.BANKTRANLIST.DTEND =
                transactions.END_DATE.ToString("yyyyMMdd"); // "20180618";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.BANKTRANLIST.STMTTRN = new List<Stmttrn>();

            foreach (var transaction in transactions.Items)
            {
                var item = new Stmttrn();
                item.TRNTYPE = transaction.TransactionType; //"DEBIT";
                item.DTPOSTED = transaction.DatePosted.ToString("yyyyMMdd"); // "20180611";
                item.TRNAMT = transaction.TransactionAmount; // "-4.49";
                item.FITID =
                    transaction.TransactionId; //  "201806110002"; // date + 1 for each transaction, unique ID
                item.NAME = transaction.Description;
                item.MEMO = transaction.MEMO;
                ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.BANKTRANLIST.STMTTRN.Add(item);
            }

            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.LEDGERBAL = new Ledgerbal();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.LEDGERBAL.BALAMT = balance.AMOUNT; //"-4855.60";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.LEDGERBAL.DTASOF = balance.AS_OF.ToString("yyyyMMdd");

            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.AVAILBAL = new Availbal();
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.AVAILBAL.BALAMT = balance.AVAILABLE_AMOUNT; //"-4855.60";
            ofx.CREDITCARDMSGSRSV1.CCSTMTTRNRS.CCSTMTRS.AVAILBAL.DTASOF =
                balance.AVAILABLE_AS_OF.ToString("yyyyMMdd");
        }

        private static void PopulateBankAccount(Transactions transactions, Balance balance, Ofx ofx)
        {
            ofx.BANKMSGSRSV1 = new Bankmsgsrsv1();
            ofx.BANKMSGSRSV1.STMTTRNRS = new Stmttrnrs();
            ofx.BANKMSGSRSV1.STMTTRNRS.TRNUID = "1";
            ofx.BANKMSGSRSV1.STMTTRNRS.STATUS = new Status();
            ofx.BANKMSGSRSV1.STMTTRNRS.STATUS.CODE = "0";
            ofx.BANKMSGSRSV1.STMTTRNRS.STATUS.CODE = "INFO";
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS = new Stmtrs();
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.CURDEF = transactions.CURRENCY; // GBP
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM = new Bankacctfrom();
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.ACCTID =
                transactions.ACCOUNT_ID; // TODO: change to bank account number
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKACCTFROM.BANKID =
                transactions.ACCOUNT_ID; // TODO:  change to sort code
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST = new Banktranlist();
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.DTSTART =
                transactions.START_DATE.ToString("yyyyMMdd"); //"20180611";
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.DTEND =
                transactions.END_DATE.ToString("yyyyMMdd"); // "20180618";
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.STMTTRN = new List<Stmttrn>();

            foreach (var transaction in transactions.Items)
            {
                var item = new Stmttrn();
                item.TRNTYPE = transaction.TransactionType; // "ATM"; TODO: check this is not credit debit
                item.DTPOSTED = transaction.DatePosted.ToString("yyyyMMdd"); // "20180611";
                item.TRNAMT = transaction.TransactionAmount; // "-4.49";
                item.FITID = transaction.TransactionId; //  "201806110002"; // date + 1 for each transaction, unique ID
                item.NAME = transaction.Description;
                item.MEMO = transaction.MEMO;
                ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.BANKTRANLIST.STMTTRN.Add(item);
            }

            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.LEDGERBAL = new Ledgerbal();
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.LEDGERBAL.BALAMT = balance.AMOUNT; //"-4855.60";
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.LEDGERBAL.DTASOF = balance.AS_OF.ToString("yyyyMMdd");

            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.AVAILBAL = new Availbal();
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.AVAILBAL.BALAMT = balance.AVAILABLE_AMOUNT; //"-4855.60";
            ofx.BANKMSGSRSV1.STMTTRNRS.STMTRS.AVAILBAL.DTASOF = balance.AVAILABLE_AS_OF.ToString("yyyyMMdd");
        }
    }
}