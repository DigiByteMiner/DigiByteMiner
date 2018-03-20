using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace DigibyteMiner.Model
{
    class Utils
    {
        public  static string MD5String(string str)
        {
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            System.Security.Cryptography.MD5CryptoServiceProvider md5provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new System.Text.UTF8Encoding().GetBytes(str));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2")); //lowerCase; X2 if uppercase desired
            }
            return hash.ToString();
        }
        public static string MD5File(string filepath)
        {
            string str="";
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filepath))
                {
                    var hash = md5.ComputeHash(stream);
                    str= BitConverter.ToString(hash).Replace("-", "");
                }
            }
            return str;
        }
    }
}
