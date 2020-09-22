using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "LEDGERBAL")]
    public class Ledgerbal
    {
        [XmlElement(ElementName = "BALAMT")] public string BALAMT { get; set; }

        [XmlElement(ElementName = "DTASOF")] public string DTASOF { get; set; }
    }
}