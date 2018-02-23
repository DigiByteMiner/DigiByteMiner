using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OneMiner.Core.Interfaces
{
    public enum CardMake
    {
        Nvidia=0,
        Amd,
        CPU,
        COMMON,//this means the gpu or the gpuminer is independend of type
        UNKNOWN,
        END
    }
    public class GpuData 
    {
        public string Hashrate { get; set; }
        public string FanSpeed { get; set; }
        public string Temperature { get; set; }
        public string GPUName { get; set; }//eg GPU0
        public CardMake Make { get; set; }
        public GpuData(string name)
        {
            Make = CardMake.END;
            GPUName = name;
            Hashrate = "0";
            FanSpeed = "0";
            Temperature = "0";
        }
        public bool IdentifyMake()
        {
            try
            {
                string nvidia_pattern = "(N|n)(V|v)(I|i)(D|d)(I|i)(A|a)|(G|g)(E|e)(F|f)(O|o)(R|r)(C|c)(E|e)|(G|g)(T|t)(X|x) ";
                string amd_pattern = "(A|a)(M|m)(D|d)|(R|r)(A|a)(D|d)(E|e)(O|o)(N|n)|(R|r)(X|x)";
                Match r_nvidia_id = Regex.Match(GPUName, nvidia_pattern);
                Match r_amd_id = Regex.Match(GPUName, amd_pattern);
                if (r_nvidia_id.Success)
                    Make= CardMake.Nvidia;
                else if (r_amd_id.Success)
                    Make = CardMake.Amd;
                else
                    Make = CardMake.UNKNOWN;
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

    }
    public class MinerDataResult
    {
        public List<string> result { get; set; }
        public int TotalHashrate { get; set; }
        public int TotalShares { get; set; }
        public int Rejected { get; set; }
        public int RunningTime { get; set; }
        public List<GpuData> GPUs { get; set; }
        public MinerDataResult()
        {
            GPUs = new List<GpuData>();
        }
        //this class does not know how to parse the results as different miners have data in different formats
        //So it accepts a visitor which can do it
        public bool Parse(IMinerResultParser parser)
        {
            return parser.Parse(this);
        }
    }
    /// <summary>
    /// different miners can implement different logic for parsing and filling the data
    /// </summary>
    public interface IMinerResultParser
    {
        bool Parse(object obj);
    }
}
