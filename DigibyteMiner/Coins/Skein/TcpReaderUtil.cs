using DigibyteMiner.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DigibyteMiner.Coins.Skein
{
    class TcpReaderUtil
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public TcpReaderUtil(string ip, string port)
        {
            IP = ip;
            Port = port;
        }

        public string GetData(string command)
        {
            int timeoutInMS = 1000;

            string output = "";
            Stream s = default(Stream);
            TcpClient tcpClient = default(TcpClient);

            try
            {
                tcpClient = new TcpClient();
                tcpClient.SendTimeout = timeoutInMS;
                tcpClient.ReceiveTimeout = timeoutInMS;
                tcpClient.Connect(IP, int.Parse(Port));
                s = tcpClient.GetStream();
                if (s != null)
                {
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true;

                    //sw.WriteLine("summary");
                    sw.WriteLine(command);

                    output = sr.ReadToEnd();
                }

            }
            catch(Exception e)
            {
                Logger.Instance.LogError("TcpReaderUtil::GetData: " + e.ToString());
            }
            finally
            {
                tcpClient.Close();
                if (s != null)
                {
                    s.Close();
                }
            }
            return output;
        }
    }
}
