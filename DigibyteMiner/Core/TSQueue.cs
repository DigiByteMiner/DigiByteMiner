using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    //thread safe abstraction of miner and downloading queues as .Net queue is not thread safe
    public class TSQueue<T>
    {
        private object s_accesssynch = new object();

        //expose them with funs
        public Queue<T> Queue { get; set; }

        public void Enqueue(T program)
        {
            lock (s_accesssynch)
            {
                try
                {
                    Queue.Enqueue(program);
                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
            }
        }
        public T Dequeue()
        {
            lock (s_accesssynch)
            {
                try
                {
                    return Queue.Dequeue();
                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
                return default(T);
            }
        }
        public void Clear()
        {
            lock (s_accesssynch)
            {
                try
                {
                    Queue.Clear();
                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
            }
        }
        public int Count
        {
            get
            {
                lock (s_accesssynch)
                {
                    try
                    {
                        return Queue.Count;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                    }
                    return 0;
                }
            }
        }

        public  TSQueue()
        {
            Queue = new Queue<T>();
        }


    }
}
