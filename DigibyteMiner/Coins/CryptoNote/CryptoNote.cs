using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Coins.CryptoNote
{
    class CryptoNote : IHashAlgorithm
    {
        enum EquihashCoins
        {
            Monero=0,
            Electroneum,
            End
        }
        enum EthHashDualCoins
        {
            End
        }
        List<ICoin> m_SupportedDualCoins = new List<ICoin>();
        List<ICoin> m_SupportedCoins = new List<ICoin>();
        Hashtable m_CoinsHash = new Hashtable();

        public CryptoNote()
        {
            m_CoinsHash[EquihashCoins.Monero] = new Monero(this);
            m_CoinsHash[EquihashCoins.Electroneum] = new Electroneum(this);


            //Now add it to the lists
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.Monero] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.Electroneum] as ICoin);

        }
        public string Name
        {
            get
            {
                return "CryptoNote";
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
                return null;
            }

        }
        public ICoin DefaultCoin
        {
            get
            {
                return m_CoinsHash[EquihashCoins.Monero] as ICoin;
            }

        }

        public ICoin DefaultDualCoin
        {
            get
            {
                return m_CoinsHash[EthHashDualCoins.End] as ICoin;
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
                    mainCoinConfigurer.Wallet = "463tWEBn5XZJSxLU6uLQnQ2iY9xuNcDbjLSjkn3XAXHCbLrTTErJrBWYgHJQyrCwkNgYvyV3z8zctJLPCZy24jvb3NiTcTJ";

                    if (pools.Count > 0)
                    {
                        Pool pool = pools[0];
                        mainCoinConfigurer.Pool = pool.Link;
                        mainCoinConfigurer.PoolAccount = pool.GetAccountLink(mainCoinConfigurer.Wallet);
                    }
                    else
                        mainCoinConfigurer.Pool = "stratum+tcp://mine.moneropool.com:3333";
                }

                miner = CreateMiner(GenerateUniqueID(), mainCoin, false, null, "Default Monero Miner",null);
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
                case "Monero":
                    coin = m_CoinsHash[EquihashCoins.Monero] as ICoin;
                    break;
                case "Electroneum":
                    coin = m_CoinsHash[EquihashCoins.Electroneum] as ICoin;
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
                //miner.MinerGpuType = minerData.MinerGpuType;
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

            IMiner miner = new CryptoNoteMiner(id, mainCoin, dualMining, dualCoin, minerName, data);
            return miner;
        }


    }
}
