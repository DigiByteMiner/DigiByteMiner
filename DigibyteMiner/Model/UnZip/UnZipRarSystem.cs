using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OneMiner.Model.UnZip
{
    class UnZipRarSystem : UnZipBase
    {
        public UnZipRarSystem(IUnzip next)
            : base(next)
        {

        }

        private string LookForInstalledWinrar()
        {
            string objRarPath = "";
            try
            {
                RegistryKey objRegKey;
                objRegKey = Registry.ClassesRoot.OpenSubKey("WinRAR\\Shell\\Open\\Command");

                Object obj = objRegKey.GetValue("");

                objRarPath = obj.ToString();
                objRarPath = objRarPath.Substring(1, objRarPath.Length - 7);

                objRegKey.Close();
                FileInfo winrar = new FileInfo(objRarPath);
                if (!winrar.Exists)
                    throw new Exception("File not found");
            }
            catch (Exception e)
            {
                return "";
            }
            return objRarPath;
        }
        public override bool UnzipUtil()
        {
            try
            {

                string winrar = LookForInstalledWinrar();
                if (winrar == "")
                    return false;

                string objArguments;
                // " X G:\Downloads\samplefile.rar G:\Downloads\sampleextractfolder\"
                objArguments = " X " + " " + ZipFileName + " " + " " + OutputFolderName;

                ProcessStartInfo objStartInfo = new ProcessStartInfo();

                // Set the UseShellExecute property of StartInfo object to FALSE
                objStartInfo.UseShellExecute = false;
                objStartInfo.FileName = winrar;
                objStartInfo.Arguments = objArguments;
                objStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //objStartInfo.WorkingDirectory = WorkingDirectory + "\\";

                Process objProcess = new Process();
                objProcess.StartInfo = objStartInfo;
                objProcess.Start();
                objProcess.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;


        }
    }
}
