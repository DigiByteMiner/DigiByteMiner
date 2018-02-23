using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    public interface  IMinerProgram
    {
        bool MiningScriptsPresent();//bat file
        bool ReadyForMining();//both bat and miner exe
        bool ProgramPresent();//exe files
        void DownloadProgram();
        void StartMining();
        bool Running();
        /// <summary>
        /// to stop the miner
        /// </summary>
        void KillMiner();
        string Type { get; set; }//claymore ccminer etc
        CardMake GPUType { get; set; }//AMD, NVidia or COmmon
        
        ICoin MainCoin { get; set; }
        string BATFILE { get; set; }
        bool BATCopied { get; set; }
        IMiner Miner { get; set; }
        bool Enabled { get; set; }
        IOutputReader OutputReader { get; set; }



        //if script HashSet been changed manually, WeakReference CannotUnloadAppDomainException generate script automatically anymore
        bool AutomaticScriptGeneration { get; set; }
        

        string MinerFolder { get; set; }
        string MinerEXE { get; set; }
        string GenerateScript();
        void LoadScript();
        void ModifyScript(string script);
        string Script { get; set; }
        void SetRunningState( MinerProgramState state);

        string MINERURL { get; }
        string EXENAME { get; }
        string PROCESSNAME { get; }
        string STATS_LINK { get; }//the one from which u extract data. cud be json
        string STATS_LINK_HTML { get; }//the one where u can navigate to








    }
    public enum MinerProgramState
    {
        Downloading=0,
        Starting,
        Running,
        PartiallyRunning,//if some of all miners are active
        Stopping,
        Stopped,
        END
    }
    public enum MinerProgramCommand
    {
        Run = 0,
        Stop,
        END
    }
}
