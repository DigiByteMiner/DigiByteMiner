using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core
{
    class TSList<T>
    {
         private object s_accesssynch = new object();

        //expose them with funs
        public List<T> List { get; set; }

        public T this[int index]
        {
            get
            {
                return List[index];
            }
        } 

        public void Add(T program)
        {
            lock (s_accesssynch)
            {
                try
                {
                    List.Add(program);
                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
            }
        }
        public void Remove(T program)
        {
            lock (s_accesssynch)
            {
                try
                {
                     List.Remove(program);
                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
            }
        }
        public void Clear()
        {
            lock (s_accesssynch)
            {
                try
                {
                    List.Clear();
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
                        return List.Count;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                    }
                    return 0;
                }
            }
        }

        public  TSList()
        {
            List = new List<T>();
        }
    }
}
