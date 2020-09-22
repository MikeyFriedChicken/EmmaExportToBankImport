using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "AVAILBAL")]
    public class Availbal
    {
        [XmlElement(ElementName = "BALAMT")] public string BALAMT { get; set; }

        [XmlElement(ElementName = "DTASOF")] public string DTASOF { get; set; }
    }
}