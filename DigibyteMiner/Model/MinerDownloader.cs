using Microsoft.Win32;
using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.Model.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace OneMiner.Model
{
    class MinerDownloader
    {
        private string m_url;
        private string m_zipFilename;
        private string m_zipFilePath;
        private string m_UnzipedFilePath;
        private string m_verifyName;
        private IFileIO m_fileio = null;
        private IMiner  m_Miner = null;

        public MinerDownloader(string url,string verifyName,IMiner miner)
        {
            m_url = url;
            m_verifyName = verifyName;
            m_fileio = GetFileIOObject();
            m_Miner = miner;

        }
        private IFileIO GetFileIOObject()
        {
            IFileIO fileio = null;

            fileio = new AppData("OneMiner", "sample");
            if (fileio.Verify())
                return fileio;
            fileio = new LocalFolder("OneMiner", "sample");
            if (fileio.Verify())
                return fileio;

            throw new Exception("Couldnt create file");
        }
        public string GetTempBatFile(string Id,string type,string name)
        {
            try
            {
                string foldername = Id + "_" + type;
                string folderFullname = m_fileio.FolderName + @"\NonDownloadedMiners\" + foldername + @"\";
                DirectoryInfo folder = new DirectoryInfo(folderFullname);
                if(!folder.Exists)
                {
                    folder.Create();
                }
                //string batName = folderFullname + name + ".bat";
                return folderFullname;

            }
            catch (Exception )
            {
                return "";
            }
        }
        bool stilldownloading = false;
        /// <summary>
        /// doenloads the zip file uncompresses it and returns the foldername
        /// </summary>
        /// <returns></returns>
        public string DownloadFile()
        {
            try
            {
                //get the appdata path and get name of target zipfile
                m_zipFilename = GetFileName();
                m_zipFilePath = m_fileio.FolderName  + m_zipFilename;

                //get name of target unzipped folder
                FileInfo fileInfo = new FileInfo(m_zipFilePath);
                string curFile = fileInfo.FullName;
                m_UnzipedFilePath = curFile.Remove(curFile.Length -fileInfo.Extension.Length);
                m_UnzipedFilePath += "\\";
                DirectoryInfo unzippedFolder = new DirectoryInfo(m_UnzipedFilePath);
                FileInfo minerFile = new FileInfo(unzippedFolder + "\\"+ m_verifyName);
                //download only if unzipped folder dosent exist
                if (!minerFile.Exists)
                {
                    using (var client = new WebClient())
                    {
                        stilldownloading = true;
                        client.DownloadProgressChanged += client_DownloadProgressChanged;
                        client.DownloadFileCompleted += client_DownloadFileCompleted;
                        client.DownloadFileAsync(new Uri(m_url), m_zipFilePath);
                        while (stilldownloading)
                        {
                            Thread.Sleep(4000);
                        }
                    }
                    //unzip
                    return Decompress();
                }
                return unzippedFolder.FullName;


            }
            catch (Exception e)
            {
            }
            return "";

        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            m_Miner.DownloadPercentage=e.ProgressPercentage;
            Factory.Instance.ViewObject.UpDateMinerState();

        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            m_Miner.DownloadPercentage = 100;
            stilldownloading = false;
            Factory.Instance.ViewObject.UpDateMinerState();

        }
        /// <summary>
        /// decompresses the file
        /// </summary>
        public  string Decompress()
        {

            UnzipManager unzip = new UnzipManager(m_zipFilePath, m_verifyName, m_UnzipedFilePath);
            if (unzip.Unzip())
                return m_UnzipedFilePath;
            return "";

        }
       
        /// <summary>
        /// gets filename from the url of the miner. will be the name of the folder when decompressed
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            string filename = "";
            try
            {
                try
                {
                    Uri uri = new Uri(m_url);
                    filename = System.IO.Path.GetFileName(uri.LocalPath);
                    if(filename!="")
                        return filename;
                }
                catch (Exception e)
                {
                }
                Random random = new Random();
                int randomNumber = random.Next(0, 100);
                return "miner1_" + randomNumber;
            }
            catch (Exception ex)
            {

            }
            return "miner1";

        }
    }
}
