using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DigibyteMiner.Core
{
    /// <summary>
    /// Thread safe logger
    /// </summary>
    public class Logger : ILogger
    {
        private static ILogger s_obj = null;
        private static object s_singletonsynch = new object();
        private static object s_accesssynch = new object();
        private bool m_logValid = false;
        AppData m_Logfolder = null;
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
                try
                {
                    string message = GetMessage(error);
                    Log("Info   "+message);
#if DEBUG
                    //MessageBox.Show(message);
#endif
                }
                catch (Exception e)
                {
                }
            }
        }

        public void LogError(string error)
        {
            lock (s_accesssynch)
            {
                try
                {
                    string message = GetMessage(error);
                    Log("Error   " + message);

#if DEBUG
                    //MessageBox.Show(message);
#endif
                }
                catch (Exception e)
                {
                }
            }
        }
        private void Log(string msg)
        {
            if(m_logValid)
            {
                string filename = m_Logfolder.FileName;
                FileStream stream = File.Open(filename, FileMode.Append);
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine(msg);
                sw.Close();

            }
        }
        private Logger()
        {
            m_Logfolder = new AppData("DigibyteMiner", "digibyteminer.log");
            m_logValid = m_Logfolder.Verify();
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
