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

namespace OneMiner.Coins.CryptoNote
{
    /// <summary>
    /// this class does not represent a miner program. coz this contains specif info like batfilepath etc
    /// this represents a miner program inside a configured miner. there could be many miners of the same type. eg ethereum, ethereum_sia
    /// for the real representation fo a miner program, look at JsonData.MinerProgram
    /// </summary>
    class ClaymoreCNCpu : MinerProgramBase
    {

        public override string MINERURL
        {
            get
            {
                return "https://github.com/nanopool/Claymore-XMR-CPU-Miner/releases/download/v3.8/Claymore.CryptoNote.CPU.Miner.v3.8.-.POOL.zip";
            }
        }
        public override string EXENAME
        {
            get
            {
                return "NsCpuCNMiner64.exe";
            }
        }
        public override string PROCESSNAME
        {
            get
            {
                return "NsCpuCNMiner64";
            }
        }
        public override string STATS_LINK
        {
            get
            {
                return "http://127.0.0.1:7999";
            }
        }
        public override string STATS_LINK_HTML
        {
            get
            {
                return "http://127.0.0.1:7999";
            }
        }

        public override string Script { get; set; }


        public override string Type { get; set; }//claymore ccminer etc

        public override IOutputReader OutputReader { get; set; }


        public ClaymoreCNCpu(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMiner miner) :
            base(mainCoin, dualMining, dualCoin, minerName, miner)
        {
            Type = "CPU";
            GPUType = CardMake.CPU;

            OutputReader = new ClayMoreCNCpuReader(STATS_LINK);
        }



        public override string GenerateScript()
        {
            try
            {
                //generate script and write to folder
                string command = EXENAME + " -o " + MainCoinConfigurer.Pool;
                command += " -u " + MainCoinConfigurer.Wallet;
                command += " -p x ";
                if (DualCoin != null)
                {
                    command += "";

                }
                //command += " -allpools 1 ";

                command += " -mport 7999";


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




        private const string SCRIPT1 = "";//maybe these variables work only with amd. not required for cpu

        /// <summary>
        /// reads data for claymore miner
        /// </summary>
        public class ClayMoreCNCpuReader : OutputReaderBase
        {
            public ClayMoreCNCpuReader(string link)
                : base(link)
            {
            }
            MinerDataResult GetResultsSection(string innerText)
            {
                try
                {
                    string pattern = @"\{([^()]|())*\}";
                    Match resultmatch = Regex.Match(innerText, pattern);
                    if (resultmatch.Success)
                    {
                        MinerDataResult minerResult = (MinerDataResult)new JavaScriptSerializer().Deserialize(resultmatch.Value, typeof(MinerDataResult));
                        return minerResult;
                    }
                }
                catch (Exception e)
                {
                }
                return null;
            }
            public override void Parse()
            {
                MinerDataResult minerResult = GetResultsSection(LastLog);
                if (minerResult.Parse(new CNCpuClaymoreResultParser(LastLog, ReReadGpuNames)))
                {
                    MinerResult = minerResult;
                }
                ReReadGpuNames = false;
            }

            public class CNCpuClaymoreResultParser : IMinerResultParser
            {
                MinerDataResult m_MinerResult = null;
                public bool Succeeded { get; set; }//if parsing succeeded without errors
                static Hashtable m_Gpus = new Hashtable();// we only need t read gpu info once as it dosent change with more logs comining in
                static bool m_identified = false;
                bool m_reReadGpunames = false;

                string m_fullLog = "";
                public CNCpuClaymoreResultParser(string fullLog, bool reReadGpunames)
                {
                    m_fullLog = fullLog;
                    m_reReadGpunames = reReadGpunames;
                }

                public bool Parse(object obj)
                {
                    Succeeded = false;

                    m_MinerResult = obj as MinerDataResult;
                    try
                    {
                        if (obj == null)
                            return false;

                        if (!m_identified || m_reReadGpunames)
                        {
                            m_identified = false;
                            m_Gpus.Clear();
                        }
                        if (m_MinerResult.result != null && m_MinerResult.result.Count >= 7)
                        {
                            ComputeRunningTime();
                            ComputeHashnShares();
                            ComputeGPUData();
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Succeeded = false;
                    }
                    return false;

                }
                public void ComputeRunningTime()
                {
                    try
                    {
                        m_MinerResult.RunningTime = Int32.Parse(m_MinerResult.result[1]);
                    }
                    catch (Exception)
                    {
                        m_MinerResult.RunningTime = 0;
                        Succeeded = false;
                        throw;
                    }
                }
                public void ComputeHashnShares()
                {
                    try
                    {
                        string combined = m_MinerResult.result[2];
                        string[] data = combined.Split(';');
                        if (data != null && data.Length == 3)
                        {
                            m_MinerResult.TotalHashrate = Int32.Parse(data[0]);//this is in H/s not MH/s UI will have to do conversion
                            m_MinerResult.TotalShares = Int32.Parse(data[1]);
                            m_MinerResult.Rejected = Int32.Parse(data[2]);

                        }
                    }
                    catch (Exception)
                    {
                        m_MinerResult.TotalHashrate = 0;
                        m_MinerResult.TotalShares = 0;
                        m_MinerResult.Rejected = 0;
                        Succeeded = false;
                        throw;
                    }
                }
                public void ComputeGPUData()
                {
                    try
                    {
                        m_MinerResult.GPUs = new List<GpuData>();
                        string hashCombined = m_MinerResult.result[3];
                        string[] hashrates = hashCombined.Split(';');

                        string fanTemp = m_MinerResult.result[6];
                        string[] fanTempArr = fanTemp.Split(';');
                        if (hashrates != null && hashrates.Length > 0)
                        {
                            int j = 0;
                            int gpu_id = 0;
                            foreach (string item in hashrates)
                            {
                                GpuData gpu = null;
                                string gpu_idstr = gpu_id.ToString();
                                gpu = m_Gpus[gpu_idstr] as GpuData;
                                if (gpu == null)
                                    gpu = new GpuData("CPU " + (gpu_id+1).ToString());

                                gpu.Hashrate = item;
                                gpu.Make = CardMake.CPU;
                                if (j < fanTempArr.Length)
                                {
                                    gpu.Temperature = fanTempArr[j] + "C";
                                    gpu.FanSpeed = fanTempArr[j + 1] + "%";
                                }
                                else
                                {
                                    gpu.Temperature = "0C";
                                    gpu.FanSpeed = "0%";
                                }

                                j += 2;
                                gpu_id++;
                                m_MinerResult.GPUs.Add(gpu);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Succeeded = false;
                        throw;
                    }
                }


            }
        }
    }

}
