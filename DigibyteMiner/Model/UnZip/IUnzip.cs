using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigibyteMiner.Model.UnZip
{
    interface  IUnzip
    {
        bool Unzip();
        void Init(string zipfile, string outputfile, string verifyName);
        void SetNExtChain(IUnzip unzip);
    }
}
