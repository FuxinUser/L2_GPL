namespace LabelPrint.Model
{
    public class PrinterStatus
    {
        // 連線
        public bool IsConnected;
        // 暫停狀態
        public bool IsPause;
        // 缺紙
        public bool IsPaperOut;
        // 炭帶
        public bool IsRibbonOut;
        // 列印錯誤
        public bool IsError;
        // 列印警告
        public bool IsWarning;
        // 錯誤碼
        public int ErrorNum;
        // 警告碼
        public int WarningNum;

        public PrinterStatus()
        {

        }
    }
}
