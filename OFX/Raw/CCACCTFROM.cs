using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "CCACCTFROM")]
    public class Ccacctfrom
    {
        [XmlElement(ElementName = "ACCTID")] public string ACCTID { get; set; }
    }
}