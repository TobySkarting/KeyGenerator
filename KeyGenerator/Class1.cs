using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace KeyGenerator
{
    public static class Keygen1
    {
        private static XslCompiledTransform xslCompiledTransform;

        static Keygen1()
        {
            XmlDocument xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDocument.LoadXml(Properties.Resources.Xml1);
            xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(xmlDocument);
        }
        
        public static string GenerateResponse(string request, string edition, int userCount)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(request);
            XmlWriterSettings xmlWriterSettings = xslCompiledTransform.OutputSettings.Clone();
            xmlWriterSettings.NewLineChars = "\r\n";
            xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
            StringBuilder stringBuilder = new StringBuilder();
            XmlWriter results = XmlWriter.Create(stringBuilder, xmlWriterSettings);
            XsltArgumentList xsltArgumentList = new XsltArgumentList();
            xsltArgumentList.AddParam("version", "", 3);
            xsltArgumentList.AddParam("edition", "", edition);
            xsltArgumentList.AddParam("userspurchased", "", userCount);
            xslCompiledTransform.Transform(xmlDocument, xsltArgumentList, results);
            return WriteResponse(stringBuilder.ToString());
        }

        private static string WriteResponse(string xml)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters
            {
                Flags = CspProviderFlags.UseMachineKeyStore
            });
            rsa.FromXmlString(Properties.Resources.Rsa1);
            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            string text = Convert.ToBase64String(rsa.SignData(bytes, new SHA1CryptoServiceProvider()));
            return "<activationresponse>\r\n" + xml + "\r\n<signature>\r\n" + text + "\r\n</signature>\r\n</activationresponse>\r\n";
        }

        public static string GenerateSerial()
        {
            StringBuilder stringBuilder = new StringBuilder("A300");
            byte[] serial = new byte[20];
            Random random = new Random();
            random.NextBytes(serial);
            for (int i = 0; i < 5; i++)
            {
                stringBuilder.AppendFormat("-{0}{1}{2}{3}", new object[]
                    {
                        RandomByte(serial[i * 4]),
                        RandomByte(serial[i * 4 + 1]),
                        RandomByte(serial[i * 4 + 2]),
                        RandomByte(serial[i * 4 + 3])
                    });
            }
            char v1, v2;
            CorrectSerial(stringBuilder.ToString(), out v1, out v2);
            stringBuilder[2] = v1;
            stringBuilder[3] = v2;
            return stringBuilder.ToString();
        }

        private static readonly char[] acceptedChar = new char[]
        {
            '2',
            '3',
            '4',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'J',
            'K',
            'M',
            'N',
            'P',
            'R',
            'T',
            'W',
            'X',
            'Y',
            'Z'
        };

        public static char RandomByte(byte b)
        {
            return acceptedChar[b % acceptedChar.Length];
        }

        public static void CorrectSerial(string key, out char v1, out char v2)
        {
            uint num = GetHash(key);
            v1 = RandomByte((byte)(num % acceptedChar.Length));
            v2 = RandomByte((byte)(num / acceptedChar.Length % acceptedChar.Length));
        }

        public static uint GetHash(string key)
        {
            long num = 0L;
            for (int i = 0; i < key.Length; i++)
            {
                int num2 = (int)key[i];
                for (int j = 7; j >= 0; j--)
                {
                    bool flag = (num & 32768L) == 32768L ^ (num2 & 1 << j) != 0;
                    num = (num & 32767L) << 1;
                    if (flag)
                    {
                        num ^= 4129L;
                    }
                }
            }
            return (uint)num;
        }
    }
}
