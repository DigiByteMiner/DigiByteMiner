using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    public interface IMiner
    {
        ICoin MainCoin { get; set; }
        ICoin DualCoin { get; set; }
        string Name { get; set; }
        bool DualMining { get; set; }
        string Id { get; set; }
        List<IMinerProgram> MinerPrograms { get; set; }
        List<IMinerProgram> ActualMinerPrograms { get; set; }


        void StartMining();
        void StopMining();
        void InitializePrograms();
        void SetRunningState(IMinerProgram program, MinerProgramState state);
        MinerProgramState MinerState { get; set; }
        int DownloadPercentage { get; set; }
        int MinerGpuType { get; set; }

        List<GpuData> GetGpuList();
        void ChangeGPUType(IMinerProgram prog);

        /// <summary>
        /// tells wether this is a default miner or not.
        /// </summary>
        bool DefaultMiner { get; set; }


    }
}
