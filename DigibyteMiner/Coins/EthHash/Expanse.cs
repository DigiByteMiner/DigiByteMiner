using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OneMiner.Coins.EthHash
{
    class Expanse : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public Expanse(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "0x8201b27781456b75502b1aed759dcc001a03f4aa";

        }
        public  string Name
        {
            get { return "Expanse"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.expanse; }
            
        }

        public ICoinConfigurer SettingsScreen
        {
            get
            {
                if (Configurer == null)
                {
                    Configurer = new ConfigureMiner();
                    Configurer.AssignCoin(this);
                }
                return Configurer;
            }
        }
        public List<Pool> GetPools()
        {
            List<Pool> pools = new List<Pool>();
            try
            {
                Pool pool1 = new Expmine("Expmine", "us.expmine.pro:9009 ");
                pools.Add(pool1);

                return pools;
            }
            catch (Exception e)
            {
            }
            return pools;
        }
        public string GetScript(string script)
        {
            return script;
        }


        class Expmine : Pool
        {
            public Expmine(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://expmine.pro/#/account/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
    }
}
