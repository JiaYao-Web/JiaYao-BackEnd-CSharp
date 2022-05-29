using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JiaYao.Authorization
{
    //AES加密  传入,要加密的串和, 解密key
    public class AESEncrypt
    {
        public static string Encrypt(string input, string key = "dataplatform2022")
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);
            var iv = Encoding.UTF8.GetBytes("1012132405963708"); //偏移量,最小为16
            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, iv))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        var decryptedContent = msEncrypt.ToArray();

                        return Convert.ToBase64String(decryptedContent);
                    }
                }
            }
        }
    }
}
