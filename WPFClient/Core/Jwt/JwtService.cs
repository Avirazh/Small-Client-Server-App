using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WPFClient.Jwt
{
    public static class JwtService
    {
        private static string tokenFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "token.txt");

        public static void CreateFile(string content)
        {
            try
            {
                using (FileStream fileStream = new FileStream(tokenFilePath, FileMode.OpenOrCreate))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(content);

                    fileStream.Write(info, 0, info.Length);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static string ReadToken()
        {
            try
            {
                var token = string.Empty;

                using (StreamReader sr = new StreamReader(tokenFilePath, Encoding.UTF8))
                {
                    token = sr.ReadToEnd();
                }
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}
