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
    class Monero : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;
        public string DefaultAddress { get; set; }

        public Monero(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "463tWEBn5XZJSxLU6uLQnQ2iY9xuNcDbjLSjkn3XAXHCbLrTTErJrBWYgHJQyrCwkNgYvyV3z8zctJLPCZy24jvb3NiTcTJ";
        }
        public string Name
        {
            get { return "Monero"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.monero; }

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
                Pool pool1 = new MoneroPool("MoneroPool", "stratum+tcp://mine.moneropool.com:3333");
                Pool pool2 = new NanoPool("Nanopool", "stratum+ssl://xmr-eu1.nanopool.org:14433");
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
        class MoneroPool : Pool
        {
            public MoneroPool(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://moneropool.com";
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
        class NanoPool : Pool
        {
            public NanoPool(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://xmr.nanopool.org/search?" + wallet;

                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

    }

}
