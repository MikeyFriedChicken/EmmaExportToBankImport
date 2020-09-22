using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "SONRS")]
    public class Sonrs
    {
        [XmlElement(ElementName = "STATUS")] public Status STATUS { get; set; }

        [XmlElement(ElementName = "DTSERVER")] public string DTSERVER { get; set; }

        [XmlElement(ElementName = "LANGUAGE")] public string LANGUAGE { get; set; }

        [XmlElement(ElementName = "INTU.BID")] public string INTU_BID { get; set; }
    }
}