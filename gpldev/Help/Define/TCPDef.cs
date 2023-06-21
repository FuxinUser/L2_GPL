namespace Core.Define
{
    public class TCPDef
    {
        public enum Statuts
        {
            /* 發生錯誤 */
            Error,
            /* 已關閉 */
            Closed,
            /* 正在連線中 */
            Connectiong,
            /* 連線已開啟 */
            Open,
        }

    }
}
