using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.Model;
using OneMiner.Model.Config;
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

namespace OneMiner.Coins.Equihash
{
    /// <summary>
    /// this class does not represent a miner program. coz this contains specif info like batfilepath etc
    /// this represents a miner program inside a configured miner. there could be many miners of the same type. eg ethereum, ethereum_sia
    /// for the real representation fo a miner program, look at JsonData.MinerProgram
    /// </summary>
    class EWBFMiner : MinerProgramBase
    {

        public const string STATSLINK2 = "127.0.0.1:12345";
        public const string STATSLINK3 = "/getstat";
        public override string MINERURL
        {
            get
            {
                return "https://github.com/nanopool/ewbf-miner/releases/download/v0.3.4b/Zec.miner.0.3.4b.zip";
            }
        }
        public override string EXENAME
        {
            get
            {
                return "miner.exe";
            }
        }
        public override string PROCESSNAME
        {
            get
            {
                return "miner";
            }
        }
        public override string STATS_LINK
        {
            get
            {
                return "http://" + STATSLINK2 + STATSLINK3;
            }
        }
        public override string STATS_LINK_HTML
        {
            get
            {
                return "http://" + STATSLINK2;
            }
        }


        public override string Script { get; set; }


        public override string Type { get; set; }//claymore ccminer etc

        public override IOutputReader OutputReader { get; set; }


        public EWBFMiner(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMiner miner) :
            base(mainCoin, dualMining, dualCoin, minerName, miner)
        {
            Type = "Nvidia";
            GPUType = CardMake.Nvidia;
            OutputReader = new EWBFReader(STATS_LINK);
        }



        public override string GenerateScript()
        {
            try
            {
                string host = "", port = "";
                try
                {
                    string url=MainCoinConfigurer.Pool;
                    if (!url.Contains("://"))
                        url = "ssl://" + url;
                    var uri = new Uri(url);
                    host = uri.Host;
                    port = uri.Port.ToString();
                }
                catch (Exception e)
                {
                    host = ""; port = "";
                }
                //var host = uri.Host;
                //generate script and write to folder
                string command = EXENAME + " --server " + host;
                command += " --user " + MainCoinConfigurer.Wallet;
                command += " --pass z ";
                if (DualCoin != null)
                {
                    //dualcoin not supported rite now for zcash
                    command += "";
                }
                command += " --port " + port;
                command += " --api " + STATSLINK2;


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




        private const string SCRIPT1 = "";


        /// <summary>
        /// reads data for claymore miner
        /// </summary>
        public class EWBFReader : OutputReaderBase
        {
            public EWBFReader(string link)
                : base(link)
            {
            }
            EWBFData GetResultsSection(string innerText)
            {
                try
                {
                    EWBFData minerResult = (EWBFData)new JavaScriptSerializer().Deserialize(innerText, typeof(EWBFData));
                    return minerResult;
                }
                catch (Exception e)
                {
                }
                return null;
            }
            public override void Parse()
            {
                EWBFData ewbfData = GetResultsSection(LastLog);
                if (ewbfData.Parse(new EWBFReaderResultParser(LastLog, ReReadGpuNames)))
                {
                    MinerResult = ewbfData.MinerDataResult;
                }
                ReReadGpuNames = false;
            }

            public class EWBFReaderResultParser : IMinerResultParser
            {
                MinerDataResult m_MinerResult = null;
                EWBFData m_EwbfData = null;
                public bool Succeeded { get; set; }//if parsing succeeded without errors
                static Hashtable m_Gpus = new Hashtable();// we only need t read gpu info once as it dosent change with more logs comining in

                string m_fullLog = "";
                public EWBFReaderResultParser(string fullLog, bool reReadGpunames)
                {
                    m_fullLog = fullLog;
                }

                public bool Parse(object obj)
                {
                    Succeeded = false;

                    m_EwbfData = obj as EWBFData;
                    try
                    {
                        if (m_EwbfData == null)
                            return false;

                        ComputeGPUData();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Succeeded = false;
                    }
                    return false;

                }

                public void ComputeGPUData()
                {
                    try
                    {
                        m_MinerResult = new MinerDataResult();
                        m_MinerResult.GPUs = new List<GpuData>();

                        int totalHashrate = 0,totalShares=0,rejected=0;
                        foreach (Result item in m_EwbfData.result)
                        {
                            GpuData gpu = new GpuData(item.name);
                            gpu.IdentifyMake();

                            gpu.Hashrate = item.speed_sps.ToString();
                            gpu.Temperature = item.temperature+ "C";
                            m_MinerResult.GPUs.Add(gpu);
                            totalHashrate += item.speed_sps;
                            totalShares += item.accepted_shares;
                            rejected += item.rejected_shares;
                        }

                        m_MinerResult.TotalHashrate = totalHashrate;
                        m_MinerResult.TotalShares = totalShares;
                        m_MinerResult.Rejected = rejected;

                        m_EwbfData.MinerDataResult = m_MinerResult;

                    }
                    catch (Exception)
                    {
                        Succeeded = false;
                        throw;
                    }
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

        public class EWBFData
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
        
    }

}
