using System;
using System.Collections.Generic;
using System.IO;
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
            string encryptedText = "";
            SymmetricAlgorithm algorithm;
            switch (algorithmName)
            {
                case ("DES"): 
                   algorithm = DES.Create();
                    break;
                case ("Rijndael"):
                    algorithm = Rijndael.Create();
                    break;
                case ("TripleDes"): 
                    algorithm = TripleDES.Create();
                    break;
                case ("RC2"): 
                    algorithm = RC2.Create();
                    break;
                default:
                    algorithm = Aes.Create();
                    break;
            }
            encryptedText = ExecuteEncryptionAlgorithm(algorithm, key, text, getCipherMode(cipherMode), getPaddingMode(paddingMode));
            return encryptedText;

        }
        private static string ExecuteEncryptionAlgorithm(SymmetricAlgorithm algorithm, string key, string text, CipherMode cipherMode, PaddingMode paddingMode)
        {
            try
            {
                algorithm.Key = UTF8Encoding.UTF8.GetBytes(key);
            }
            catch (ArgumentException ae)
            {
                algorithm.GenerateKey();
            }
            catch(CryptographicException)
            {
                algorithm.GenerateKey();
            }
            byte[] inputBytes = UTF8Encoding.UTF8.GetBytes(text);
            algorithm.GenerateIV();
            algorithm.FeedbackSize = 8;
            algorithm.Padding = paddingMode;
            algorithm.Mode = cipherMode;
            byte[] encryptedBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                }
                 encryptedBytes = memoryStream.ToArray();
             }
          
            return Convert.ToBase64String(encryptedBytes);
        }
        private static CipherMode getCipherMode(string cipherMode)
        {
            switch(cipherMode)
            {
                case ("ECB"): return CipherMode.ECB;
                case ("CFB"): return CipherMode.CFB;
                default: return CipherMode.CBC;
            }
        }
        private static PaddingMode getPaddingMode(string paddingMode)
        {
            switch (paddingMode)
            {
                case ("ISO10126"): return PaddingMode.ISO10126;
                case ("PKCS7"): return PaddingMode.PKCS7;
                case ("ANSIX923"): return PaddingMode.ANSIX923;
                default: return PaddingMode.Zeros;

            }
           
        }
    }
}
