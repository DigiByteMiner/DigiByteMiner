using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigibyteMiner.Core.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string error);
        void LogError(string error);
    }
}
