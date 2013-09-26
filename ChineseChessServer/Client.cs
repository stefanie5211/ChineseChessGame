using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace ChineseChessServer
{
    public class Client
    {
        private Thread clientThread;
        private EndPoint endPoint;   
        private string name;   
        private Socket socket;

        public Client(string name, EndPoint endPoint, Thread clientThread, Socket socket)   
        {
            this.clientThread = clientThread;
            this.endPoint = endPoint;
            this.name = name;
            this.socket = socket;
        } 
  
        public override string ToString()   
        {
            return endPoint.ToString() + " : " + name;   
        }   

        public Thread ClientThread   
        {
            get { return clientThread; }
            set { clientThread = value; }   
        }   

        public EndPoint ClientEndPoint   
        {
            get { return endPoint; }
            set { endPoint = value; }   
        }   

        public string Name   
        {
            get { return name; }
            set { name = value; }   
        }

        public Socket SocketInfo 
        {
            get { return socket; }
            set { socket = value; }   
        }   
    }
}
