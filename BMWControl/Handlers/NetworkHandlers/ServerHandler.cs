using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BMWControl.Handlers
{
    public class ServerHandler
    {
        private const string ServerIP = "sinxclan.net";
        private const int Port = 747;

        public ServerHandler()
        {
            
        }

        public static string GETString(string message)
        {
            try
            {
                using (TcpClient webhandle = new TcpClient(ServerIP, Port))
                {
                    webhandle.ReceiveTimeout = 3000;
                    webhandle.SendTimeout = 3000;

                    using (NetworkStream nStream = webhandle.GetStream())
                    {
                        nStream.WriteTimeout = 3000;
                        nStream.ReadTimeout = 3000;

                        string req = message;

                        byte[] data = Encoding.UTF8.GetBytes(req);
                        nStream.Write(data, 0, data.Length);

                        byte[] receive = new byte[webhandle.ReceiveBufferSize];
                        int count = nStream.Read(receive, 0, (webhandle.ReceiveBufferSize));
                        string response = Encoding.UTF8.GetString(receive, 0, count);

                        if (!response.ToLower().Contains("error"))
                            return response;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }

        //public void CheckForUpdate()
        //{
        //    if(GETString("")) 
        //}

        public string GetFileMD5(string FileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FileName))
                {
                    StringBuilder sb = new StringBuilder();
                    byte[] MD5 = md5.ComputeHash(stream);

                    for (int x = 0; x < MD5.Length; x++)
                        sb.Append(MD5[x].ToString("X2"));

                    return sb.ToString();
                }
            }
        }
    }
}
