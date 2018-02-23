using OneMiner.Core.Interfaces;
using OneMiner.Model.Config;
using OneMiner.View;
using OneMiner.View.v1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core
{
    class Factory
    {
        enum AlgoEnums
        {
            EthHash = 0,
            Equihash,
            Cryptonyte,
            End
        }
        private static Factory s_obj=null;
        private List<IHashAlgorithm> m_algorithms = new List<IHashAlgorithm>();
        Hashtable m_algoHash = new Hashtable();

        public OneMiner CoreObject { get; set; }
        public Config Model { get; set; }
        public IView ViewObject { get; set; }
        public ILogger Logger { get; set; }
        public DateTime StartTime { get; set; }



        //private IView s_view = null;
        private Factory ()
	    {

            ViewObject = new V1View();
            Model = new Config();
            CoreObject = new OneMiner();
            m_algoHash[AlgoEnums.EthHash] = new EthHash.EthHash();
            m_algoHash[AlgoEnums.Equihash] = new Equihash.Equihash();
            m_algorithms.Add(m_algoHash[AlgoEnums.EthHash] as IHashAlgorithm);
            m_algorithms.Add(m_algoHash[AlgoEnums.Equihash] as IHashAlgorithm);
            StartTime = DateTime.Now;
	    }
        private void Init()
        {
            //if there is no config file, we shud popolate the list of algos so that later we can add miner programs etc
            //It only adds if there if algo is not present i config file
            Model.AddAlgorithms(m_algorithms);
        }
        public IHashAlgorithm CreateAlgoObject(string name)
        {
            IHashAlgorithm algo = null;
            switch(name)
            {
                case "Ethhash":
                    algo = new EthHash.EthHash();
                    break;
                case "Equihash":
                    algo = new Equihash.Equihash();
                    break;
                case "CryptoNote":
                    algo = new Coins.CryptoNote.CryptoNote();
                    break;

            }
            return algo;
        }
        public static Factory Instance
        {
            get
            {
                if (s_obj == null)
                {
                    s_obj = new Factory();
                    s_obj.Init();
                }
                return s_obj;
            }
        }

        //Todo: maybe this shud be created w=everytime addminer is clicked. that way we wont be reusing ojects
        public List<IHashAlgorithm> Algorithms
        {
            get
            {
                List<IHashAlgorithm> algos = new List<IHashAlgorithm>();
                algos.Add(new EthHash.EthHash());
                algos.Add(new Equihash.Equihash());
                algos.Add(new Coins.CryptoNote.CryptoNote());
                return algos;
            }
        }
        /// <summary>
        /// This will be the algo that will be given precendence if we hav to display anywhere
        /// </summary>
        public IHashAlgorithm DefaultAlgorithm
        {
            get
            {
                //Todo: maybe this shud be created w=everytime addminer is clicked. that way we wont be reusing ojects
                //return m_algoHash[AlgoEnums.EthHash] as IHashAlgorithm;
                return new EthHash.EthHash();
            }
        }

    }
}
