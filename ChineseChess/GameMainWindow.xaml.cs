using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.ComponentModel;

namespace ChineseChess
{
    /// <summary>
    /// GameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameMainWindow : Window
    {
        private GameMain gameMain;
        private GameHallWindow gameHallWindow;
        private ChessPiece chessPiece;
        private ChessBoard chessBoard;
        private Button selectButton;
        private bool myTurn;
        private bool closeForWin;

        public GameMainWindow(GameHallWindow gameHallWindow)
        {
            InitializeComponent();
            chessPiece = new ChessPiece(this);
            chessBoard = new ChessBoard(this, chessPiece);
            chessPiece.InitializeRedPieces();
            chessPiece.InitializeBlackPieces();
            chessBoard.InitializeButtonToGrid();
            chessBoard.InitializeGrids();
            Button button = chessPiece.BlackPieceButtons["blackGuardButtonLeft"];
            this.gameHallWindow = gameHallWindow;
            gameMain = new GameMain(this, gameHallWindow);
            selectButton = null;
            closeForWin = false;
        }

        public bool CloseForWin
        {
            get { return closeForWin; }
            set { closeForWin = value; }
        }

        public bool MyTurn
        {
            get { return myTurn; }
            set { myTurn = value; }
        }

        public GameMain GameMainInfo
        {
            get { return gameMain; }
        }

        public ChessPiece ChessPieceInfo
        {
            get { return chessPiece; }
        }

        public Button SelectButton
        {
            get { return selectButton; }
        }

        public ChessBoard ChessBoardInfo
        {
            get { return chessBoard; }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            gameHallWindow.NotTakenSeatStyle(gameHallWindow.MySeatButton);           
            if (readyButton.Visibility == Visibility.Visible || closeForWin == true)
            {
                gameHallWindow.GameHallInfo.GameHallClientInfo.SendData("SEAT|" + gameHallWindow.SeatNumber.ToString() + "|0");  
            }
            else
            {
                gameHallWindow.GameHallInfo.GameHallClientInfo.SendData("LOST|" + gameHallWindow.SeatNumber.ToString() + "|" + gameHallWindow.OddOrEven);
                gameHallWindow.NotTakenSeatStyle(gameHallWindow.MySeatButton);
            }

            gameHallWindow.ShowGameMainWindow = false;
            gameHallWindow.SeatNumber = 0;
            GameMainWindow gmw = new GameMainWindow(gameHallWindow);
            gameHallWindow.GameMainWindowInfo = gmw;
            gameMain.GameMainWindowInfo = gmw;
        }

        private void readyButton_Click(object sender, RoutedEventArgs e)
        {
            unReadyButton.IsEnabled = true;
            readyButton.IsEnabled = false;

            gameHallWindow.GameHallInfo.GameHallClientInfo.SendData("REDY|" + gameHallWindow.SeatNumber.ToString() + "|" + gameHallWindow.OddOrEven);
        }

        private void unReadybutton_Click(object sender, RoutedEventArgs e)
        {
            unReadyButton.IsEnabled = false;
            readyButton.IsEnabled = true;

            gameHallWindow.GameHallInfo.GameHallClientInfo.SendData("URED|" + gameHallWindow.SeatNumber.ToString() + "|" + gameHallWindow.OddOrEven);
        }

        private void sendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sendTextBox.Text != "")
            {
                gameHallWindow.GameHallInfo.GameHallClientInfo.SendData("PRIV|" + gameHallWindow.GameHallInfo.GameHallClientInfo.PlayerName + ": " + sendTextBox.Text + "|" + gameHallWindow.SeatNumber.ToString() +"\r\n");
                sendTextBox.Text = "";
            }
        }

        private void wrapPanelGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            if (grid.Children.Count == 0 && selectButton != null && myTurn == true)
            {
                (chessBoard.ButtonToGrid[selectButton]).Children.RemoveAt(0);
                chessBoard.ButtonToGrid[selectButton] = grid;
                grid.Children.Add(selectButton);
                if (grid.Name.Length == 16)
                {
                        gameHallWindow.GameHallInfo.GameHallClientInfo.SendData(
                    "MOVE|" + gameHallWindow.SeatNumber.ToString() + "|" +
                    gameHallWindow.OddOrEven + "|" + 
                    "black" + selectButton.Name.Substring(3) + "|" +
                    grid.Name[13] + "|" + grid.Name[15]);
                } 
                else
                {
                    gameHallWindow.GameHallInfo.GameHallClientInfo.SendData(
                    "MOVE|" + gameHallWindow.SeatNumber.ToString() + "|" +
                    gameHallWindow.OddOrEven + "|" + 
                    "black" + selectButton.Name.Substring(3) + "|" +
                    grid.Name.Substring(13, 2) + "|" + grid.Name[16]);
                }
                if (gameHallWindow.OddOrEven == "odd")
                {
                    turnTextBlock.Text = player2TextBlock.Text;
                }
                else
                {
                    turnTextBlock.Text = player1TextBlock.Text;
                }
                myTurn = false;
            }
        }

        private void blackRookButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            selectButton = (Button)sender;
        }

        private void grid3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectButton != null)
            {
                sendTextBox.Focus();
            }
        }
    }
}
