using System.Collections.Generic;
using System.Xml.Serialization;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Raw
{
    [XmlRoot(ElementName = "BANKTRANLIST")]
    public class Banktranlist
    {
        [XmlElement(ElementName = "DTSTART")] public string DTSTART { get; set; }

        [XmlElement(ElementName = "DTEND")] public string DTEND { get; set; }

        [XmlElement(ElementName = "STMTTRN")] public List<Stmttrn> STMTTRN { get; set; }
    }
}