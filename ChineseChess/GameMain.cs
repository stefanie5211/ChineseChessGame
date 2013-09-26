using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChineseChess
{
    public class GameMain
    {
        delegate void SetPlayerInfoDelegate(TextBlock textBlock, string str);
        delegate void StartGameDelegate();
        delegate void ShowPiecesDelegate();
        delegate void SetChatTextDelegate(string str);
        delegate void MovePieceDelegate(string[] tokens);
        delegate void SetMyTurnDelegate();
        delegate void WinForLostDelegate();

        private GameMainWindow gameMainWindow;
        private GameHallWindow gameHallWindow;

        public GameMain(GameMainWindow gameMainWindow, GameHallWindow gameHallWindow)
        {
            this.gameMainWindow = gameMainWindow;
            this.gameHallWindow = gameHallWindow;
        }

        public GameMainWindow GameMainWindowInfo
        {
            get { return gameMainWindow; }
            set { gameMainWindow = value; }
        }

        public GameHallWindow GameHallWindowInfo
        {
            get { return gameHallWindow; }
        }

        public void StartGame()
        {
            Application.Current.Dispatcher.Invoke(new StartGameDelegate(DelegateStartGame));
            SetPlayerTextBlockInfo(gameMainWindow.player1StateTextBlock, "游戏进行中");
            SetPlayerTextBlockInfo(gameMainWindow.player2StateTextBlock, "游戏进行中");
            ShowPieces();
        }

        public void DelegateStartGame()
        {
            gameMainWindow.readyButton.Visibility = Visibility.Hidden;
            gameMainWindow.unReadyButton.Visibility = Visibility.Hidden;
            gameMainWindow.turnNameTextBlock.Visibility = Visibility.Visible;
            gameMainWindow.turnTextBlock.Visibility = Visibility.Visible;
        }

        public void ShowPieces()
        {
            Application.Current.Dispatcher.Invoke(new ShowPiecesDelegate(DelegateShowPieces));
        }

        public void DelegateShowPieces()
        {
            foreach(Button button in gameMainWindow.ChessPieceInfo.RedPieceButtons.Values)
            {
                button.Visibility = Visibility.Visible;
            }

            foreach(Button button in gameMainWindow.ChessPieceInfo.BlackPieceButtons.Values)
            {
                button.Visibility = Visibility.Visible;
            }
        }

        public void SetPlayerInfo(string [] tokens)
        {
            if (tokens[3] == "odd")
            {
                SetPlayerTextBlockInfo(gameMainWindow.player1TextBlock, tokens[1]);
                switch (tokens[2])
                {
                    case "True":
                        {                           
                            SetPlayerTextBlockInfo(gameMainWindow.player1StateTextBlock, "Ready");
                            break;
                        }
                    case "False":
                        {
                            SetPlayerTextBlockInfo(gameMainWindow.player1StateTextBlock, "Connected");
                            break;
                        }
                    case "leave":
                        {
                            SetPlayerTextBlockInfo(gameMainWindow.player1StateTextBlock, "Wait for connection");
                            break;
                        }
                }
            } 
            else
            {
                SetPlayerTextBlockInfo(gameMainWindow.player2TextBlock, tokens[1]);
                switch (tokens[2])
                {
                    case "True":
                        {
                            SetPlayerTextBlockInfo(gameMainWindow.player2StateTextBlock, "Ready");
                            break;
                        }
                    case "False":
                        {
                            SetPlayerTextBlockInfo(gameMainWindow.player2StateTextBlock, "Connected");
                            break;
                        }
                    case "leave":
                        {
                            SetPlayerTextBlockInfo(gameMainWindow.player2StateTextBlock, "Wait for connection");
                            break;
                        }
                }
            }

        }

        public void SetPlayerTextBlockInfo(TextBlock textBlock, string str)
        {
            textBlock.Dispatcher.Invoke(new SetPlayerInfoDelegate(DelegateSetPlayerInfo),textBlock, str);
        }

        public void DelegateSetPlayerInfo(TextBlock textBlock, string str)
        {
            textBlock.Text = str;
        }

        public void SetChatText(string str)
        {
            gameMainWindow.showTextBox.Dispatcher.Invoke(new SetChatTextDelegate(DelegateSetChatText), str);
        }

        public void DelegateSetChatText(string str)
        {
            gameMainWindow.showTextBox.Text += str;
        }

        public void MovePiece(string [] tokens)
        {
            
            //buttont = gameMainWindow.ChessPieceInfo.BlackPieceButtons[tokens[1]]; 
            object[] obj = new object[] { tokens };
            Application.Current.Dispatcher.Invoke(new MovePieceDelegate(DelegateMovePiece), obj);

        }

        public void DelegateMovePiece(string[] tokens)
        {
            int x = int.Parse(tokens[2]);
            int y = int.Parse(tokens[3]);
            Button button = gameMainWindow.ChessPieceInfo.BlackPieceButtons[tokens[1]];
            Grid grid = gameMainWindow.ChessBoardInfo.GridS[x, y];

            (((gameMainWindow.ChessBoardInfo.ButtonToGrid)[button]).Children).RemoveAt(0);
            (gameMainWindow.ChessBoardInfo.ButtonToGrid)[button] = grid;
            grid.Children.Add(button);
        }

        public void SetMyTurn()
        {
            Application.Current.Dispatcher.Invoke(new SetMyTurnDelegate(DelegateSetMyTurn));
        }

        public void DelegateSetMyTurn()
        {
            gameMainWindow.MyTurn = true;
            if (gameHallWindow.OddOrEven == "odd")
            {
                gameMainWindow.turnTextBlock.Text = gameMainWindow.player1TextBlock.Text;
            }
            else
            {
                gameMainWindow.turnTextBlock.Text = gameMainWindow.player2TextBlock.Text;
            }
        }

        public void WinForLost(string [] tokens)
        {
            Application.Current.Dispatcher.Invoke(new WinForLostDelegate(DelegateWinForLost));
        }

        public void DelegateWinForLost()
        {
            MessageBox.Show("Opponent Run！","You win!",MessageBoxButton.OK, MessageBoxImage.Asterisk);
            gameMainWindow.CloseForWin = true;
            gameMainWindow.Close();
        }
    }
}
