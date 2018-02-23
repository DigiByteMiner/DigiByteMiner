using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    public interface  IOutputReader
    {
        //Todo:Assign callback which will call ui maybe whenever we get new data
        //or kep everything in queue here and push pul l wen needed
        void AlarmRaised();
        string LastLog { get; set; }
        string NextLog { get; set; }
        string StatsLink { get; set; }
        MinerDataResult MinerResult { get; set; }
        //if true, next time we parse outputs, we will try to read the gpu names again. will reset when new object is made and miner is started
        bool ReReadGpuNames { get; set; }

    }
}
