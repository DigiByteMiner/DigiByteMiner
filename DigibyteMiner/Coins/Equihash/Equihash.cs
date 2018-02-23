using OneMiner.Coins.Equihash;
using OneMiner.Coins.EthHash;
using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.EthHash;
using OneMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Equihash
{
    class Equihash : IHashAlgorithm
    {
        enum EquihashCoins
        {
            Zcash=0,
            ZenCash,
            ZClassic,
            Bitcoin_Gold,
            End
        }
        enum EthHashDualCoins
        {
            End
        }
        List<ICoin> m_SupportedDualCoins = new List<ICoin>();
        List<ICoin> m_SupportedCoins = new List<ICoin>();
        Hashtable m_CoinsHash = new Hashtable();

        public Equihash()
        {
            m_CoinsHash[EquihashCoins.Zcash] = new Zcash(this);
            m_CoinsHash[EquihashCoins.ZenCash] = new ZenCash(this);
            m_CoinsHash[EquihashCoins.ZClassic] = new ZClassic(this);
            m_CoinsHash[EquihashCoins.Bitcoin_Gold] = new BitcoinGold(this);


            //Now add it to the lists
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.Zcash] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.Bitcoin_Gold] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.ZenCash] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EquihashCoins.ZClassic] as ICoin);

        }
        public string Name
        {
            get
            {
                return "Equihash";
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
                return m_CoinsHash[EquihashCoins.Zcash] as ICoin;
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
                    mainCoinConfigurer.Wallet = "t1ZBzTwKs8wctQrcD6PmH3SRgJhAcLRwPZQ";

                    if (pools.Count > 0)
                    {
                        Pool pool = pools[0];
                        mainCoinConfigurer.Pool = pool.Link;
                        mainCoinConfigurer.PoolAccount = pool.GetAccountLink(mainCoinConfigurer.Wallet);
                    }
                    else
                        mainCoinConfigurer.Pool = "eu1-zcash.flypool.org:3333";
                }

                miner = CreateMiner(GenerateUniqueID(), mainCoin, false, null, "Default Zcash Miner",null);
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
                case "Zcash":
                    coin = m_CoinsHash[EquihashCoins.Zcash] as ICoin;
                    break;
                case "ZenCash":
                    coin = m_CoinsHash[EquihashCoins.ZenCash] as ICoin;
                    break;
                case "ZClassic":
                    coin = m_CoinsHash[EquihashCoins.ZClassic] as ICoin;
                    break;                
                case "Bitcoin Gold":
                    coin = m_CoinsHash[EquihashCoins.Bitcoin_Gold] as ICoin;
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

            IMiner miner = new ZCashMiner(id, mainCoin, dualMining, dualCoin, minerName, data);
            return miner;
        }


    }
}
