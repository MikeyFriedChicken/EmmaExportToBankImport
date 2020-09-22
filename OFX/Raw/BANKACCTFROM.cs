using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "BANKACCTFROM")]
    public class Bankacctfrom
    {
        [XmlElement(ElementName = "BANKID")] public string BANKID { get; set; }
        [XmlElement(ElementName = "ACCTID")] public string ACCTID { get; set; }
        [XmlElement(ElementName = "ACCTTYPE")] public string ACCTTYPE { get; set; }
    }
}