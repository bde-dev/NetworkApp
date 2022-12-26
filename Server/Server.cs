using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace NetworkApp
{
    class Server
    {
        public static void RunServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(10);

            while (true)
            {
                Console.WriteLine("\n===================================\nWaiting for connection...");

                Socket client = listener.Accept();
                Console.WriteLine("Connection accepted\n");

                byte[] bytes = new byte[1024];
                string data = null;

                int numByte = client.Receive(bytes);

                data += Encoding.ASCII.GetString(bytes, 0, numByte);

                Console.WriteLine("Text received -> " + data);
                byte[] message = Encoding.ASCII.GetBytes("Test Server");

                client.Send(message);

                Console.WriteLine("\nClosing Connection...");
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                Console.WriteLine("Connection Closed\n");
            }
        }
    }
}