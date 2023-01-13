using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetworkApp;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server");
            NetworkApp.Server server= new NetworkApp.Server();
            server.RunServer();
        }
    }
}