using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OneMiner.Coins.CryptoNote
{
    /// <summary>
    /// These classes dont store user data. They are to drive the UI
    /// </summary>
    class Electroneum : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;
        public string DefaultAddress { get; set; }

        public Electroneum(IHashAlgorithm algo)
        {
            Algorithm = algo;
            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "etnjzKFU6ogESSKRZZbdqraPdcKVxEC17Cm1Xvbyy76PARQMmgrgceH4krAH6xmjKwJ3HtSAKuyFm1BBWYqtchtq9tBap8Qr4M";
        }
        public string Name
        {
            get { return "Electroneum"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.electroneum; }

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
                Pool pool1 = new ElectroneumPool("electroneum.com", "stratum+tcp://uspool.electroneum.com:3333");
                Pool pool2 = new HashParty("HashParty", "stratum+tcp://us-etn-pool.hashparty.io:3333");
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
        class ElectroneumPool : Pool
        {
            public ElectroneumPool(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://uspool.electroneum.com/";
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
        class HashParty : Pool
        {
            public HashParty(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "http://us-etn-stats.hashparty.io";
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

    }

}
