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

namespace OneMiner.Coins
{

    class MinerProgramBase : IMinerProgram
    {
        public virtual string MINERURL
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual string EXENAME
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual string PROCESSNAME
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual string STATS_LINK
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual string STATS_LINK_HTML
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Script { get; set; }
        public virtual bool Enabled { get; set; }



        public IOutputReader Reader { get; set; }

        public string MinerFolder { get; set; }
        public string MinerEXE { get; set; }
        public string BATFILE { get; set; }
        public bool BATCopied { get; set; }

        public bool AutomaticScriptGeneration { get; set; }


        public MinerProgramState MinerState { get; set; }

        public IMiner Miner { get; set; }
        public ICoin MainCoin { get; set; }
        public ICoin DualCoin { get; set; }

        public ICoinConfigurer MainCoinConfigurer { get; set; }
        public ICoinConfigurer DualCoinConfigurer { get; set; }
        public bool DualMining { get; set; }
        public string Name { get; set; }
        public virtual string Type { get; set; }//claymore ccminer etc
        public CardMake GPUType { get; set; }//AMD, NVidia or COmmon
        MinerDownloader m_downloader = null;
        private Process m_Process = null;
        private object m_accesssynch = new object();
        public virtual IOutputReader OutputReader { get; set; }



        public MinerProgramBase(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMiner miner)
        {

            MinerState = MinerProgramState.Stopped;

            MainCoin = mainCoin;
            MainCoinConfigurer = mainCoin.SettingsScreen;
            DualCoin = dualCoin;
            if (DualCoin != null)
                DualCoinConfigurer = DualCoin.SettingsScreen;
            DualMining = dualMining;
            Name = minerName;
            Miner = miner;
            AutomaticScriptGeneration = true;
            m_downloader = new MinerDownloader(MINERURL, EXENAME,Miner);
            Enabled = false;
            GenerateScript();

        }



        public virtual bool ReadyForMining()
        {
            return MiningScriptsPresent() && ProgramPresent() && BATCopied;
        }
        public virtual bool MiningScriptsPresent()
        {
            if (BATFILE == null || BATFILE == "")
                return false;
            FileInfo script = new FileInfo(BATFILE);
            if (script.Exists)
                return true;
            return ProgramPresent();
        }
        public virtual bool ProgramPresent()
        {
            if (MinerEXE == null || MinerEXE == "")
                return false;
            FileInfo miner = new FileInfo(MinerEXE);
            if (miner.Exists)
                return true;
            return false;
        }
        public virtual void SaveProgramToDB()
        {
            if (ProgramPresent())
            {
                Config model = Factory.Instance.Model;
                model.AddMinerProgram(this);

            }
        }
        public virtual void SaveScriptToDB()
        {
            if (MiningScriptsPresent())
            {
                Config model = Factory.Instance.Model;
                model.AddMinerScript(this, Miner);

            }
        }

        public virtual string FormBatFileName(string folder)
        {
            return folder + @"\" + Miner.Name + ".bat";
        }
        public virtual void DownloadProgram()
        {
            try
            {
                if (!ProgramPresent())
                {
                    MinerState = MinerProgramState.Downloading;
                    Miner.SetRunningState(this, MinerState);

                    MinerFolder = m_downloader.DownloadFile();
                    MinerEXE = MinerFolder + @"\" + EXENAME;
                    SaveProgramToDB();

                }
                string actualBatfileName = FormBatFileName(MinerFolder);

                if (AutomaticScriptGeneration == false)
                {
                    //this might be becoz user has edited the bat file
                    FileInfo file = new FileInfo(BATFILE);
                    if (file.Exists)
                    {
                        file.CopyTo(actualBatfileName, true);
                    }
                }
                BATFILE = actualBatfileName;
                ConfigureMiner();
                BATCopied = true;
                SaveScriptToDB();
                MinerState = MinerProgramState.Stopped;
            }
            catch (Exception e)
            {
                Logger.Instance.LogError(e.Message);
            }
        }

