using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OneMiner.Model.UnZip
{
    class UnZipBase : IUnzip
    {
        public string ZipFileName { get; set; }
        public string OutputFolderName { get; set; }
        public string VerifyName { get; set; }//will check to see if this file is there for verify  to succeed

        protected FileInfo m_zipfileInfo = null;
        public IUnzip m_NextActor = null;
        public UnZipBase(IUnzip next)
        {
            m_NextActor = next;
        }
        public virtual bool UnzipUtil()
        {
            return false;
        }
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
                return;
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public void LookExtractInside(DirectoryInfo parent, FileInfo file)
        {
            DirectoryInfo[] folders = parent.GetDirectories();
            foreach (DirectoryInfo item in folders)
            {
                //identify the internal folder which contains this file
                FileInfo myFile = new FileInfo(item.FullName +"\\"+ file.Name);
                if(myFile.Exists)
                {
                    //this folder contains my miner
                    DirectoryCopy(item.FullName, parent.FullName, true);
                    return;
                }
            }

        }
        public bool Verify()
        {
            DirectoryInfo folder = new DirectoryInfo(OutputFolderName);
            string miner=folder.FullName+@"\"+VerifyName;
            FileInfo minerFile=new FileInfo(miner);
            if (folder.Exists)
            {
                if (minerFile.Exists)
                    return true;
                else
                {
                    LookExtractInside(folder, minerFile);
                    minerFile.Refresh();
                    if (minerFile.Exists)
                        return true;
                }
            }
            return false;
        }
        public bool Unzip()
        {
            try
            {
                bool success = UnzipUtil();
                if (success && Verify())
                    return true;
                else
                {
                    //Call the next item in chain of responsibility
                    if (m_NextActor == null)
                        return false;
                    m_NextActor.Init(ZipFileName, OutputFolderName, VerifyName);
                    return m_NextActor.Unzip();
                }
            }
            catch (Exception e)
            {
            }
            return false;
        }
        public void Init(string zipfile, string outputfile, string verifyName)
        {
            ZipFileName = zipfile;
            OutputFolderName = outputfile;
            VerifyName = verifyName;
            m_zipfileInfo = new FileInfo(zipfile);
        }
        /// <summary>
        /// used to change any actors in the chain of responsibility later
        /// </summary>
        /// <param name="unzip"></param>
        public void SetNExtChain(IUnzip unzip)
        {
            m_NextActor = unzip;
        }
    }
}
