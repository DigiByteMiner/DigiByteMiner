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
    class CGMiner : MinerProgramBase
    {

        public const string STATSLINK2 = "127.0.0.1";
        public const string STATSLINK3 = "";
        public override string MINERURL
        {
            get
            {
                return "https://github.com/arunsatyarth/MinerRepos/releases/download/v0.2/cgminer_skein.zip";
            }
        }
        public override string EXENAME
        {
            get
            {
                return "cgminer.exe";
            }
        }
        public override string PROCESSNAME
        {
            get
            {
                return "cgminer";
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
                return "4028";
            }
        }


        public override string Script { get; set; }


        public override string Type { get; set; }//claymore ccminer etc

        public override IOutputReader OutputReader { get; set; }


        public CGMiner(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMiner miner) :
            base(mainCoin, dualMining, dualCoin, minerName, miner)
        {
            Type = "AMD";
            GPUType = CardMake.Amd;
            OutputReader = new CGReader(STATS_LINK, STATS_LINK_PORT);
            MiningIntensityLow = 2;
            MiningIntensityHigh = 19;
            MiningIntensity = 7;
        }

        public override string GenerateScript(bool saveScript)
        {
            try
            {

                string command = EXENAME + " --skein -o " + MainCoinConfigurer.Pool;
                command += " -u " + MainCoinConfigurer.Wallet;
                string pwd = MainCoinConfigurer.Password.Trim();
                if (pwd.Length == 0)
                    pwd = " x ";
                command += " -p " + pwd;
                command += " -I " + MiningIntensity.ToString();

                command += " --api-listen ";


                Script = SCRIPT1 + command;
                AutomaticScriptGeneration = true;
                if(saveScript)
                    SaveScriptToDB();
                return Script;
            }
            catch (Exception e)
            {
                return "";
            }
        }


        private const string SCRIPT1 =
@"setx GPU_MAX_ALLOC_PERCENT 100
setx GPU_USE_SYNC_OBJECTS 1
";



        /// <summary>
        /// reads data for claymore miner
        /// </summary>
        public class CGReader : OutputReaderBase
        {
            public CGReader(string link, string port)
                : base(link,port)
            {
            }

            public override void Read()
            {
                try
                {
                    CGMinerCommandOutputs output = new CGMinerCommandOutputs();
                    string result = "";
                    TcpReaderUtil util = new TcpReaderUtil(StatsLink, StatsPort);
                    result = util.GetData("{\"command\":\"summary\"}");
                    output.Summary = result;

                    result = util.GetData("{\"command\":\"devs\"}");
                    output.Devs = result;

                    result = util.GetData("{\"command\":\"devdetails\"}");
                    output.Devdetails = result;

                    string str = new JavaScriptSerializer().Serialize(output);


                    NextLog = str;
                }
                catch (Exception e)
                {
                    throw;
                }

            }
            public override void Parse()
            {
                try
                {
                    CGMinerCommandOutputs minerResult = (CGMinerCommandOutputs)new JavaScriptSerializer().Deserialize(LastLog, typeof(CGMinerCommandOutputs));
                    if (minerResult!=null)
                    {
                        if (minerResult.Parse(new CGMinerResultParser(LastLog, ReReadGpuNames)))
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

            public class CGMinerResultParser : IMinerResultParser
            {
                MinerDataResult m_MinerResult = null;
                CGMinerCommandOutputs m_CgminerData = null;
                public bool Succeeded { get; set; }//if parsing succeeded without errors
                static Hashtable m_Gpus = new Hashtable();// we only need t read gpu info once as it dosent change with more logs comining in

                string m_fullLog = "";
                public CGMinerResultParser(string fullLog, bool reReadGpunames)
                {
                    m_fullLog = fullLog;
                }

                public bool Parse(object obj)
                {
                    Succeeded = false;
                    m_CgminerData = obj as CGMinerCommandOutputs;
                    try
                    {
                        if (m_CgminerData == null)
                            return false;
                        m_MinerResult = new MinerDataResult();
                        m_MinerResult.GPUs = new List<GpuData>();
                        try
                        {
                            string content = m_CgminerData.Summary.Replace(" ", "");
                            content = content.Substring(0, content.Length - 1);
                            SummaryRoot minerResult = (SummaryRoot)new JavaScriptSerializer().Deserialize(content, typeof(SummaryRoot));
                            m_MinerResult.TotalHashrate = (int)minerResult.SUMMARY[0].MHS5s;
                            m_MinerResult.TotalHashrate *= 1000;
                            m_MinerResult.TotalShares = (int)minerResult.SUMMARY[0].Accepted;
                            m_MinerResult.Rejected = (int)minerResult.SUMMARY[0].Rejected;

                        }
                        catch (Exception e)
                        {
                        }
                        //now read gpus from dev

                        try
                        {

                            string content = m_CgminerData.Devs.Replace(" ", "");
                            content = content.Substring(0, content.Length - 1);

                            RootObject minerResult = (RootObject)new JavaScriptSerializer().Deserialize(content, typeof(RootObject));
                            foreach (DEV item in minerResult.DEVS)
	                        {
                                GpuData gpu = new GpuData("AMD GPU "+item.GPU.ToString());//Todo: finfing the name has proven difficult
                                gpu.Make = CardMake.Amd;
                                gpu.Hashrate = (item.MHS5s*1000).ToString();//convert to Khs
                                gpu.Temperature = item.Temperature.ToString()+"C";
                                m_MinerResult.GPUs.Add(gpu);

	                        } 
                        }
                        catch (Exception e)
                        {
                        }
                        m_CgminerData.MinerDataResult = m_MinerResult;
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
        #region DEVS_CALL_JSOn_STRUCTURE
        public class S
        {
            public string STATUS { get; set; }
            public int When { get; set; }
            public int Code { get; set; }
            public string Msg { get; set; }
            public string Description { get; set; }
        }

        public class DEV
        {
            public int GPU { get; set; }
            public string Enabled { get; set; }
            public string Status { get; set; }
            public double Temperature { get; set; }
            public int FanSpeed { get; set; }
            public int FanPercent { get; set; }
            public int GPUClock { get; set; }
            public int MemoryClock { get; set; }
            public double GPUVoltage { get; set; }
            public int GPUActivity { get; set; }
            public int Powertune { get; set; }
            public double MHSav { get; set; }
            public double MHS5s { get; set; }
            public int Accepted { get; set; }
            public int Rejected { get; set; }
            public int HardwareErrors { get; set; }
            public double Utility { get; set; }
            public string Intensity { get; set; }
            public int LastSharePool { get; set; }
            public int LastShareTime { get; set; }
            public double TotalMH { get; set; }
            public int Diff1Work { get; set; }
            public double DifficultyAccepted { get; set; }
            public double DifficultyRejected { get; set; }
            public double LastShareDifficulty { get; set; }
            public int LastValidWork { get; set; }
            public double DeviceHardware { get; set; }
            public double DeviceRejected { get; set; }
            public int DeviceElapsed { get; set; }
        }

        public class RootObject
        {
            public List<S> STATUS { get; set; }
            public List<DEV> DEVS { get; set; }
            public int id { get; set; }
        }
        #endregion

        #region SUMMARY_CALL_JSOn_STRUCTURE
        public class S2
        {
            public string STATUS { get; set; }
            public int When { get; set; }
            public int Code { get; set; }
            public string Msg { get; set; }
            public string Description { get; set; }
        }

        public class Sum
        {
            public int Elapsed { get; set; }
            public double MHSav { get; set; }
            public double MHS5s { get; set; }
            public int FoundBlocks { get; set; }
            public int Getworks { get; set; }
            public int Accepted { get; set; }
            public int Rejected { get; set; }
            public int HardwareErrors { get; set; }
            public double Utility { get; set; }
            public int Discarded { get; set; }
            public int Stale { get; set; }
            public int GetFailures { get; set; }
            public int LocalWork { get; set; }
            public int RemoteFailures { get; set; }
            public int NetworkBlocks { get; set; }
            public double TotalMH { get; set; }
            public double WorkUtility { get; set; }
            public double DifficultyAccepted { get; set; }
            public double DifficultyRejected { get; set; }
            public double DifficultyStale { get; set; }
            public int BestShare { get; set; }
            public double DeviceHardware { get; set; }
            public double DeviceRejected { get; set; }
            public double PoolRejected { get; set; }
            public double PoolStale { get; set; }
        }

        public class SummaryRoot
        {
            public List<S2> STATUS { get; set; }
            public List<Sum> SUMMARY { get; set; }
            public int id { get; set; }
        }
        #endregion

        #region DEVDETAILS_CALL_JSOn_STRUCTURE
        #endregion



        public class CGMinerCommandOutputs
        {
            public string Summary { get; set; }
            public string Devs { get; set; }
            public string Devdetails { get; set; }

            //following onjects are used later
            public MinerDataResult MinerDataResult { get; set; }

            public bool Parse(IMinerResultParser parser)
            {
                return parser.Parse(this);
            }
        }

    }

}
