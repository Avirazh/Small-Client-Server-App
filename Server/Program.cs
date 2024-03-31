using Server.DataAccess;
using Server.DataAccess.Model;
using Server.ForTestingPurposes;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.PopulateDbWithTestData();
        }        
    }
}