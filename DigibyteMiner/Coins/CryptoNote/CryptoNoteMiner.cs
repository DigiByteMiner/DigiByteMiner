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

namespace OneMiner.Coins.CryptoNote
{
    public class CryptoNoteMiner : MinerBase
    {
        public CryptoNoteMiner(string id, ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMinerData minerData) :
            base(id, mainCoin, dualMining, dualCoin, minerName, minerData)
        {

        }

        public override void SetupMiner(bool minerCreation)
        {
            ActualMinerPrograms.Clear();
            MinerPrograms.Clear();
            m_MinerProgsHash.Clear();

            IMinerProgram prog = new ClaymoreCNAmd(MainCoin, DualMining, DualCoin, Name, this);
            MinerPrograms.Add(prog);
            IMinerProgram prog2 = new CCMiner(MainCoin, DualMining, DualCoin, Name, this);
            MinerPrograms.Add(prog2);
            IMinerProgram prog3 = new ClaymoreCNCpu(MainCoin, DualMining, DualCoin, Name, this);
            MinerPrograms.Add(prog3);

            m_MinerProgsHash.Add(prog.GPUType, prog);
            m_MinerProgsHash.Add(prog2.GPUType, prog2);
            m_MinerProgsHash.Add(CardMake.CPU, prog3);

            if (!minerCreation &&  (MinerGpuType & 1) > 0)//nvidia supported only if manually added
            {
                IMinerProgram program = m_MinerProgsHash[CardMake.Nvidia] as IMinerProgram;
                if (prog != null)
                {
                    program.Enabled = true;
                    ActualMinerPrograms.Add(program);
                }
            }
            if ((MinerGpuType & 2) > 0)
            {
                IMinerProgram program = m_MinerProgsHash[CardMake.Amd] as IMinerProgram;
                if (prog != null)
                {
                    program.Enabled = true;
                    ActualMinerPrograms.Add(program);
                }
            }
            //Add CPU miner in 2 condition
            //1. User explicitely asked to add
            //2. During creation of miner, there was no GPU miner available
            if ((MinerGpuType & 4) > 0 || 
                ( minerCreation && ActualMinerPrograms.Count == 0))
            {
                //To know why 2 shits are used, refer MinerBase::ChangeGPUType. So 0 shift is 1, 1 shift is 2 and 2 shift is 4
                MinerGpuType = MinerGpuType | (1 << 2); 
                IMinerProgram program = m_MinerProgsHash[CardMake.CPU] as IMinerProgram;
                if (prog != null)
                {
                    program.Enabled = true;
                    ActualMinerPrograms.Add(program);
                }
            }

        }


    }
}
