using DigibyteMiner.Core;
using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigibyteMiner.Skein
{
    public class Skein : IHashAlgorithm
    {
        enum SkeinCoins{
            Digibyte=0,
            End
        }

        List<ICoin> m_SupportedDualCoins = new List<ICoin>();
        List<ICoin> m_SupportedCoins = new List<ICoin>();
        Hashtable m_CoinsHash = new Hashtable();

        public Skein()
        {
            m_CoinsHash[SkeinCoins.Digibyte] = new Digibyte(this);

            //Now add it to the lists
            m_SupportedCoins.Add(m_CoinsHash[SkeinCoins.Digibyte] as ICoin);

        }
        public string Name
        {
            get
            {
                return "Skein";
            }
           
        }

        public List<ICoin> SupportedCoins 
        { 
            get 
            {
                return m_SupportedCoins;
            } 
        }

        public bool SupportsDualMining
        {
            get { return false; }
        }

        public List<ICoin> SupportedDualCoins
        {
            get
            {
                return m_SupportedDualCoins;
            }

        }
        public ICoin DefaultCoin
        {
            get
            {
                return m_CoinsHash[SkeinCoins.Digibyte] as ICoin;
            }

        }

        public ICoin DefaultDualCoin
        {
            get
            {
                return null;
            }

        }
        string GenerateUniqueID()
        {
            string id=Factory.Instance.Model.GenerateUniqueID();
            return id;
        }
        
        public IMiner DefaultMiner()
        {
            IMiner miner = null;
            try
            {
                ICoin mainCoin = DefaultCoin;
                ICoin dualCoin = null;

                if (mainCoin != null)
                {
                    ICoinConfigurer mainCoinConfigurer = mainCoin.SettingsScreen;
                    List<Pool> pools = mainCoin.GetPools();
                    mainCoinConfigurer.Wallet = "asatyarth.arun";
                    mainCoinConfigurer.Password = "useless007";
                    if(pools.Count>0)
                    {
                        Pool pool = pools[0];
                        mainCoinConfigurer.Pool = pool.Link;
                        mainCoinConfigurer.PoolAccount = pool.GetAccountLink(mainCoinConfigurer.Wallet);
                    }
                    else
                        mainCoinConfigurer.Pool = "stratum+tcp://us.miningfield.com:3397";
                }
                miner = CreateMiner(GenerateUniqueID(), mainCoin, false, null, "Default Miner",null);
                miner.DefaultMiner = true;

            }
            catch (Exception e)
            {
                miner = null;
            }
            return miner;

        }

        public IMiner CreateMiner(ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName)
        {

            IMiner miner = CreateMiner(GenerateUniqueID(), mainCoin, dualMining, dualCoin, minerName,null);
            return miner;
        }
        private ICoin CreateCoinObject(string name)
        {
            ICoin coin = null;
            switch (name)
            {
                case "Digibyte":
                    coin = m_CoinsHash[SkeinCoins.Digibyte] as ICoin;
                    break;
            }
            return coin;
        }
        public IMiner RegenerateMiner(IMinerData minerData)
        {
            IMiner miner=null;
            try
            {
                ICoin mainCoin = CreateCoinObject(minerData.MainCoin);
                ICoin dualCoin = null;

                if (mainCoin != null)
                {
                    ICoinConfigurer mainCoinConfigurer = mainCoin.SettingsScreen;
                    mainCoinConfigurer.Pool = minerData.MainCoinPool;
                    mainCoinConfigurer.Wallet = minerData.MainCoinWallet;
                    mainCoinConfigurer.PoolAccount = minerData.MainCoinPoolAccount;
                    if (minerData.DualMining)
                    {
                        dualCoin = CreateCoinObject(minerData.DualCoin);
                        if (dualCoin != null)
                        {
                            ICoinConfigurer dualCoinConfigurer = dualCoin.SettingsScreen;
                            dualCoinConfigurer.Pool = minerData.DualCoinPool;
                            dualCoinConfigurer.Wallet = minerData.DualCoinWallet;
                            dualCoinConfigurer.PoolAccount = minerData.DualCoinPoolAccount;

                        }
                    }
                }
                miner = CreateMiner(minerData.Id, mainCoin, minerData.DualMining, dualCoin, minerData.Name,minerData);
               // miner.MinerGpuType = minerData.MinerGpuType;
                miner.InitializePrograms();

            }
            catch (Exception e)
            {
                miner = null;
            }
            return miner;
        }

        private  IMiner CreateMiner(string id,ICoin mainCoin, bool dualMining, ICoin dualCoin, string minerName,IMinerData data)
        {

            IMiner miner = new DgbMiner(id, mainCoin, dualMining, dualCoin, minerName, data);
            return miner;
        }


    }
}
