using System;

namespace DataMod.Printer
{
    public class ZebraStatus
    {        
        public bool IsConnected { get; set; }
        public bool IsPaperOut { get; set; }
        public bool IsPause { get; set; }
        public bool IsRibbonOut { get; set; }
        public bool IsError { get; set; }
        public bool IsWarning { get; set; }
        public int ErrorNum { get; set; }
        public int WarningNum { get; set; }
        public DateTime CheckDT { get; set; }
    }
}
