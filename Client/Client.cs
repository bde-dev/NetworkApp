using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace NetworkApp
{
    class Client
    {
        public void RunClient()
        {
            try
            {
                Console.WriteLine("\n===============================");

                Console.WriteLine("Client Host Name: " + Dns.GetHostName());
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

                Console.WriteLine("\nAddresses at Host:");
                foreach (var hostIp in ipHost.AddressList)
                {
                    IPAddress ip;
                    ip = hostIp;
                    Console.WriteLine(ip.ToString());
                }
                
                Console.WriteLine("\nEnter IP Address to connect to (leave blank and press enter for localhost):");
                string IP = Console.ReadLine();
                IPAddress ipAddr;
                IPEndPoint localEndPoint;
                if (IP == "")
                {
                    ipAddr = ipHost.AddressList[0];
                    localEndPoint = new IPEndPoint(ipAddr, 11111);
                }
                else
                {
                    ipAddr = IPAddress.Parse(IP);
                    localEndPoint = new IPEndPoint(ipAddr, 11111);
                }
                


                Console.WriteLine("\nCreating local socket...");
                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Local socket created\n");

                try
                {
                    Console.WriteLine("Attempting to connect...");
                    sender.Connect(localEndPoint);

                    Console.WriteLine("Socket connected to -> {0}", sender.RemoteEndPoint.ToString());


                    //WHILE CONNECTED START.
                    Console.WriteLine("Type message to server, or press enter to send 'Test Client'");
                    string input;
                    input = Console.ReadLine();
                    if (input == "" || input == null)
                        input = "Test Client";
                    byte[] sendMessage = Encoding.ASCII.GetBytes(input);
                    int byteSent = sender.Send(sendMessage);

                    byte[] recMessage = new byte[1024];

                    int recByte = sender.Receive(recMessage);
                    Console.WriteLine("Message from server -> {0}", Encoding.ASCII.GetString(recMessage, 0, recByte));

                    //WHILE CONNECTED END.
                    Console.WriteLine("Closing connection...");
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    Console.WriteLine("Connection closed");
                    Console.ReadLine();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("Argument null exception: {0}", ane.ToString());
                    Console.ReadLine();
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Socket exception: {0}", se.ToString());
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown exception: {0}", e.ToString());
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception: {0}", e.ToString());
                Console.ReadLine();
            }
        }
    }
}