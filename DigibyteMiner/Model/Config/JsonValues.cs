using OneMiner.Core.Interfaces;
using OneMiner.EthHash;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OneMiner.Model.Config
{
    /// <summary>
    /// structitr of the data is as follows
    /*
{
     defaultMiner:minerName,
     MiningAlgos:[
     {
		name:ethhash
		Programs:[
		{
			type:'nvidia'
			exefile:''
			exefolder:''
		}
		]
     },
	 {
      		name:equihash
		Programs:[
		{
			type:'nvidia'
			exefile:''
			exefolder:''
		}
		{
			type:'amd'
			exefile:''
			exefolder:''
		}
		]
      }
	  ],
	  
	  Miners:[
	  {
	  name:'',
	  batname:'',
	  maincoin:''
	  maincoinpool:''
	  maincoinwallet:''
	  dualcoin:''
	  
	  }
	  ]
}
     * 
     */
    /*
    public class MinerData//mark as abstract class
    {
        public string Name { get; set; }
        public MinerData()
        {
            Name = "Unset";
        }
    }*/
    public class Options
    {
        public Boolean Startup { get; set; }
        public Boolean MineOnStartup { get; set; }
        public Boolean ShowMinerWindows { get; set; }
        public Options()
        {
            Startup = true;
            MineOnStartup = false;
            ShowMinerWindows = true;
        }
    }
    /// <summary>
    /// minerprogram is specific to an algorithm but MinerScript is specific to a miner object
    /// </summary>
    public class MinerProgram
    {
        public string ProgramType { get; set; }//eg:mvidia or AMD
        public string Exepath { get; set; }
        public string ExeFolder { get; set; }

    }
    public class MinerAlgo
    {
        public string Name { get; set; }
        public List<MinerProgram> MinerPrograms { get; set; }
        public MinerAlgo()
        {
            MinerPrograms = new List<MinerProgram>();
        }

    }
    public class MinerScript : IMinerScript
    {
        public string ProgramType { get; set; }//eg:mvidia or AMD
        public string BATfile { get; set; }
        public bool BATCopied { get; set; }//bat file has been copied inside miner folder. true generally means its ready to mine

        public bool AutomaticScriptGeneration { get; set; }
        public MinerScript()
        {
            ProgramType = "";
            BATfile = "";
            AutomaticScriptGeneration = true;
            BATCopied = false;
        }

    }
    public class MinerData : IMinerData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Algorithm { get; set; }
        //public string BATFileName { get; set; }
        public string MainCoin { get; set; }
        public string MainCoinPool { get; set; }
        public string MainCoinWallet { get; set; }
        public string MainCoinPoolAccount { get; set; }


        public bool DualMining { get; set; }
        public string DualCoin { get; set; }
        public string DualCoinPool { get; set; }
        public string DualCoinWallet { get; set; }
        public string DualCoinPoolAccount { get; set; }
        public List<MinerScript> MinerScripts { get; set; }
        public int MinerGpuType { get; set; }

        public MinerData()
        {
            Id = "";
            Name = "";
            Algorithm = "";
            //BATFileName ="";
            MainCoin ="";
            MainCoinPool ="";
            MainCoinWallet ="";
            DualMining =false;
            DualCoin ="";
            DualCoinPool ="";
            DualCoinWallet ="";
            MinerScripts = new List<MinerScript>();
            MinerGpuType = 0;
        }

    }


    public class DB
    {
        public string CurrentMinerId { get; set; }//uniquely identifies a miner from 
        public List<MinerAlgo> MinerAlgos { get; set; }
        public List<MinerData> Miners { get; set; }
        public Options Option { get; set; }
        public DB()
        {
            Option = new Options();
            MinerAlgos = new List<MinerAlgo>();
            Miners = new List<MinerData>();
        }
        public MinerAlgo FindAlgo(IHashAlgorithm algo)
        {
            foreach (MinerAlgo item in MinerAlgos)
            {
                if (item.Name == algo.Name)
                    return item;

            }
            return null;
        }
        public bool AddAlgorithms(List<IHashAlgorithm> algorithms)
        {
            bool toSave = false;
            Hashtable presentAlgos = new Hashtable();
            foreach (MinerAlgo item in MinerAlgos)
            {
                presentAlgos[item.Name] = item;
            }
            foreach (IHashAlgorithm item in algorithms)
            {
                MinerAlgo algo = presentAlgos[item.Name] as MinerAlgo;
                if (algo == null)
                {
                    algo = new MinerAlgo();
                    algo.Name = item.Name;
                    MinerAlgos.Add(algo);
                    toSave = true;
                }
            }
            return toSave;

        }
        public bool AddMinerProgram(IMinerProgram program)
        {
            bool toSave = false;
            string algoName = program.MainCoin.Algorithm.Name;
            MinerAlgo algo = null;
            foreach (MinerAlgo item in MinerAlgos)
            {
                if (item.Name == algoName)
                {
                    algo = item;
                    break;
                }
            }
            if (algo != null)
            {
                MinerProgram prog = new MinerProgram();
                prog.ProgramType = program.Type;
                prog.Exepath = program.MinerEXE;
                prog.ExeFolder = program.MinerFolder;

                //if a similar type is alredy present, them remove it
                List<int> removeIds = new List<int>();
                int i = 0;
                foreach (MinerProgram item in algo.MinerPrograms)
                {
                    if (item.ProgramType == program.Type)
                    {
                        removeIds.Add(i);
                    }
                    i++;
                }
                foreach (int j in removeIds)
                {
                    algo.MinerPrograms.RemoveAt(j);
                }
                algo.MinerPrograms.Add(prog);
                toSave = true;
            }
            return toSave;

        }
        public bool AddMinerScript(IMinerProgram program,IMiner miner)
        {
            bool toSave = false;

            foreach (MinerData item in Miners)
            {
                if(item.Id==miner.Id)
                {
                    //identified the miner
                    foreach (MinerScript script in item.MinerScripts)
                    {
                        if (script.ProgramType == program.Type)
                        {
                            script.BATfile = program.BATFILE;
                            script.BATCopied = program.BATCopied;
                            script.AutomaticScriptGeneration = program.AutomaticScriptGeneration;
                            toSave = true;
                        }
                    }
                    if(toSave == false)
                    {
                        MinerScript script = new MinerScript();
                        script.ProgramType = program.Type;
                        script.BATfile = program.BATFILE;
                        script.BATCopied = program.BATCopied;
                        script.AutomaticScriptGeneration = program.AutomaticScriptGeneration;
                        item.MinerScripts.Add(script);
                        toSave = true;
                    }

                }
            }
            return toSave;

        }
        public bool AddMiner(IMiner miner)
        {
            bool toSave = false;

            ICoinConfigurer mainCoinConfigurer=miner.MainCoin.SettingsScreen;
            ICoinConfigurer dualCoinConfigurer = null;
            if (miner.DualMining)
                dualCoinConfigurer=miner.DualCoin.SettingsScreen;

            MinerData newMiner = new MinerData();

            newMiner.Id =miner.Id;
            newMiner.Name = miner.Name;
            newMiner.Algorithm = miner.MainCoin.Algorithm.Name;
            //newMiner.BATFileName ="";
            newMiner.MainCoin=miner.MainCoin.Name;
            newMiner.MainCoinPool =mainCoinConfigurer.Pool;
            newMiner.MainCoinWallet = mainCoinConfigurer.Wallet;
            newMiner.MainCoinPoolAccount = mainCoinConfigurer.PoolAccount;
            newMiner.DualMining =miner.DualMining;
            newMiner.MinerGpuType = miner.MinerGpuType;
            if (miner.DualMining)
            {
                newMiner.DualCoin = miner.DualCoin.Name;
                newMiner.DualCoinPool = dualCoinConfigurer.Pool;
                newMiner.DualCoinWallet = dualCoinConfigurer.Wallet;
                newMiner.DualCoinPoolAccount = dualCoinConfigurer.PoolAccount;
            }
            //minerprograms
            foreach (IMinerProgram item in miner.MinerPrograms)
            {
                MinerScript script = new MinerScript();
                script.BATfile = item.BATFILE;
                script.ProgramType = item.Type;
                script.AutomaticScriptGeneration = item.AutomaticScriptGeneration;
                newMiner.MinerScripts.Add(script);
                
            }

            //if a similar type is alredy present, them remove it
            List<int> removeIds = new List<int>();
            int i = 0;
            foreach (MinerData item in Miners)
            {
                if (item.Id == miner.Id)
                {
                    removeIds.Add(i);
                }
                i++;
            }
            foreach (int j in removeIds)
            {
                Miners.RemoveAt(j);
            }
            Miners.Add(newMiner);
            toSave = true;

            return toSave;
        }
        public bool RemoveMiner(IMiner miner)
        {
            bool toSave = false;
            List<int> removeIds = new List<int>();
            int i = 0;
            foreach (MinerData item in Miners)
            {
                if (item.Id == miner.Id)
                {
                    removeIds.Add(i);
                    toSave = true;
                }
                i++;
            }
            foreach (int j in removeIds)
            {
                Miners.RemoveAt(j);
            }
            return toSave;
        }


    }
}
