namespace LogSender
{
    public interface ILog
    {
        void E(string title, string content);
        void A(string title, string content);
        void I(string title, string content);
        void D(string title, string content);
    }
}
