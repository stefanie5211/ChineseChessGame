using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows;
using System.Collections;

namespace ChineseChess
{
    public class GameHallClient
    {
        private GameHall gameHall;
        private GameMain gameMain;
        private IPAddress serverIPAddress;
        private string playerName;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private Thread netThread;

        public GameHallClient(GameHall gameHall, GameMain gameMain)
        {
            this.gameHall = gameHall;
            this.gameMain = gameMain;
            serverIPAddress = gameHall.GameHallWindowInfo.MenuWindowInfo.ServerIPAddress;
            playerName = gameHall.GameHallWindowInfo.MenuWindowInfo.PlayName;
            tcpClient = new TcpClient();
            netThread = new Thread(new ThreadStart(RunNetwork));
            TcpClientConnect();
            RegisterWithServer();
        }

        public string PlayerName
        {
            get { return playerName; }
        }

        public void TcpClientConnect()
        {
            try
            {
                tcpClient.Connect(serverIPAddress, 8500);
                networkStream = tcpClient.GetStream();
                streamReader = new StreamReader(networkStream); 
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect", "Network Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }      
        }

        public void RegisterWithServer()
        {
            try
            {
                string message = "CONN|" + playerName;
                SendData(message);

                string serverResponse1 = streamReader.ReadLine();
                serverResponse1.Trim();
                string[] tokens1 = serverResponse1.Split('|'); 
                if (tokens1[0] == "LIST")
                {
                    gameHall.UpdateClientList(tokens1);
                }

                string serverResponse2 = streamReader.ReadLine();
                serverResponse2.Trim();
                string[] tokens2 = serverResponse2.Split('|');
                if (tokens2[0] == "SEAT")
                {
                    gameHall.UpdateSeatState(tokens2);
                }
            } 
            catch (Exception e) 
            { 
                MessageBox.Show("Registe Error！"+ e.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                System.Environment.Exit(0);
            } 
            
            netThread.Start();
        }

        public void RunNetwork()
        {
            try
            {
                while (true)
                {
                    Byte[] buffer = new Byte[1024];
                    networkStream.Read(buffer, 0, buffer.Length);
                    string chatter = System.Text.Encoding.ASCII.GetString(buffer);
                    string[] tokens = chatter.Split(new Char[] { '|' });

                    switch (tokens[0])
                    {
                        case "CHAT" : //用户聊天
                            {
                                gameHall.SendChatMessage(tokens[1]);
                                break;
                            }

                        case "SEAT" : //重置大厅用户列表信息List
                            {
                                gameHall.AddSeatState(tokens);
                                break;
                            }

                        case "JOIN" : //用户加入
                            {
                                gameHall.AddClientList(tokens);
                                gameHall.SendSystemMessage(tokens[1].Trim() + " has joined the Chat!\r\n");
                                break;
                            }

                        case "GONE" : //用户离开
                            {
                                gameHall.DeleteClientList(tokens);
                                gameHall.SendSystemMessage(tokens[1].Trim() + " has left the Chat!\r\n");
                                break;
                            }

                        case "PINF" :
                            {
                                gameMain.SetPlayerInfo(tokens);
                                break;
                            }
                        case "REDY" :
                            {
                                gameMain.SetPlayerInfo(tokens);
                                break;
                            }
                        case "URED" :
                            {
                                gameMain.SetPlayerInfo(tokens);
                                break;
                            }
                        case "STAR" :
                            {
                                gameMain.StartGame();
                                break;
                            }
                        case "PRIV" :
                            {
                                gameMain.SetChatText(tokens[1] + "\r\n");
                                break;
                            }
                        case "MOVE" :
                            {
                                gameMain.MovePiece(tokens);
                                break;
                            }
                        case "TURN" :
                            {
                                gameMain.SetMyTurn();
                                break;
                            }
                        case "LWIN":
                            {
                                gameHall.AddSeatState(tokens);
                                gameMain.WinForLost(tokens);
                                break;
                            }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Network interruption, please reconnect", "Network Information", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Environment.Exit(0);
            }
        }

        public void SendData(string data)
        {
            try
            {
                Byte[] outbytes = System.Text.Encoding.ASCII.GetBytes(data.ToCharArray());
                networkStream.Write(outbytes, 0, outbytes.Length);
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with you network, please check it", "Network Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
