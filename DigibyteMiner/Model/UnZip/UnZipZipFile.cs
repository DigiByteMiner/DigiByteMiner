using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMiner.Model.UnZip
{
    class UnZipZipFile : UnZipBase
    {
        public UnZipZipFile(IUnzip next)
            : base(next)
        {

        }
        public override bool UnzipUtil()
        {
            try
            {
                
                ZipFile.ExtractToDirectory(ZipFileName, OutputFolderName);

                return true;
            }
            catch (Exception e)
            {
            }
            return false;


        }
    }
}
