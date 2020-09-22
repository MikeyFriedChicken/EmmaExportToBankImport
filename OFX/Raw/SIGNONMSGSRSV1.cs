using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "SIGNONMSGSRSV1")]
    public class Signonmsgsrsv1
    {
        [XmlElement(ElementName = "SONRS")] public Sonrs SONRS { get; set; }
    }
}