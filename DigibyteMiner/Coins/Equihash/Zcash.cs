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
    class Zcash : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;
        public string DefaultAddress { get; set; }

        public Zcash(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "t1ZBzTwKs8wctQrcD6PmH3SRgJhAcLRwPZQ";
        }
        public string Name
        {
            get { return "Zcash"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.zcash; }

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
                Pool pool1 = new FlyPool("Flypool", "eu1-zcash.flypool.org:3333");
                Pool pool2 = new NanoPool("Nanopool", "zec-us-east1.nanopool.org:6666");
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
        class FlyPool : Pool
        {
            public FlyPool(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "http://zcash.flypool.org/miners/" + wallet;
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
                    acc = "https://zec.nanopool.org/account/" + wallet;

                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

    }

}
