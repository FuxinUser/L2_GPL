using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 發送Que
/// </summary>
namespace DataModel.Common
{
    public class SendQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();
        public int Count => _queue.Count;
        public void EnQueue(T data)
        {
            _queue.Enqueue(data);
        }
        public bool TryDeQueue(out T data)
        {
            bool rtn = false;
            data = default(T);
            if (_queue.Count > 0)
            {
                rtn = true;
                data = _queue.Dequeue();
            }
            return rtn;
        }
        public void TryDeQueue()
        {
            if (_queue.Count > 0) _queue.Dequeue();
        }
        public bool TryPeek(out T data)
        {
            bool rtn = false;
            data = default(T);
            if (_queue.Count > 0)
            {
                rtn = true;
                data = _queue.Peek();
            }
            return rtn;
        }
    }
}
