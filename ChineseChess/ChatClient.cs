using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChineseChess
{
    class ChatClient
    {
        private GameHallWindow gameHallWindow;
        private UdpClient udpClient;
        private IPEndPoint remotePoint;
        private Thread netThread;

        public ChatClient(GameHallWindow gameHallWindow)
        {
            this.gameHallWindow = gameHallWindow;
            netThread = new Thread(new ThreadStart(WaitForPackets));
            udpClient = new UdpClient(4445);
            remotePoint = new IPEndPoint(gameHallWindow.MenuWindowInfo.ServerIPAddress, 4444);
            netThread.Start();
        }

        private void WaitForPackets()
        {
            while (true)
            {
                byte[] data = udpClient.Receive(ref remotePoint);
                gameHallWindow.GameMainWindowInfo.showTextBox.Text += System.Text.Encoding.UTF8.GetString(data) + "\r\n";
            }
        }

        private void SendData(string data)
        {
            byte[] sendData = System.Text.Encoding.UTF8.GetBytes(data);
            udpClient.Send(sendData, sendData.Length, remotePoint);
        }
    }
}
