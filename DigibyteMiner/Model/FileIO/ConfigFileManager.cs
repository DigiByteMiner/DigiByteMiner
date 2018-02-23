using OneMiner.Model.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OneMiner.Model
{
    /// <summary>
    /// Todo:
    /// have a primary(local) and secondayy(appdata)
    /// while reading
    ///     read primary
    ///     if failed(becoz its missing)
    ///         read secondary
    ///         copy to primary
    ///     else(if success)
    ///         copy to secondary
    /// while writing
    ///     write primary
    ///     if failed 
    ///         write secondary
    ///         copy to primary
    ///     else
    ///         copy to sencondary
    /// </summary>
    class ConfigFileManager
    {
        const string minerfileName = "oneminer.json";
        string m_filepath = "";
        private IFileIO m_fileio = null;//object whic cretes the config. usually appdata
        //private string m_data = "";
        public string Data { get; set; }
        private IFileIO GetFileIOObject()
        {
            IFileIO fileio = null;
            fileio = new AppData("OneMiner", minerfileName);
            if (fileio.Verify())
                return fileio;
            fileio = new LocalFolder("OneMiner", minerfileName);
            if (fileio.Verify())
                return fileio;
         
            throw new Exception("Couldnt create file");
        }
        public ConfigFileManager()
        {
            try
            {
                Data = "";
                m_fileio = GetFileIOObject();
                LoadData();
            }
            catch (Exception e)
            {

            }
            

        }
        private string GetFileName()
        {
            return m_fileio.FileName;

        }
        private void LoadData()
        {
            try
            {
                string filename = GetFileName();
                Stream stream = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(stream);
                Data = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {

            }
        }
        public void WriteData()
        {
            try
            {
                string filename = GetFileName();
                Stream stream = new FileStream(filename, FileMode.Create);
                StreamWriter sr = new StreamWriter(stream);
                sr.Write(Data);
                sr.Close();
            }
            catch (Exception e)
            {

            }
        }
        //if config file couldnt be read for whatever reason, then any new data entries would overwrite previous values
        //so take a backup
        public void BackupConfigFile()
        {
            try
            {
                bool unique = false;
                Random rnd = new Random();
                string oldfile = GetFileName();
                FileInfo f1 = new FileInfo(oldfile);
                FileInfo f2=null;
                int tries = 0;
                while (!unique && tries<5)
                {
                    int num = rnd.Next(1, 9999999);

                    string filename = oldfile + num.ToString();

                    f2 = new FileInfo(filename);
                    if (!f2.Exists)
                    {
                        unique = true;
                    }
                    tries++;
                }
                if(unique)
                {
                    f1.CopyTo(f2.FullName);
                }
             
            }
            catch (Exception)
            {
            }

        }
    }
}
