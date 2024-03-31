using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Startup
    {
        private string _ip;
        private int _port;

        public Startup(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void StartServer()
        {
            string ipAddress = "127.0.0.1";
            int port = 8080;

            TcpListener listener = new TcpListener(IPAddress.Parse(ipAddress), port);

            try
            {
                listener.Start();
                Console.WriteLine("Сервер запущен...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Новое подключение...");

                    byte[] buffer = new byte[1024];
                    int bytesRead = client.GetStream().Read(buffer, 0, buffer.Length);
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    string[] parts = dataReceived.Split('&');
                    string username = parts[0].Split('=')[1];
                    string password = parts[1].Split('=')[1];

                    bool isAuthenticated = (username == "user" && password == "pass");

                    string response = isAuthenticated ? "Аутентификация успешна" : "Ошибка аутентификации";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    client.GetStream().Write(responseData, 0, responseData.Length);

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
