using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DigibyteMiner.Skein
{
    /// <summary>
    /// These classes dont store user data. They are to drive the UI
    /// </summary>
    class Digibyte : ICoin
    {
        public IHashAlgorithm Algorithm { get; set; }

        ICoinConfigurer Configurer;

        public string DefaultAddress { get; set; }

        public Digibyte(IHashAlgorithm algo)
        {
            Algorithm = algo;

            //This is only used in Debug mode for quick testing of the miner
            DefaultAddress = "asatyarth.arun";

        }
        public  string Name
        {
            get { return "DigiByte"; }
        }

        public Bitmap Logo
        {
            get { return DigibyteMiner.Properties.Resources.digibyte; }
            
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
                Pool pool1 = new MiningField("MiningField", "stratum+tcp://us.miningfield.com:3397");
                Pool pool2 = new DigiHash("DigiHash", "stratum+tcp://digihash.co:3009");
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

        class MiningField : Pool
        {
            public MiningField(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://dgbskein.miningfield.com/index.php?page=dashboard";
                    
                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
        class DigiHash : Pool
        {
            public DigiHash(string name, string url)
                : base(name, url)
            {

            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "http://digihash.co/workers/" + wallet;

                }
                catch (Exception)
                {
                }
                return acc;
            }
        }

  
    }
}
