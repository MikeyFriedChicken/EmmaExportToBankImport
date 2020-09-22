using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "BANKMSGSRSV1")]
    public class Bankmsgsrsv1
    {
        [XmlElement(ElementName = "STMTTRNRS")]
        public Stmttrnrs STMTTRNRS { get; set; }
    }
}