using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "STMTTRNRS")]
    public class Stmttrnrs
    {
        [XmlElement(ElementName = "TRNUID")] public string TRNUID { get; set; }
        [XmlElement(ElementName = "STATUS")] public Status STATUS { get; set; }
        [XmlElement(ElementName = "STMTRS")] public Stmtrs STMTRS { get; set; }
    }
}