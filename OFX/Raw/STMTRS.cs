using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "STMTRS")]
    public class Stmtrs
    {
        [XmlElement(ElementName = "CURDEF")] public string CURDEF { get; set; }

        [XmlElement(ElementName = "BANKACCTFROM")]
        public Bankacctfrom BANKACCTFROM { get; set; }

        [XmlElement(ElementName = "BANKTRANLIST")]
        public Banktranlist BANKTRANLIST { get; set; }

        [XmlElement(ElementName = "LEDGERBAL")]
        public Ledgerbal LEDGERBAL { get; set; }

        [XmlElement(ElementName = "AVAILBAL")] public Availbal AVAILBAL { get; set; }
    }
}