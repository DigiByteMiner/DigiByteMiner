using OneMiner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.View.v1.AddMinerScreen
{
    public interface IMinerContainer
    {
        ICoin SelectedCoin { get; set; }
        ICoin SelectedDualCoin { get; set; }
        bool BAddDualMiner { get; set; }
        
        void DisableFinishButton();
        void EnableNextButton();
        void DisableNextButton();
        void MakeSelectedCoin(ICoin coin);
        void MakeSelectedDualCoin(ICoin coin);
        
        string GetMinername();
        

    }
}
