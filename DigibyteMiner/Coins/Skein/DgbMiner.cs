using DigibyteMiner.Coins;
using DigibyteMiner.Coins.Skein;
using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace DigibyteMiner.Skein
{
    public class DgbMiner: MinerBase
    {
        public DgbMiner(string id, ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName, IMinerData minerData) :
            base( id,  mainCoin,  dualMining,  dualCoin,  minerName,  minerData)
        {

        }

        public override void SetupMiner(bool minerCreation)
        {
            ActualMinerPrograms.Clear();
            MinerPrograms.Clear();
            m_MinerProgsHash.Clear();

            IMinerProgram prog = new CCMiner(MainCoin, DualMining, DualCoin, Name, this);
            MinerPrograms.Add(prog);
            IMinerProgram prog2 = new CGMiner(MainCoin, DualMining, DualCoin, Name, this);
            MinerPrograms.Add(prog2);

            m_MinerProgsHash.Add(prog.GPUType, prog);
            m_MinerProgsHash.Add(prog2.GPUType, prog2);
            if (MinerGpuType == 3)
            {
                foreach (IMinerProgram item in MinerPrograms)
                {
                    item.Enabled = true;
                    ActualMinerPrograms.Add(item);
                }
            }
            else if (MinerGpuType == 1)
            {
                IMinerProgram program = m_MinerProgsHash[CardMake.Nvidia] as IMinerProgram;
                if (prog != null)
                {
                    program.Enabled = true;
                    ActualMinerPrograms.Add(program);
                }
            }
            else if (MinerGpuType == 2)
            {
                IMinerProgram program = m_MinerProgsHash[CardMake.Amd] as IMinerProgram;
                if (prog != null)
                {
                    program.Enabled = true;
                    ActualMinerPrograms.Add(program);
                }
            }
        }

        

    }
}
