using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "CREDITCARDMSGSRSV1")]
    public class Creditcardmsgsrsv1
    {
        [XmlElement(ElementName = "CCSTMTTRNRS")]
        public Ccstmttrnrs CCSTMTTRNRS { get; set; }
    }
}