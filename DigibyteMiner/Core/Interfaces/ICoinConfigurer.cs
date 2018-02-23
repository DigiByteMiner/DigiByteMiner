using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Core.Interfaces
{
    public interface ICoinConfigurer
    {
        /// <summary>
        /// parent form for this form if this form is gonna be binded to something instead of as a standaline
        /// </summary>
        /// <param name="parent"></param>
        void AssignParent(object parent);

        void AssignCoin(ICoin coin);

        string Pool {get;set;}

        string Wallet { get; set; }
        string PoolAccount { get; set; }

    }
}
