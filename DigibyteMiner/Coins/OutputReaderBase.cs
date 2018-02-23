using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace OneMiner.Coins
{
    /// <summary>
    /// reads data for  miner
    /// </summary>
    public class OutputReaderBase : IOutputReader
    {
        private const int MAX_QUEUESIZE = 5;

        private object s_accesssynch = new object();
        private object s_resultSynch = new object();
        public string StatsLink { get; set; }
        private string m_Lastlog = "";
        //if true, next time we parse outputs, we will try to read the gpu names again. will reset when new object is made and miner is started
        public bool ReReadGpuNames { get; set; }
        public Queue<string> m_AllLogs = new Queue<string>();
        MinerDataResult m_Result = new MinerDataResult();
        public MinerDataResult MinerResult
        {
            get
            {
                lock (s_resultSynch)
                {
                    try
                    {
                        return m_Result;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                        return null;
                    }
                }
            }
            set
            {
                lock (s_resultSynch)
                {
                    try
                    {
                        m_Result = value;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                        m_Lastlog = null;
                    }
                }
            }
        }
        public string LastLog
        {
            get
            {
                lock (s_accesssynch)
                {
                    try
                    {
                        return m_Lastlog;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                        return "";
                    }
                }
            }
            set
            {
                lock (s_accesssynch)
                {
                    try
                    {
                        m_Lastlog = value;
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                        m_Lastlog = "";
                    }
                }
            }
        }
        public string NextLog
        {
            get
            {
                lock (s_accesssynch)
                {
                    try
                    {
                        return m_AllLogs.Dequeue();
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                        return "";
                    }
                }
            }
            set
            {
                lock (s_accesssynch)
                {
                    try
                    {
                        if (value != null && value != "")
                        {
                            LastLog = value;
                            m_AllLogs.Enqueue(value);
                            if (m_AllLogs.Count >= MAX_QUEUESIZE)//if consumer is slower than producer, then we need to remove old vals
                                m_AllLogs.Dequeue();
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError(e.ToString());
                    }
                }
            }
        }

        public OutputReaderBase(string link)
        {
            StatsLink = link;
            ReReadGpuNames = true;

        }



        public void ReadWithBrowser()
        {
            try
            {
                DownloadRequest request = new DownloadRequest();
                request.LINK = StatsLink;
                request.Reader = this;
                Factory.Instance.ViewObject.DownloadRequestQueue.Enqueue(request);
            }
            catch (Exception e)
            {
            }
        }


        public void Read()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(StatsLink);
                request.Method = "GET";
                request.KeepAlive = false;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                request.Accept = "/";
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //doc.Save(request.GetRequestStream());
                HttpWebResponse resp = request.GetResponse() as HttpWebResponse;
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string s = sr.ReadToEnd();
                NextLog = s;
            }
            catch (Exception e)
            {
                ReadWithBrowser();
                throw;
            }

        }

        public void AlarmRaised()
        {
            try
            {
                Read();
                Parse();
            }
            catch (Exception e)
            {
            }

        }

        public virtual void Parse()
        {
            throw new NotImplementedException();

        }

    }
}
