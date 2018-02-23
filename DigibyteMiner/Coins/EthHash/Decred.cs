using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.EthHash
{
    class Decred : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }
        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public Decred(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "asatyarth.Sat_miner";

        }
        public string Name
        {
            get { return "Decred"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.decred; }
            
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
                Pool pool1 = new Supernova("Supernova", "dcr.suprnova.cc:3252");
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

        class Supernova : Pool
        {
            public Supernova(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://dcr.suprnova.cc/index.php?page=dashboard";
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

    }
}
