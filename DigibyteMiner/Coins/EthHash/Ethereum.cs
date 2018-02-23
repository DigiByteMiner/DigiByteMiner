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
    class Ethereum : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }
        
        public Ethereum(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "0x033ff6918d434cef3887d8e529c14d1bcb91ca8b";

        }
        public  string Name
        {
            get { return "Ethereum"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.ethereum; }
            
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
                Pool pool1 = new Ethermine("Ethermine", "us1.ethermine.org:4444");
                Pool pool2 = new Nanopool("Nanopool", "eth-us-west1.nanopool.org:9999");
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
                    acc = "https://ethermine.org/miners/" + wallet;
                    
                }
                catch (Exception)
                {
                }
                return acc;
            }
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
                    acc = "https://eth.nanopool.org/account/" + wallet;

                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

  
    }
}
