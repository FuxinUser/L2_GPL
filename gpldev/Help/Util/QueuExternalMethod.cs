using System.Collections.Concurrent;

namespace Core.Util
{
    public static class QueuExternalMethod
    {
        public static void Clear<T> (this ConcurrentQueue<T> _CQ)
        {
            T TempItem;
            while (_CQ.TryDequeue(out TempItem)) ;
        }

    }
}
