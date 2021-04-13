using System.Text;
using System.Security.Cryptography;

namespace LabBook.Security
{
    public class Encrypt
    {
        public static string MD5Encrypt(string value)
        {
            MD5CryptoServiceProvider crypt = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(value);
            data = crypt.ComputeHash(data);
            string encrypt = "";
            for (var i = 0; i < data.Length; i++)
            {
                encrypt += data[i].ToString("x2").ToLower();
            }

            return encrypt;
        }
    }
}
