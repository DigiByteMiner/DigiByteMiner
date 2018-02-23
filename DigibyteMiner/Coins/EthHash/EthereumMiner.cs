using OneMiner.Coins;
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

namespace OneMiner.EthHash
{
    public class EthereumMiner: MinerBase
    {
        public EthereumMiner(string id, ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMinerData minerData):
            base( id,  mainCoin,  dualMining,  dualCoin,  minerName,  minerData)
        {

        }

        public override void SetupMiner(bool minerCreation)
        {
            ActualMinerPrograms.Clear();
            MinerPrograms.Clear();
            m_MinerProgsHash.Clear();
 
            IMinerProgram prog=new ClaymoreMiner(MainCoin, DualMining, DualCoin, Name,this);
            MinerPrograms.Add(prog);
            if (MinerGpuType >= 1 && MinerGpuType <= 3)
            {
                prog.Enabled = true;
                ActualMinerPrograms.Add(prog);
            }
            m_MinerProgsHash.Add(prog.Type, prog);
        }

        

    }
}
