using DigibyteMiner.Core.Interfaces;
using DigibyteMiner.Model.Config;
using DigibyteMiner.View;
using DigibyteMiner.View.v1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigibyteMiner.Core
{
    class Factory
    {
        enum AlgoEnums
        {
            Skein = 0,
            Equihash,
            End
        }
        private static Factory s_obj=null;
        private List<IHashAlgorithm> m_algorithms = new List<IHashAlgorithm>();
        Hashtable m_algoHash = new Hashtable();

        public DigibyteMiner CoreObject { get; set; }
        public Config Model { get; set; }
        public IView ViewObject { get; set; }
        public ILogger Logger { get; set; }
        public DateTime StartTime { get; set; }



        //private IView s_view = null;
        private Factory ()
	    {

            ViewObject = new V1View();
            Model = new Config();
            CoreObject = new DigibyteMiner();
            m_algoHash[AlgoEnums.Skein] = new Skein.Skein();
            m_algorithms.Add(m_algoHash[AlgoEnums.Skein] as IHashAlgorithm);
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
                case "Skein":
                    algo = new Skein.Skein();
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
                algos.Add(new Skein.Skein());
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
                return new Skein.Skein();
            }
        }

    }
}
