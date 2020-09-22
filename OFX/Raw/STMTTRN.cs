using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "STMTTRN")]
    public class Stmttrn
    {
        [XmlElement(ElementName = "TRNTYPE")] public string TRNTYPE { get; set; }

        [XmlElement(ElementName = "DTPOSTED")] public string DTPOSTED { get; set; }

        [XmlElement(ElementName = "TRNAMT")] public string TRNAMT { get; set; }

        [XmlElement(ElementName = "FITID")] public string FITID { get; set; }

        [XmlElement(ElementName = "NAME")] public string NAME { get; set; }

        [XmlElement(ElementName = "MEMO")] public string MEMO { get; set; }
    }
}