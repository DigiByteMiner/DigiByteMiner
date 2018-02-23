using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OneMiner.Coins.EthHash
{
    class EtherClassic : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public EtherClassic(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "0xf7684cff8186f8d1f9044a597f40cc03af453351";

        }
        public string Name
        {
            get { return "Ethereum Classic"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.ethclassic; }
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
                Pool pool1 = new Ethermine("Ethermine", "us1-etc.ethermine.org:4444");
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



        class Ethermine : Pool
        {
            public Ethermine(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://etc.ethermine.org/miners/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
       
    }
}
