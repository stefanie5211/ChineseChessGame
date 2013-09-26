using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseChessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer gameServer = new GameServer();
            gameServer.RunServer();
        }
    }
}
