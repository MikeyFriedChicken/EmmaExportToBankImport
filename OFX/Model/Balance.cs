using System;

namespace MikeyFriedChicken.EmmaExportToBankImport.OFX.Model
{
    public class Balance
    {
        public string AVAILABLE_AMOUNT { get; set; }
        public DateTime AVAILABLE_AS_OF { get; set; }
        public string AMOUNT { get; set; }
        public DateTime AS_OF { get; set; }
    }
}