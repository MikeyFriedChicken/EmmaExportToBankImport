using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "CCSTMTRS")]
    public class Ccstmtrs
    {
        [XmlElement(ElementName = "CURDEF")] public string CURDEF { get; set; }

        [XmlElement(ElementName = "CCACCTFROM")]
        public Ccacctfrom CCACCTFROM { get; set; }

        [XmlElement(ElementName = "BANKTRANLIST")]
        public Banktranlist BANKTRANLIST { get; set; }

        [XmlElement(ElementName = "LEDGERBAL")]
        public Ledgerbal LEDGERBAL { get; set; }

        [XmlElement(ElementName = "AVAILBAL")] public Availbal AVAILBAL { get; set; }
    }
}