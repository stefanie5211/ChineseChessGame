using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

namespace ChineseChessServer
{
    class GameServer
    {
        private IPAddress ipAddress;
        private TcpListener tcpListener;
        private int port;
        private Socket clientSocket;
        private Thread clientServiceThread;
        private int clientCounter;
        private ArrayList clients;
        private List<int> seats;
        private List<VSGroup> vsGroups;

        public GameServer()
        {
            port = 8500;
            //ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            ipAddress = IPAddress.Parse("127.0.0.1");
            clientCounter = 0;
            clients = new ArrayList();
            //八个座位,0为可用，1为不可用
            seats = new List<int>() 
            {
                0,
                0,0,    0,0, 
                0,0,    0,0,
            };
            //4个对战组
            vsGroups = new List<VSGroup>();
            vsGroups.Add(null);
            vsGroups.Add(new VSGroup());
            vsGroups.Add(new VSGroup());
            vsGroups.Add(new VSGroup());
            vsGroups.Add(new VSGroup());
            Console.WriteLine("Server is running successfully！");
        }

        public void RunServer()
        {
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();
            while (true)
            {
                try
                {
                    Socket socket = tcpListener.AcceptSocket();
                    clientSocket = socket;                  
                    clientServiceThread = new Thread(new ThreadStart(RunClientService));
                    clientServiceThread.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void SendMessageToClient(Client client, string message)
        {
            Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);
            Socket socket = client.SocketInfo;
            socket.Send(bytes, bytes.Length, 0);
        }

        private string GetChatterList()
        {
            string result = "";

            for (int i = 0; i < clients.Count; ++i)
            {
                result += ((Client)clients[i]).Name + "|" + ((Client)clients[i]).ClientEndPoint.ToString() + "|";
            }
            return result;
        }

        private string GetSeatList()
        {
            string result = "";

            for (int i = 1; i < 9; ++i )
            {
                result += (seats[i].ToString() + "|");
            }

            return result;
        }

        private void SetVSGroup(int seatNumber, int inOrOut, Client client)
        {
            Client playerB = null;
            Client playerA = null;
            if (inOrOut == 1)
            {
                if (seatNumber%2 == 1)
                {
                    vsGroups[(seatNumber+1)/2].PlayerA = client;
                    playerB = vsGroups[(seatNumber + 1) / 2].PlayerB;          
                    if (playerB != null)
                    {                        
                        SendMessageToClient(client,
                            "PINF|" + playerB.Name + "|" + vsGroups[(seatNumber + 1) / 2].PlayerBReady.ToString() + "|" + "even" + "|");
                        SendMessageToClient(playerB,
                            "PINF|" + client.Name + "|" +"False" + "|" + "odd" + "|");
                    }
                }
                else
                {
                    vsGroups[seatNumber/2].PlayerB = client;
                    playerA = vsGroups[seatNumber / 2].PlayerA;
                    if (playerA != null)
                    {
                        SendMessageToClient(client,
                            "PINF|" + playerA.Name + "|" + vsGroups[(seatNumber) / 2].PlayerAReady.ToString() + "|" + "odd" + "|");
                        SendMessageToClient(playerA,
                            "PINF|" + client.Name + "|" + "False" + "|" + "even" + "|");
                    }
                }
            } 
            else
            {
                if (seatNumber % 2 == 1)
                {
                    vsGroups[(seatNumber + 1)/2].PlayerA = null;
                    playerB = vsGroups[(seatNumber + 1) / 2].PlayerB;
                    if (playerB != null)
                    {
                        SendMessageToClient(playerB,
                            "PINF|" + "Player1" + "|" + "leave" + "|" + "odd" + "|");
                    }
                }
                else
                {
                    vsGroups[seatNumber/2].PlayerB = null;
                    playerA = vsGroups[seatNumber / 2].PlayerA;
                    if (playerA != null)
                    {
                        SendMessageToClient(playerA,
                            "PINF|" + "Player2" + "|" + "leave" + "|" + "even" + "|");
                    }
                }
            }
        }

        private void RunClientService()
        {
            Client client = null;
            Socket socket = clientSocket;
            bool keepalive = true;

            while (keepalive)
            {
                Byte[] buffer = new Byte[1024];
                int bufferLength = 0;
                try
                {
                    bufferLength = socket.Available;
                    socket.Receive(buffer, 0, bufferLength,SocketFlags.None);

                    if (bufferLength == 0)
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Receive Error:" + e.Message);
                    return;
                }

                string clientMessage = System.Text.Encoding.ASCII.GetString(buffer).Substring(0,bufferLength);
                string[] tokens = clientMessage.Split(new Char[] { '|' });
                Console.WriteLine(clientMessage);

                switch (tokens[0])
                {
                    case "CONN": //建立一个新的客户端连接，将现有的用户列表发送给新用户并告知其他用户有一个新用户加入
                        {
                            for (int i = 0; i < clients.Count; ++i)
                            {
                                Client cl = (Client)clients[i];
                                SendMessageToClient(cl, "JOIN|" + tokens[1] + "|" + socket.RemoteEndPoint.ToString() + "|");
                            }

                            ++clientCounter;
                            Console.WriteLine("接受到客户端连接！{0} 当前在线用户数量：{1}", socket.RemoteEndPoint, clientCounter.ToString());
                            EndPoint endpoint = socket.RemoteEndPoint;
                            client = new Client(tokens[1], endpoint, clientServiceThread, socket);
                            clients.Add(client);
                            string message1 = "LIST|" + GetChatterList() + "\r\n";
                            string meesage2 = "SEAT|" + GetSeatList() + "\r\n";
                            SendMessageToClient(client, message1);
                            SendMessageToClient(client, meesage2);

                            break;
                        }

                    case "CHAT": //将聊天发送给所有用户
                        {
                            for (int i = 0; i < clients.Count; ++i)
                            {
                                Client cl = (Client)clients[i];
                                SendMessageToClient(cl, clientMessage + '|');
                            }

                            break;
                        }

                    case "PRIV": //将悄悄话发送给某个用户
                        {
                            int n = int.Parse(tokens[2]);
                            Client playerA = vsGroups[(n + 1) / 2].PlayerA;
                            Client playerB = vsGroups[(n + 1) / 2].PlayerB;
                            if (playerA!=null)
                            {
                                SendMessageToClient(playerA,"PRIV|" + tokens[1] + "|");
                            }
                            if (playerB!=null)
                            {
                                SendMessageToClient(playerB, "PRIV|" + tokens[1] + "|");
                            }
                            break;
                        }

                    case "GONE": //从用户列表中除去一个已离开的用户并告知其他的用户某某已经离开了
                        {
                            int removeIndex = 0;
                            bool found = false;
                            int c = clients.Count;

                            for (int i = 0; i < clients.Count; i++)
                            {
                                Client cl = (Client)clients[i];
                                SendMessageToClient(cl, clientMessage + '|' + client.ClientEndPoint + '|');
                                if (cl.ClientEndPoint == client.ClientEndPoint)
                                {
                                    removeIndex = i;
                                    found = true;
                                }
                            }

                            if (found)
                            {
                                --clientCounter;
                                Console.WriteLine("{0}已断开连接！ 当前在线用户数量：{1}", client.Name, clientCounter.ToString());
                                clients.RemoveAt(removeIndex);                       
                                socket.Close();
                            }
                            keepalive = false;
                            break;
                        }

                    case "SEAT" :
                        {
                            int n = int.Parse(tokens[1]);
                            int t = int.Parse(tokens[2]);
                            seats[n] = t;                          
                            for (int i = 0; i < clients.Count; i++)
                            {
                                Client cl = (Client)clients[i];
                                if(cl != client)
                                {
                                    SendMessageToClient(cl, clientMessage + "|");
                                }
                            }
                            SetVSGroup(n, t, client);
                            break;
                        }
                    case "REDY":
                        {
                            int n = int.Parse(tokens[1]);
                            Client pA = vsGroups[(n + 1) / 2].PlayerA;
                            Client pB = vsGroups[(n + 1) / 2].PlayerB;
                            if (tokens[2] == "odd")
                            {
                                vsGroups[(n + 1) / 2].PlayerAReady = true;
                            }
                            else
                            {
                                vsGroups[(n + 1) / 2].PlayerBReady = true;
                            }
                            if (pA != null)
                            {    
                                SendMessageToClient(pA,
                                    "REDY|" + client.Name + "|" + "True" + "|" + tokens[2] + "|");
                            }
                            if (pB != null)
                            {
                                SendMessageToClient(pB,
                                    "REDY|" + client.Name + "|" + "True" + "|" + tokens[2] + "|");
                            }

                            if (vsGroups[(n + 1) / 2].CanStart() == true)
                            {
                                SendMessageToClient(pA, "STAR|");
                                SendMessageToClient(pB, "STAR|");
                            }
                            break;
                        }
                    case "URED":
                        {
                            int n = int.Parse(tokens[1]);
                            Client pA = vsGroups[(n + 1) / 2].PlayerA;
                            Client pB = vsGroups[(n + 1) / 2].PlayerB;
                            if (tokens[2] == "odd")
                            {
                                vsGroups[(n + 1) / 2].PlayerAReady = false;
                            }
                            else
                            {
                                vsGroups[(n + 1) / 2].PlayerBReady = false;
                            }
                            if (pA != null)
                            {
                                SendMessageToClient(pA,
                                    "URED|" + client.Name + "|" + "False" + "|" + tokens[2] + "|");
                            }
                            if (pB != null)
                            {
                                SendMessageToClient(pB,
                                    "URED|" + client.Name + "|" + "False" + "|" + tokens[2] + "|");
                            }
                            break;
                        }
                    case "MOVE" :
                        {                        
                            if (tokens[2] == "odd")
                            {
                                Client pl = vsGroups[(int.Parse(tokens[1]) + 1) / 2].PlayerB;
                                SendMessageToClient(pl, "MOVE|" + tokens[3] + "|" + 
                                    (11-int.Parse(tokens[4])).ToString() + "|" +
                                    (10 - int.Parse(tokens[5])).ToString() + "|");
                                SendMessageToClient(pl, "TURN|");
                            } 
                            else
                            {
                                Client pl = vsGroups[(int.Parse(tokens[1]) + 1) / 2].PlayerA;
                                SendMessageToClient(pl, "MOVE|" + tokens[3] + "|" +
                                    (11 - int.Parse(tokens[4])).ToString() + "|" +
                                    (10 - int.Parse(tokens[5])).ToString() + "|");
                                SendMessageToClient(pl, "TURN|");
                            }
                            break;
                        }
                    case "LOST" :
                        {
                            if (tokens[2] == "odd")
                            {
                                Client pl = vsGroups[(int.Parse(tokens[1]) + 1) / 2].PlayerB;
                                SendMessageToClient(pl, "LWIN|" + tokens[1] + "|0" + "|");
                            }
                            else
                            {
                                Client pl = vsGroups[(int.Parse(tokens[1]) + 1) / 2].PlayerA;
                                SendMessageToClient(pl, "LWIN|" + tokens[1] + "|0" + "|");
                            }
                            SetVSGroup(int.Parse(tokens[1]), 0, client);
                            break;
                        }
                }
            }
        }
    }
}
