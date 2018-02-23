using OneMiner.Coins.EthHash;
using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace OneMiner.Coins
{
    public class MinerBase : MinerData, IMiner
    {
        //these are the miners thet the algo supports
        public List<IMinerProgram> MinerPrograms { get; set; }
        //thse are the miners that are going to run based on what the gpu types are or what the user has selected
        public List<IMinerProgram> ActualMinerPrograms { get; set; }
        public Hashtable m_MinerProgsHash = new Hashtable();
        HashSet<string> m_MinerRunningHash = new HashSet<string>();

        public ICoin MainCoin { get; set; }
        public ICoin DualCoin { get; set; }
        private IMinerData MinerData { get; set; }
        public int MinerGpuType { get; set; }

        public bool DualMining { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }//not sure if needed
        public MinerProgramState MinerState { get; set; }
        public int DownloadPercentage { get; set; }

        public bool DefaultMiner { get; set; }

        public MinerBase(string id, ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMinerData minerData)
        {
            MinerState = MinerProgramState.Stopped;
            Id = id;
            MainCoin = mainCoin;
            DualCoin = dualCoin;
            DualMining = dualMining;
            Name = minerName;
            MinerData = minerData;
            if (MinerData != null)
                MinerGpuType = MinerData.MinerGpuType;
            else
                IdentifyGpuTypes();
            MinerPrograms = new List<IMinerProgram>();
            ActualMinerPrograms = new List<IMinerProgram>();
            SetupMiner(true);
            DefaultMiner = false;
            DownloadPercentage = 0;
        }
        public void IdentifyGpuTypes()
        {
            List<GpuData> gpus = GetGpuList();
            int gpuType = 0;
            foreach (GpuData gpuData in gpus)
            {
                if (gpuData.Make == CardMake.Nvidia)
                    //gpuType = gpuType | 1; 
                    gpuType = gpuType | (1 << 0);
                if (gpuData.Make == CardMake.Amd)
                    //gpuType = gpuType | 2;
                    gpuType = gpuType | (1 << 1);

            }
            MinerGpuType = gpuType;

        }
        public void ChangeGPUType(IMinerProgram prog)
        {
            int gpuType = MinerGpuType;
            if (prog.GPUType == CardMake.Nvidia)
                gpuType = gpuType ^ (1 << 0);
            if (prog.GPUType == CardMake.Amd)
                gpuType = gpuType ^ (1 << 1);
            if (prog.GPUType == CardMake.CPU)
                gpuType = gpuType ^ (1 << 2);
            MinerGpuType = gpuType;
            SetupMiner(false);
            Factory.Instance.Model.AddMiner(this);


        }
        public void InitializePrograms()
        {
            try
            {
                DB db = Factory.Instance.Model.Data;
                MinerAlgo algo = null;
                foreach (MinerAlgo item in db.MinerAlgos)
                {
                    if (item.Name == MainCoin.Algorithm.Name)
                    {
                        //found the algo. check to see alredy configured miners
                        algo = item;
                        break;
                    }
                }
                if (algo != null)
                {
                    List<MinerProgram> programs = algo.MinerPrograms;
                    //if (programs != null && programs.Count > 0)
                    //{
                    //at least 1 program is there
                    Hashtable progs = new Hashtable();
                    Hashtable scripts = new Hashtable();
                    foreach (MinerProgram item in programs)
                    {
                        progs.Add(item.ProgramType, item);
                    }
                    foreach (MinerScript item in MinerData.MinerScripts)
                    {
                        scripts.Add(item.ProgramType, item);
                    }
                    foreach (IMinerProgram item in MinerPrograms)
                    {
                        MinerProgram p = progs[item.Type] as MinerProgram;
                        MinerScript scr = scripts[item.Type] as MinerScript;
                        if (p != null)
                        {
                            item.MinerEXE = p.Exepath;
                            item.MinerFolder = p.ExeFolder;
                        }
                        if (scr != null)
                        {
                            item.BATFILE = scr.BATfile;
                            item.BATCopied = scr.BATCopied;
                            item.AutomaticScriptGeneration = scr.AutomaticScriptGeneration;
                            item.LoadScript();

                        }
                    }
                    //}

                }
            }
            catch (Exception)
            {
            }
        }
        public void SetRunningState(IMinerProgram program, MinerProgramState state)
        {
            int count = MinerPrograms.Count;
            if (state != MinerProgramState.Running)
            {
                m_MinerRunningHash.Remove(program.Type);
            }
            else
            {
                m_MinerRunningHash.Add(program.Type);
            }
            if (m_MinerRunningHash.Count == 0)
                MinerState = MinerProgramState.Stopped;
            else if (m_MinerRunningHash.Count < ActualMinerPrograms.Count)
                MinerState = MinerProgramState.PartiallyRunning;
            else if (m_MinerRunningHash.Count == ActualMinerPrograms.Count)
                MinerState = MinerProgramState.Running;
            else
                MinerState = MinerProgramState.Stopping;//ideally it shudnt com here

            //if its not running or partiallyrunning then check if its downloading. if so then set it
            if (MinerState == MinerProgramState.Stopped && state == MinerProgramState.Downloading)
                MinerState = MinerProgramState.Downloading;
            Factory.Instance.ViewObject.UpDateMinerState();

        }
        public virtual void SetupMiner(bool minerCreation)
        {
            throw new NotImplementedException();
        }
        public virtual void StartMining()
        {
            DownloadPercentage = 0;
            MinerState = MinerProgramState.Starting;
            if (ActualMinerPrograms.Count==0)
            {
                Factory.Instance.ViewObject.ShowHardwareMissingError();
                StopMining();
                return;
            }
            foreach (IMinerProgram item in ActualMinerPrograms)
            {
                //push miners into mining queue wher they wud be picked up by threads and executed
                Factory.Instance.CoreObject.MiningQueue.Enqueue(item);
            }
            Factory.Instance.ViewObject.UpDateMinerState();
        }
        public void StopMining()
        {
            DownloadPercentage = 0;
            MinerState = MinerProgramState.Stopping;
            m_MinerRunningHash.Clear();

            foreach (IMinerProgram item in MinerPrograms)
            {
                //push miners into mining queue wher they wud be picked up by threads and executed
                item.KillMiner();
            }
            MinerState = MinerProgramState.Stopped;
            Factory.Instance.ViewObject.UpDateMinerState();


        }
        /// <summary>
        /// this is put here instead of in a central location like core because in future u might want to disable few graphic cards in which case
        /// this class will have that info as disabling will be specific to a miner
        /// </summary>
        public List<GpuData> GetGpuList()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                List<GpuData> gpus = new List<GpuData>();

                string graphicsCard = string.Empty;
                foreach (ManagementObject mo in searcher.Get())
                {
                    string name = mo["Name"] as string;
                    if (!string.IsNullOrEmpty(name))
                    {
                        GpuData data = new GpuData(name);
                        data.IdentifyMake();
                        gpus.Add(data);
                    }
                }
                return gpus;
            }
            catch (Exception e)
            {
            }
            return null;
        }

    }
}
