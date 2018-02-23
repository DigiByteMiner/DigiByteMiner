using OneMiner.Coins.EthHash;
using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.Model.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.EthHash
{
    class EthHash : IHashAlgorithm
    {
        enum EthHashCoins{
            Ethereum=0,
            EtherClassic,
            Ubiq,
            Expanse,
            End
        }
        enum EthHashDualCoins
        {
            Siacoin=0,
            Decred,
            End
        }
        List<ICoin> m_SupportedDualCoins = new List<ICoin>();
        List<ICoin> m_SupportedCoins = new List<ICoin>();
        Hashtable m_CoinsHash = new Hashtable();

        public EthHash()
        {
            m_CoinsHash[EthHashCoins.Ethereum] = new Ethereum(this);
            m_CoinsHash[EthHashCoins.EtherClassic] = new EtherClassic(this);
            m_CoinsHash[EthHashCoins.Expanse] = new Expanse(this);
            m_CoinsHash[EthHashCoins.Ubiq] = new Ubiq(this);

            m_CoinsHash[EthHashDualCoins.Decred] = new Decred(this);
            m_CoinsHash[EthHashDualCoins.Siacoin] = new Sia(this);

            //Now add it to the lists
            m_SupportedCoins.Add(m_CoinsHash[EthHashCoins.EtherClassic] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EthHashCoins.Ethereum] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EthHashCoins.Expanse] as ICoin);
            m_SupportedCoins.Add(m_CoinsHash[EthHashCoins.Ubiq] as ICoin);


            m_SupportedDualCoins.Add(m_CoinsHash[EthHashDualCoins.Decred] as ICoin);
            m_SupportedDualCoins.Add(m_CoinsHash[EthHashDualCoins.Siacoin] as ICoin);

        }
        public string Name
        {
            get
            {
                return "Ethhash";
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
            get { return true; }
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
                return m_CoinsHash[EthHashCoins.Ethereum] as ICoin;
            }

        }

        public ICoin DefaultDualCoin
        {
            get
            {
                return m_CoinsHash[EthHashDualCoins.Decred] as ICoin;
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
                    mainCoinConfigurer.Wallet = "0x033ff6918d434cef3887d8e529c14d1bcb91ca8b";

                    if(pools.Count>0)
                    {
                        Pool pool = pools[0];
                        mainCoinConfigurer.Pool = pool.Link;
                        mainCoinConfigurer.PoolAccount = pool.GetAccountLink(mainCoinConfigurer.Wallet);
                    }
                    else
                        mainCoinConfigurer.Pool = "eu1.ethermine.org:4444";
                }
                miner = CreateMiner(GenerateUniqueID(), mainCoin, false, null, "Default Ethereum Miner",null);
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
                case "Ethereum":
                    coin = m_CoinsHash[EthHashCoins.Ethereum] as ICoin;
                    break;
                case "Ethereum Classic":
                    coin = m_CoinsHash[EthHashCoins.EtherClassic] as ICoin;
                    break;
                case "Expanse":
                    coin = m_CoinsHash[EthHashCoins.Expanse] as ICoin;
                    break;
                case "Ubiq":
                    coin = m_CoinsHash[EthHashCoins.Ubiq] as ICoin;
                    break;

                case "Decred":
                    coin = m_CoinsHash[EthHashDualCoins.Decred] as ICoin;
                    break;
                case "SiaCoin":
                    coin = m_CoinsHash[EthHashDualCoins.Siacoin] as ICoin;
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

            IMiner miner = new EthereumMiner(id, mainCoin, dualMining, dualCoin, minerName, data);
            return miner;
        }


    }
}
