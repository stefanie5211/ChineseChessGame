using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace ChineseChess
{
    public class GameHall
    {
        delegate void SetTextDelegate(string str);
        delegate void SetDataGridDelegate(Player player);
        delegate void DataGridDelegate(string str);
        delegate void SeatDelegate(string[] strs, int i);

        private GameHallWindow gameHallWindow;
        private GameHallClient gameHallClient;
        private GameMain gameMain;
        private ObservableCollection<Player> players;
        private Dictionary<string, Player> playersDictionary;

        public GameHall(GameHallWindow gameHallWindow)
        {
            players = new ObservableCollection<Player>();
            playersDictionary = new Dictionary<string, Player>();
            this.gameHallWindow = gameHallWindow;
            gameMain = gameHallWindow.GameMainWindowInfo.GameMainInfo;
            gameHallClient = new GameHallClient(this, gameMain);
            gameHallWindow.clientDataGrid.DataContext = players;           
        }

        public GameHallWindow GameHallWindowInfo
        {
            get { return gameHallWindow; }
        }

        public GameHallClient GameHallClientInfo
        {
            get { return gameHallClient; }
        }

        public Dictionary<string, Player> PlayerDictionary
        {
            get { return playersDictionary; }
        }

        public void UpdateClientList(string[] tokens)
        {
            if(tokens[1] != "")
            {
                for(int i = 1; i < tokens.Length - 1; ++i)
                { 
                    Player play = new Player() 
                    { 
                        PlayerName = tokens[i].Trim(new char[] { '\r', '\n' }),
                        PlayerIPAddress = tokens[++i].Trim(new char[] { '\r', '\n' })
                    };
                    players.Add(play);
                    playersDictionary.Add(play.PlayerIPAddress, play);
                }
            }
        }

        public void UpdateSeatState(string[] tokens)
        {
            for (int i = 1; i < tokens.Length - 1; ++i)
            {
                if (tokens[i] == "1")
                {
                    gameHallWindow.TakenSeatStyle(gameHallWindow.SeatButtons[i]);
                }
            }
        }

        public void AddClientList(string[] tokens)
        {
            Player player = new Player()
            {
                PlayerName = tokens[1].Trim(new char[] { '\r', '\n' }),
                PlayerIPAddress = tokens[2].Trim(new char[] { '\r', '\n' })
            };
            playersDictionary.Add(player.PlayerIPAddress, player);
            gameHallWindow.clientDataGrid.Dispatcher.Invoke(new SetDataGridDelegate(DispatcherAddClientList), player);
        }

        public void AddSeatState(string[] tokens)
        {
            int i = int.Parse(tokens[1]);
            if (tokens[2] == "1")
            {
                gameHallWindow.SeatButtons[i].Dispatcher.Invoke(new SeatDelegate(DelegateSeatStateTaken), tokens, i);             
            }
            else
            {
                gameHallWindow.SeatButtons[i].Dispatcher.Invoke(new SeatDelegate(DelegateSeatStateNotTaken), tokens, i);
            }          
        }

        public void DelegateSeatStateTaken(string[] tokens, int i)
        {
            gameHallWindow.TakenSeatStyle(gameHallWindow.SeatButtons[i]);
        }

        public void DelegateSeatStateNotTaken(string[] tokens, int i)
        {
            gameHallWindow.NotTakenSeatStyle(gameHallWindow.SeatButtons[i]);
        }


        public void DeleteClientList(string[] tokens)
        {
            gameHallWindow.clientDataGrid.Dispatcher.Invoke(new DataGridDelegate(DispatcherDeleteClientList), tokens[2]);
        }

        public void DispatcherAddClientList(Player player)
        {
            players.Add(player);
        }

        public void DispatcherDeleteClientList(string str)
        {
            players.Remove(playersDictionary[str]);
        }

        public void SendChatMessage(string str)
        {
            gameHallWindow.chatHallShowTextBox.Dispatcher.Invoke(
               DispatcherPriority.Normal,
               new SetTextDelegate(gameHallWindow.UpdateChatHallShowTexBox),
               str);
        }

        public void SendSystemMessage(string str)
        {
            string message = "System:";
            message += str;
           
            gameHallWindow.chatHallShowTextBox.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                new SetTextDelegate(gameHallWindow.UpdateChatHallShowTexBox),
                message);
        }
    }
}
