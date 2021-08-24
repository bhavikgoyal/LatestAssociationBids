using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AssociationBids.Portal.Common
{
    public class Security
    {
        private static string AES_KEY = "axQ8kBnPjcmBcPWcmcKwpQ==";
        private static string AES_IV = "xzyLMjDtHu5kHwHYxEiOvg==";
        
        public Security()
        {

        }

        public static string Encrypt(string source)
        {
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.KeySize = 128;
            aesProvider.Key = Convert.FromBase64String(AES_KEY);
            aesProvider.IV = Convert.FromBase64String(AES_IV);

            string encryptedString;

            MemoryStream stream = new MemoryStream();
            BinaryFormatter serializer = new BinaryFormatter();

            CryptoStream cryptostream = new CryptoStream(stream, aesProvider.CreateEncryptor(aesProvider.Key, aesProvider.IV), CryptoStreamMode.Write);

            serializer.Serialize(cryptostream, source);
            cryptostream.FlushFinalBlock();

            encryptedString = Convert.ToBase64String(stream.ToArray());

            cryptostream.Close();

            return encryptedString;
        }

        public static string Decrypt(string source)
        {
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.KeySize = 128;
            aesProvider.Key = Convert.FromBase64String(AES_KEY);
            aesProvider.IV = Convert.FromBase64String(AES_IV);

            string deCypheredText = string.Empty;

            if (source != null || source.Length > 0)
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter deserializer = new BinaryFormatter();

                CryptoStream cryptostream = new CryptoStream(stream, aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV), CryptoStreamMode.Read);

                Byte[] buffer = Convert.FromBase64String(source);

                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;

                deCypheredText = (string)deserializer.Deserialize(cryptostream);

                cryptostream.Close();
            }

            return deCypheredText;
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

    }
}