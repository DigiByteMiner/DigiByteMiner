using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace OneMiner.Model.UnZip
{
    class UnZipGZipStream : UnZipBase
    {

        public UnZipGZipStream(IUnzip next):base(next)
        {

        }
        public override bool UnzipUtil()
        {
            try
            {
                using (FileStream inFile = m_zipfileInfo.OpenRead())
                {
                    using (FileStream outFile = File.Create(OutputFolderName))
                    {
                        using (GZipStream Decompress = new GZipStream(inFile,
                                CompressionMode.Decompress))
                        {
                            Decompress.CopyTo(outFile);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
            }
            return false;


        }


    }
}
