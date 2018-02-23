using OneMiner.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    public interface IMinerScript
    {
        string ProgramType { get; set; }//eg:mvidia or AMD
        string BATfile { get; set; }
        bool BATCopied { get; set; }//bat file has been copied inside miner folder. true generally means its ready to mine
        bool AutomaticScriptGeneration { get; set; }
    }
    public interface IMinerData
    {
        string Id { get; set; }
        string Name { get; set; }
        string Algorithm { get; set; }
        //string BATFileName { get; set; }
        string MainCoin { get; set; }
        string MainCoinPool { get; set; }
        string MainCoinWallet { get; set; }
        string MainCoinPoolAccount { get; set; }
        bool DualMining { get; set; }
        string DualCoin { get; set; }
        string DualCoinPool { get; set; }
        string DualCoinWallet { get; set; }
        string DualCoinPoolAccount { get; set; }
        List<MinerScript> MinerScripts { get; set; }
        int MinerGpuType { get; set; }


    }
}
