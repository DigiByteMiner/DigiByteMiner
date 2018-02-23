using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.Coins.EthHash
{
    class Sia: ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }
        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public Sia(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "e6b14fab8a4fd55155322cad706831d677342527c75141cf8700ea77d647bb4ba791cf11a4c5";

        }
        public string Name
        {
            get { return "SiaCoin"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.siacoin; }
            
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
                Pool pool2 = new Nanopool("Nanopool", "sia-us-west1.nanopool.org:7777");
                pools.Add(pool2);

                return pools;
            }
            catch (Exception e)
            {
            }
            return pools;
        }
        public string GetScript(string script)
        {
            string scr = script;
            scr += " -dcoin sia ";
            return scr;
        }

        class Nanopool : Pool
        {
            public Nanopool(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://sia.nanopool.org/account/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
    }
}
