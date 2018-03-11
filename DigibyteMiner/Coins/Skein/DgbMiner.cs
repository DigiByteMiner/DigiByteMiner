using DigibyteMiner.Coins;
using DigibyteMiner.Coins.Skein;
using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

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
        public override void CalculateProfitability()
        {
            try
            {
                float hr = float.Parse(HashRate);
                hr=(hr/1000000000);
                string hr_s = hr.ToString("F2");
                string url = "https://whattomine.com/coins/114.json?utf8=%E2%9C%93&hr=";
                url+=hr_s;
                url+="&p=100.0&fee=0.0&cost=0.1&hcost=0.0&commit=Calculate";
                try
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string sss = reader.ReadToEnd();
                        Profit = (ProfitInfo)new JavaScriptSerializer().Deserialize(sss, typeof(ProfitInfo));
                    }
                }
                catch (Exception e)
                {
                }
            }
            catch (Exception e)
            {

            }
        }
        

    }
}
