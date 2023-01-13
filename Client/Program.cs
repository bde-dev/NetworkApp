using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetworkApp;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client");
            NetworkApp.Client client = new NetworkApp.Client();
            client.RunClient();
        }
    }
}