using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "CCSTMTTRNRS")]
    public class Ccstmttrnrs
    {
        [XmlElement(ElementName = "TRNUID")] public string TRNUID { get; set; }

        [XmlElement(ElementName = "STATUS")] public Status STATUS { get; set; }

        [XmlElement(ElementName = "CCSTMTRS")] public Ccstmtrs CCSTMTRS { get; set; }
    }
}