using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SymmetricEncryption
{
    internal class CryptoAlgorithms
    {
        public static string EncryptText(string algorithmName, string key, string text, string cipherMode, string paddingMode)
        {
            //return $"{algorithm} {key} {cipherMode} {paddingMode}";
            string encryptedText = "xxxx";
            switch (algorithmName)
            {
                case ("aes"): encryptedText = EncryptAes();
                    break;
                case ("des"): encryptedText = EncryptDes();
                    break;
                case ("rijndael"): encryptedText = EncryptRijndael();
                    break;
                case ("TripleDes"): encryptedText = EncryptTripleDes(key, text, getCipherMode(cipherMode), getPaddingMode(paddingMode));
                    break;
                case ("rc2"): encryptedText = EncryptRC2();
                    break;  
            }
            return encryptedText;

        }
        private static string EncryptAes()
        {
            throw new NotImplementedException();
        }
        private static string EncryptDes()
        {
            throw new NotImplementedException();
        }
        private static string EncryptRC2()
        {
            throw new NotImplementedException();
        }
        private static string EncryptRijndael()
        {
            throw new NotImplementedException();
        }
        private static string EncryptTripleDes(string key, string text, CipherMode cipherMode, PaddingMode paddingMode)
        {
            //throw new NotImplementedException();
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(text);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            try
            {
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            }
            catch(ArgumentException e)
            {
                tripleDES.GenerateKey();
            }
            tripleDES.Mode = cipherMode;
            tripleDES.Padding =paddingMode;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        private static CipherMode getCipherMode(string cipherMode)
        {
            switch(cipherMode)
            {
                case ("ECB"): return CipherMode.ECB;
                case ("CBC"): return CipherMode.CBC;
                case ("CFB"): return CipherMode.CFB;
                case ("CTS"): return CipherMode.CTS;
                default: return CipherMode.CBC;
            }
        }
        private static PaddingMode getPaddingMode(string paddingMode)
        {
            switch (paddingMode)
            {
                case ("None"): return PaddingMode.None;
                case ("Zeros"): return PaddingMode.Zeros;
                case ("ISO10126"): return PaddingMode.ISO10126;
                case ("PKCS7"): return PaddingMode.PKCS7;
                case ("ANSIX923"): return PaddingMode.ANSIX923;
                default: return PaddingMode.None;

            }
           
        }
    }
}
