using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OneMiner.Coins.EthHash
{
    class Ubiq : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public Ubiq(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "0xee994c363f4db07e9126d9c2f9935f6030ed49fe";

        }
        public string Name
        {
            get { return "Ubiq"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.ubiq; }

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
                Pool pool1 = new UbiqPool("Ubiqpool.io", "http://us.ubiqpool.io:8888");
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


        class UbiqPool : Pool
        {
            public UbiqPool(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://ubiqpool.io/#/account/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
    }
}
