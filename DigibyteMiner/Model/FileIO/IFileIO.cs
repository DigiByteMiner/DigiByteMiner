using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.Model.FileIO
{
    interface IFileIO
    {
        string FileName { get;  }
        string FolderName { get;  }
        Boolean Verify();
    }
}
