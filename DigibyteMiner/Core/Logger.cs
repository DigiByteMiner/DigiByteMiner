using OneMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OneMiner.Core
{
    /// <summary>
    /// Thread safe logger
    /// </summary>
    public class Logger : ILogger
    {
        private static ILogger s_obj = null;
        private static object s_singletonsynch = new object();
        private static object s_accesssynch = new object();
        public string GetMessage(string msg)
        {
            string message = "";
            try
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                message = "Thread: " + threadId.ToString() + ": " + msg;
            }
            catch (Exception e)
            {
            }
            return message;
        }
        public void LogInfo(string error)
        {
            lock (s_accesssynch)
            {
                //logging code
            }
        }

        public void LogError(string error)
        {
            lock (s_accesssynch)
            {
                try
                {
                    string message = GetMessage(error);
#if DEBUG
                    //MessageBox.Show(message);
#endif
                }
                catch (Exception e)
                {
                }
            }
        }

        private Logger()
        {
        }

        public static ILogger Instance
        {
            get
            {
                if (s_obj != null)
                    return s_obj;
                else
                {
                    lock (s_singletonsynch)//Locking singleton
                    {
                        if (s_obj == null)
                            s_obj = new Logger();
                        return s_obj;
                    }

                }
            }
        }
    }
}
