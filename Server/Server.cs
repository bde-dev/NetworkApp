using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace NetworkApp
{
    class Server
    {
        public void RunServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

            int index = 1;
            Console.WriteLine("\nAddresses at Host:");
            foreach (var hostIp in ipHost.AddressList)
            {
                IPAddress ip;
                ip = hostIp;
                Console.WriteLine(index + ": " + ip.ToString());
                index++;
            }

            Console.WriteLine("\nSelect address to listen on (leave blank and press enter for localhost):");

            //TODO: Add validation.
            string IP = Console.ReadLine();
            IPAddress ipAddr;
            IPEndPoint localEndPoint;

            if (IP == "")
            {
                ipAddr = ipHost.AddressList[0];
                localEndPoint = new IPEndPoint(ipAddr, 11111);
                Console.WriteLine("\nSelected IP to listen on: localhost");
            }
            else
            {
                ipAddr = ipHost.AddressList[(int.Parse(IP)) - 1];
                localEndPoint = new IPEndPoint(ipAddr, 11111);
                Console.WriteLine("\nSelected IP to listen on: " + ipAddr.ToString());
            }

            
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            

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

                //RECEIVE DATA.
                int numByte = client.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, numByte);
                Console.WriteLine("Text received -> " + data);

                //SEND DATA.
                byte[] sendMessage = Encoding.ASCII.GetBytes("Test Server");
                client.Send(sendMessage);

                //CLOSE CONNECTION.
                CloseConnection(client);
            }
        }

        private void CloseConnection(Socket client)
        {
            Console.WriteLine("\nClosing Connection...");
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Console.WriteLine("Connection Closed\n");
        }
    }
}