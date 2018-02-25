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
            OutputReader = new CCReader(STATS_LINK, STATS_LINK_PORT);
        }

        public override string GenerateScript()
        {
            try
            {

                string command = EXENAME + " --skein -o " + MainCoinConfigurer.Pool;
                command += " -u " + MainCoinConfigurer.Wallet;
                string pwd = MainCoinConfigurer.Password.Trim();
                if (pwd.Length == 0)
                    pwd = " x ";
                command += " -p " + pwd;

                command += " --api-listen ";


                Script = SCRIPT1 + command;
                AutomaticScriptGeneration = true;
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
        public class CCReader : OutputReaderBase
        {
            public CCReader(string link,string port)
                : base(link,port)
            {
            }
            CGMinerCommandOutputs GetResultsSection(string innerText)
            {
                try
                {
                    CGMinerCommandOutputs minerResult = (CGMinerCommandOutputs)new JavaScriptSerializer().Deserialize(innerText, typeof(CGMinerCommandOutputs));
                    return minerResult;
                }
                catch (Exception e)
                {
                }
                return null;
            }
            public override void Read()
            {
                try
                {
                    CGMinerCommandOutputs output = new CGMinerCommandOutputs();
                    string result = "";
                    TcpReaderUtil util = new TcpReaderUtil(StatsLink, StatsPort);
                    result = util.GetData("summary");
                    output.Summary = result;

                    result = util.GetData("devs");
                    output.Devs = result;

                    result = util.GetData("devdetails");
                    output.Devdetails = result;

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
                            SummaryRoot minerResult = (SummaryRoot)new JavaScriptSerializer().Deserialize(m_CgminerData.Summary, typeof(SummaryRoot));
                            m_MinerResult.TotalHashrate = (int)minerResult.SUMMARY[0].MHS5s;
                            m_MinerResult.TotalShares = (int)minerResult.SUMMARY[0].Accepted;
                            m_MinerResult.Rejected = (int)minerResult.SUMMARY[0].Rejected;

                        }
                        catch (Exception e)
                        {
                        }
                        //now read gpus from dev
                        try
                        {
                            DEVSROOT minerResult = (DEVSROOT)new JavaScriptSerializer().Deserialize(m_CgminerData.Devs, typeof(DEVSROOT));
                            foreach (D1 item in minerResult.DEVS)
	                        {
                                GpuData gpu = new GpuData(item.GPU.ToString());//Todo: finfing the name has proven difficult
                                gpu.Make = CardMake.Amd;
                                gpu.Hashrate = item.MHS_5s.ToString();
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
        public class S1
        {
            public string STATUS { get; set; }
            public int When { get; set; }
            public int Code { get; set; }
            public string Msg { get; set; }
            public string Description { get; set; }
        }

        public class D1
        {
            public int GPU { get; set; }
            public string Enabled { get; set; }
            public string Status { get; set; }
            public double Temperature { get; set; }
            public int Fan_Speed { get; set; }
            public int Fan_Percent { get; set; }
            public int GPU_Clock { get; set; }
            public int Memory_Clock { get; set; }
            public double GPU_Voltage { get; set; }
            public int GPU_Activity { get; set; }
            public int Powertune { get; set; }
            public double MHS_av { get; set; }
            public double MHS_5s { get; set; }
            public int Accepted { get; set; }
            public int Rejected { get; set; }
            public int Hardware_Errors { get; set; }
            public double Utility { get; set; }
            public string Intensity { get; set; }
            public int Last_Share_Pool { get; set; }
            public int Last_Share_Time { get; set; }
            public double Total_MH { get; set; }
            public int Diff1_Work { get; set; }
            public double Difficulty_Accepted { get; set; }
            public double Difficulty_Rejected { get; set; }
            public double Last_Share_Difficulty { get; set; }
            public int Last_Valid_Work { get; set; }
            public double Device_Hardware { get; set; }
            public double Device_Rejected { get; set; }
            public int Device_Elapsed { get; set; }
        }

        public class DEVSROOT
        {
            public List<S1> STATUS { get; set; }
            public List<D1> DEVS { get; set; }
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