        public virtual void StartMining()
        {
            //lock ensures that neither can someone kill a miner while it is being started, nor can 2 people start it at same time
            lock (m_accesssynch)
            {
                try
                {
                    FileInfo file = new FileInfo(BATFILE);
                    if (Factory.Instance.CoreObject.MiningCommand != MinerProgramCommand.Run)
                    {
                        throw new Exception("Mining command is not 'Run'");
                    }
                    if (m_Process != null)
                    {
                        throw new Exception("Process object is not null while starting");
                    }
                    if (file.Exists)
                    {
                        MinerState = MinerProgramState.Running;
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.UseShellExecute = false;
                        info.CreateNoWindow = ! Factory.Instance.Model.Data.Option.ShowMinerWindows;
                        info.FileName = BATFILE;
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                        info.WorkingDirectory = file.DirectoryName + "\\";

                        m_Process = new Process();
                        m_Process.StartInfo = info;
                        bool success = m_Process.Start();
                        if (success)
                        {
                            MinerState = MinerProgramState.Running;
                            Miner.SetRunningState(this, MinerProgramState.Running);
                            Alarm.RegisterForTimer(OutputReader.AlarmRaised);
                            OutputReader.ReReadGpuNames = true;
                        }

                    }

                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
                finally
                {
                    //MinerState = MinerProgramState.Stopped;
                }
            }
        }
        public virtual void SetRunningState(MinerProgramState state)
        {
            lock (m_accesssynch)
            {
                try
                {
                    MinerState = state;
                    Miner.SetRunningState(this, state);
                }
                catch (Exception e)
                {
                }
            }
        }
        public virtual void KillMiner()
        {
            lock (m_accesssynch)
            {
                try
                {
                    if (m_Process != null)
                    {
                        try
                        {
                            //this actually dos4nt work as we get handle to command prompt used by the miner as its a batch file
                            m_Process.Kill();
                        }
                        catch (Exception e)
                        {
                            Logger.Instance.LogError(e.ToString());
                        }
                    }
                    Process[] allprocess = Process.GetProcessesByName(PROCESSNAME);//this does the job
                    if (allprocess != null && allprocess.Length > 0)
                    {
                        foreach (Process item in allprocess)
                        {
                            item.Kill();
                        }
                    }
                    m_Process = null;
                    MinerState = MinerProgramState.Stopped;
                    Miner.SetRunningState(this, MinerState);

                }
                catch (Exception e)
                {
                    Logger.Instance.LogError(e.ToString());
                }
            }
        }

        public virtual bool Running()
        {
            bool running = false;
            try
            {
                running = !m_Process.HasExited;
            }
            catch (Exception e)
            {
                running = false;
            }
            return running;
        }



        public virtual string GenerateScript()
        {
            throw new NotImplementedException();
        }
        public virtual void ModifyScript(string script)
        {
            Script = script;
            string tempBatFile = "";
            string tempBatFileFolder = "";
            if (MinerFolder != null && MinerFolder != "")
                tempBatFileFolder = MinerFolder;
            else
                tempBatFileFolder = m_downloader.GetTempBatFile(Miner.Id, Type, Miner.Name);
            tempBatFile = FormBatFileName(tempBatFileFolder);

            if (tempBatFile != "")
            {
                BATFILE = tempBatFile;
                SaveToBAtFile();
                AutomaticScriptGeneration = false;
                SaveScriptToDB();
            }
        }
        public virtual void ConfigureMiner()
        {
            try
            {
                if (AutomaticScriptGeneration == false)
                    return;

                GenerateScript();
                SaveToBAtFile();

            }
            catch (Exception e)
            {
            }
        }
        public virtual void SaveToBAtFile()
        {
            try
            {
                FileStream stream = File.Open(BATFILE, FileMode.Create);
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(Script);
                sw.Flush();
                sw.Close();
                //generate script and write to folder

            }
            catch (Exception e)
            {
            }
        }
        public virtual void LoadScript()
        {
            try
            {
                if (AutomaticScriptGeneration)
                {
                    GenerateScript();
                }
                else
                {
                    FileStream stream = File.Open(BATFILE, FileMode.Open);
                    StreamReader sr = new StreamReader(stream);
                    Script = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception e)
            {
            }
        }



        private const string SCRIPT1 = "";



       
    }

}
