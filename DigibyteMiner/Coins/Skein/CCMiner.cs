using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model;
using DigibyteMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace DigibyteMiner.Coins.Skein
{
    /// <summary>
    /// Resources::
    /// https://www.cryptocurrencyfreak.com/2017/08/13/monero-gpu-mining-ccminer-2-2/
    /// http://cryptomining-blog.com/3987-new-ccminer-1-5-fork-by-tpruvot-with-websocket-api-example-available/
    /// </summary>
    class CCMiner : MinerProgramBase
    {

        public const string STATSLINK2 = "127.0.0.1";
        public const string STATSLINK3 = "";
        public override string MINERURL
        {
            get
            {
                return "https://github.com/arunsatyarth/MinerRepos/releases/download/v0.2/ccminer-alexis.zip";
            }
        }
        public override string EXENAME
        {
            get
            {
                return "ccminer.exe";
            }
        }
        public override string PROCESSNAME
        {
            get
            {
                return "ccminer";
            }
        }
        public override string STATS_LINK
        {
            get
            {
                return STATSLINK2;
            }
        }
        public override string STATS_LINK_HTML
        {
            get
            {
                return "http://" + STATSLINK2;
            }
        }
        public override string STATS_LINK_PORT
        {
            get
            {
                return "4068";
            }
        }


        public override string Script { get; set; }


        public override string Type { get; set; }//claymore ccminer etc

        public override IOutputReader OutputReader { get; set; }


        public CCMiner(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMiner miner) :
            base(mainCoin, dualMining, dualCoin, minerName, miner)
        {
            Type = "Nvidia";
            GPUType = CardMake.Nvidia;
            OutputReader = new CCReader(STATS_LINK, STATS_LINK_PORT);
            MiningIntensityLow = 6;
            MiningIntensityHigh = 35;
            MiningIntensity = 23;
        }

        public override string GenerateScript(bool saveScript)
        {
            try
            {
                if (MiningIntensity == 0)
                    MiningIntensity = 23;  
                string command = EXENAME + " --algo=skein -o " + MainCoinConfigurer.Pool;
                command += " -u " + MainCoinConfigurer.Wallet;
                string pwd = MainCoinConfigurer.Password.Trim();
                if (pwd.Length == 0)
                    pwd = " x ";
                command += " -p " + pwd;
                command += " -i " + MiningIntensity.ToString();

                command += " --api-bind " + STATS_LINK + ":" + STATS_LINK_PORT;


                Script = SCRIPT1 + command;
                AutomaticScriptGeneration = true;
                if (saveScript)
                    SaveScriptToDB();
                return Script;
            }
            catch (Exception e)
            {
                return "";
            }
        }


        private const string SCRIPT1 = "";


        /// <summary>
        /// reads data for claymore miner
        /// </summary>
        public class CCReader : OutputReaderBase
        {
            public CCReader(string link, string port)
                : base(link, port)
            {
            }


            public override void Read()
            {
                try
                {
                    CCMinerCommandOutputs output = new CCMinerCommandOutputs();
                    string result = "";
                    TcpReaderUtil util = new TcpReaderUtil(StatsLink, StatsPort);
                    result = util.GetData("summary");
                    output.Summary = result;

                    result = util.GetData("threads");
                    output.Threads = result;

                    string str = new JavaScriptSerializer().Serialize(output);


                    NextLog = str;
                }
                catch (Exception e)
                {
                    ReadWithBrowser();
                    throw;
                }

            }
            public override void Parse()
            {
                try
                {
                    CCMinerCommandOutputs minerResult = (CCMinerCommandOutputs)new JavaScriptSerializer().Deserialize(LastLog, typeof(CCMinerCommandOutputs));
                    if (minerResult != null)
                    {
                        if (minerResult.Parse(new CcMinerResultParser(LastLog, ReReadGpuNames)))
                        {
                            MinerResult = minerResult.MinerDataResult;
                        }
                    }
                }
                catch (Exception e)
                {
                }
                ReReadGpuNames = false;
            }

            public class CcMinerResultParser : IMinerResultParser
            {
                MinerDataResult m_MinerResult = null;
                CCMinerCommandOutputs m_CCMinerData = null;
                public bool Succeeded { get; set; }//if parsing succeeded without errors
                static Hashtable m_Gpus = new Hashtable();// we only need t read gpu info once as it dosent change with more logs comining in

                string m_fullLog = "";
                public CcMinerResultParser(string fullLog, bool reReadGpunames)
                {
                    m_fullLog = fullLog;
                }

                public bool Parse(object obj)
                {
                    Succeeded = false;
                    m_CCMinerData = obj as CCMinerCommandOutputs;
                    try
                    {
                        if (m_CCMinerData == null)
                            return false;
                        m_MinerResult = new MinerDataResult();
                        m_MinerResult.GPUs = new List<GpuData>();
                        try
                        {
                            string content = m_CCMinerData.Summary;
                            string[] a = content.Split(new string[] { ";" }, StringSplitOptions.None);
                            foreach (string item in a)
                            {
                                if (item.Contains("KHS"))
                                {
                                    string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                    string kh = values[1];
                                    double kh_d = double.Parse(kh);
                                    //kh_d /= 1024;
                                    m_MinerResult.TotalHashrate = (int)kh_d;

                                }
                                else if (item.Contains("ACC"))
                                {
                                    string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                    string acc = values[1];
                                    int acc_i = int.Parse(acc);
                                    m_MinerResult.TotalShares = acc_i;

                                }
                                else if (item.Contains("REJ"))
                                {
                                    string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                    string rej = values[1];
                                    int rej_i = int.Parse(rej);
                                    m_MinerResult.Rejected = rej_i;
                                }

                            }

                        }
                        catch (Exception e)
                        {
                        }
                        //now read gpus from dev
                        try
                        {
                            string content = m_CCMinerData.Threads;
                            string[] b = content.Split(new string[] { "|" }, StringSplitOptions.None);

                            foreach (var ice in b)
                            {
                                bool add = false;
                                GpuData gpu = new GpuData("");//Todo: finfing the name has proven difficult
                                gpu.Make = CardMake.Nvidia;
                                //m_MinerResult.GPUs.Add(gpu);
                                string[] a = ice.Split(new string[] { ";" }, StringSplitOptions.None);
                                foreach (string item in a)
                                {
                                    if (item.Contains("CARD"))
                                    {
                                        string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                        gpu.GPUName = values[1];
                                    }
                                    if (item.Contains("FAN"))
                                    {
                                        string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                        gpu.FanSpeed = values[1];
                                    }
                                    if (item.Contains("TEMP"))
                                    {
                                        string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                        gpu.Temperature = values[1];
                                    }
                                    if (item.Contains("KHS"))
                                    {
                                        string[] values = item.Split(new string[] { "=" }, StringSplitOptions.None);
                                        string kh = values[1];
                                        double kh_d = double.Parse(kh);
                                        //kh_d /= 1024;
                                        gpu.Hashrate = kh_d.ToString();
                                        add = true;
                                    }
                                }
                                if (add)
                                    m_MinerResult.GPUs.Add(gpu);

                            }
                        }
                        catch (Exception e)
                        {
                        }
                        m_CCMinerData.MinerDataResult = m_MinerResult;
                        return true;
                    }
                    catch (Exception e)
                    {
                        Succeeded = false;
                    }
                    return false;

                }



            }
        }
        public class Result
        {
            public int gpuid { get; set; }
            public int cudaid { get; set; }
            public string busid { get; set; }
            public string name { get; set; }
            public int gpu_status { get; set; }
            public int solver { get; set; }
            public int temperature { get; set; }
            public int gpu_power_usage { get; set; }
            public int speed_sps { get; set; }
            public int accepted_shares { get; set; }
            public int rejected_shares { get; set; }
            public int start_time { get; set; }
        }

        public class CCMinerData
        {
            public MinerDataResult MinerDataResult { get; set; }
            public string method { get; set; }
            public object error { get; set; }
            public int start_time { get; set; }
            public string current_server { get; set; }
            public int available_servers { get; set; }
            public int server_status { get; set; }
            public List<Result> result { get; set; }
            public bool Parse(IMinerResultParser parser)
            {
                return parser.Parse(this);
            }
        }
        public class CCMinerCommandOutputs
        {
            public string Summary { get; set; }
            public string Threads { get; set; }
            //following onjects are used later
            public MinerDataResult MinerDataResult { get; set; }

            public bool Parse(IMinerResultParser parser)
            {
                return parser.Parse(this);
            }

        }

    }

}
