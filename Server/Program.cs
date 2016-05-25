using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Network;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
         //   NetPeerConfiguration config = new NetPeerConfiguration("Server");
        //    NetServer server = new NetServer(config);
        //    server.Start();
            ServerConnectionContainer serverConnection = ConnectionFactory.CreateServerConnectionContainer(9001, true);
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            bool hasDone = false;
            while (true)

                if (((watch.ElapsedMilliseconds/1000)%10) == 0)
                {
                    if (!hasDone)
                    {
                //        Console.WriteLine(server.Status);
                //        server.DiscoverLocalPeers(9001);
                        hasDone = true;
                    }
                }
                else
                {
                    hasDone = false;
                }
        }
        }
    }
