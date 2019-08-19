using System;
using System.Security.Cryptography;
using System.Text;

namespace api.visanet.validate.utils
{
    public class CSEncryptDecrypt
    {
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        
        public static string DecryptData(string sData, string sKey, string sVector)
        {
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider();
            AES.Mode = CipherMode.CBC;
            AES.Padding = PaddingMode.PKCS7;
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Key = StringToByteArray(sKey);
            AES.IV = StringToByteArray(sVector);

            byte[] src = System.Convert.FromBase64String(sData);
            using (ICryptoTransform aes = AES.CreateDecryptor())
            {
                byte[] dest = aes.TransformFinalBlock(src, 0, src.Length);
                return Encoding.UTF8.GetString(dest);
            }
        }

        public static string EncryptData(string sData, string sKey, string sVector)
        {
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider();
            AES.Mode = CipherMode.CBC;
            AES.Padding = PaddingMode.PKCS7;
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Key = StringToByteArray(sKey);
            AES.IV = StringToByteArray(sVector);

            byte[] src = Encoding.UTF8.GetBytes(sData);
            using (ICryptoTransform aes = AES.CreateEncryptor())
            {
                byte[] dest = aes.TransformFinalBlock(src, 0, src.Length);
                return Convert.ToBase64String(dest);
            }
        }

        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}