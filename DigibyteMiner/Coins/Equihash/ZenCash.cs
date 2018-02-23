using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OneMiner.EthHash
{
    /// <summary>
    /// These classes dont store user data. They are to drive the UI
    /// </summary>
    class ZenCash : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public ZenCash(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "znoxX7U7EYC3XaAN81eziNk7YYboA2wHZMF";
        }
        public string Name
        {
            get { return "ZenCash"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.zencash; }

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
                Pool pool1 = new ZenMinePro("ZenMine.Pro", "ssl://eu.zenmine.pro:9999");
                Pool pool2 = new MinezZone("minez.zone", "stratum+tcp://ny1.minez.zone:3035");
                pools.Add(pool1);
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
            return script;
        }

        class MinezZone : Pool
        {
            public MinezZone(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://minez.zone/workers/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
        class ZenMinePro : Pool
        {
            public ZenMinePro(string name, string url)
                : base(name, url)
            {
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://zenmine.pro/#/account/" + wallet;
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
    }
}
