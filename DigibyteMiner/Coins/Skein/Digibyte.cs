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
                Pool pool2 = new Blocksfactory("Blocksfactory", "stratum+tcp://stratum.dgb.theblocksfactory.com:9002");
                Pool pool3 = new SuperNova("SuperNova", "stratum+tcp://dgbs.suprnova.cc:5226");
                Pool pool4 = new DigiHash("DigiHash", "stratum+tcp://digihash.co:3011");
                pools.Add(pool4);
                pools.Add(pool1);
                pools.Add(pool2);
                pools.Add(pool3);

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
                WrongWallet = "Should be Account.Workername";

            }
            public override bool ValidateAddress(string address)
            {
                if (address.Contains('.') && address.IndexOf('.')!=address.Length-1)
                    return true;
                return false;
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
        class Blocksfactory : Pool
        {
            public Blocksfactory(string name, string url)
                : base(name, url)
            {
                WrongWallet = "Should be Account.Workername";

            }
            public override bool ValidateAddress(string address)
            {
                if (address.Contains('.') && address.IndexOf('.') != address.Length - 1)
                    return true;
                return false;
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://dgb-skein.theblocksfactory.com/accountdetails";

                }
                catch (Exception)
                {
                }
                return acc;
            }
        }
        class SuperNova : Pool
        {
            public SuperNova(string name, string url)
                : base(name, url)
            {
                WrongWallet = "Should be Account.Workername";

            }
            public override bool ValidateAddress(string address)
            {
                if (address.Contains('.') && address.IndexOf('.') != address.Length - 1)
                    return true;
                return false;
            }
            public override string GetAccountLink(string wallet)
            {
                string acc = "";
                try
                {
                    acc = "https://dgbs.suprnova.cc/index.php?page=dashboard";

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
                WalletName = "DigiByte Address";
                WalletAddress = "DFVsFBiKuaL5HM9NWZgdHTQecLNit6tX5Y";
                WrongWallet = "Enter a valid Digibyte address";

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
            public override bool ValidateAddress(string address)
            {
                if (address.Contains('.'))
                    return false;
                if(address.Length!=34)
                    return false;
                return true;
            }
        }

  
    }
}
