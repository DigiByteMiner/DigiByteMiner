using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    /// <summary>
    /// callback for all timers
    /// </summary>
    public delegate void OneMinerTimerEvent();

    public interface IHashAlgorithm
    {
        /// <summary>
        /// Name of the algo
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Coins supported by this algo. eg: Ethhash would support Ethereum , Ubiq etc
        /// </summary>
        List<ICoin> SupportedCoins { get; }

        /// <summary>
        /// Some algo can profitably dual mine eg ethhash but not equihash
        /// </summary>
        Boolean SupportsDualMining { get; }

        /// <summary>
        /// List of coin s supportde by this algo
        /// </summary>
        List<ICoin> SupportedDualCoins { get; }

        /// <summary>
        /// This will be the coin that will be given precendence if we hav to display anywhere
        /// </summary>
        ICoin DefaultCoin{get;}

        /// <summary>
        /// This will be the coin that will be given precendence if we hav to display anywhere
        /// </summary>
        ICoin DefaultDualCoin { get; }

        IMiner CreateMiner(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName);

        IMiner RegenerateMiner(IMinerData miner);

        IMiner DefaultMiner();


    }
}
