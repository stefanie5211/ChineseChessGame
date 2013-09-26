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

namespace ChineseChess
{
    /// <summary>
    /// GameHallWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GameHallWindow : Window
    {
        private MenuWindow menuWindow;
        private GameMainWindow gameMainWindow;
        private GameHall gameHall;
        private Button mySeatButton;
        private List<Button> seatButtons;
        private GameMain gameMain;
        private bool showGameMainWindow;
        private int seatNumber;
        private string oddOrEven;

        public GameHallWindow()
        {
            InitializeComponent();
            InitializeSeatButtons();
            menuWindow = new MenuWindow();           
            menuWindow.ShowDialog();
            gameMainWindow = new GameMainWindow(this);
            gameHall = new GameHall(this);

            showGameMainWindow = false;
            seatNumber = 0;
        }

        public bool ShowGameMainWindow
        {
            get { return showGameMainWindow; }
            set { showGameMainWindow = value; }
        }

        public List<Button> SeatButtons
        {
            get { return seatButtons; }
        }

        public GameHall GameHallInfo
        {
            get { return gameHall; }
        }

        public MenuWindow MenuWindowInfo
        {
            get { return menuWindow; }
        }

        public GameMainWindow GameMainWindowInfo
        {
            get { return gameMainWindow; }
            set { gameMainWindow = value; }
        }

        public Button MySeatButton
        {
            get { return mySeatButton; }
        }

        public string OddOrEven
        {
            get { return oddOrEven; }
        }

        public int SeatNumber
        {
            get { return seatNumber; }
            set { seatNumber = value; }
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            Button seatButton = (Button)sender;
            if ((((string)(seatButton.Content)) == "Null") && (showGameMainWindow == false))
            {
                mySeatButton = seatButton;
                TakenSeatStyle(mySeatButton);
                string str = seatButton.Name.Substring(10);
                seatNumber = int.Parse(str);
                gameHall.GameHallClientInfo.SendData("SEAT|" + seatNumber + "|1|" + " |" + " |" + "");
                if (seatNumber % 2 == 1)
                {
                    oddOrEven = "odd";
                    gameMainWindow.MyTurn = true;
                    gameMainWindow.player1TextBlock.Text = gameHall.GameHallClientInfo.PlayerName;
                    gameMainWindow.player1StateTextBlock.Text = "Connected";
                }
                else
                {
                    oddOrEven = "even"  ;
                    gameMainWindow.MyTurn = false;
                    gameMainWindow.player2TextBlock.Text = gameHall.GameHallClientInfo.PlayerName;
                    gameMainWindow.player2StateTextBlock.Text = "Connected";
                }
                gameMainWindow.Show();
                showGameMainWindow = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gameHall.GameHallClientInfo.SendData("GONE|" + gameHall.GameHallClientInfo.PlayerName);
            System.Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void hallSendButton_Click(object sender, RoutedEventArgs e)
        {
            if (chatHallSendTextBox.Text != "")
            {
                gameHall.GameHallClientInfo.SendData("CHAT|" + gameHall.GameHallClientInfo.PlayerName + ": " + chatHallSendTextBox.Text + "\r\n");
                chatHallSendTextBox.Text = "";
            }
        }

        public void UpdateChatHallShowTexBox(string str)
        {
            chatHallShowTextBox.Text += str;
        }

        public void InitializeSeatButtons()
        {
            seatButtons = new List<Button>();

            seatButtons.Add(null);

            seatButtons.Add(seatButton1);
            seatButtons.Add(seatButton2);

            seatButtons.Add(seatButton3);
            seatButtons.Add(seatButton4);

            seatButtons.Add(seatButton5);
            seatButtons.Add(seatButton6);

            seatButtons.Add(seatButton7);
            seatButtons.Add(seatButton8);
        }

        public void TakenSeatStyle(Button seatButton)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0.5, 0);
            linearGradientBrush.EndPoint = new Point(0.5, 1);
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Coral, 0));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Brown, 1));
            seatButton.Background = linearGradientBrush;
            seatButton.Content = "Full";
        }

        public void NotTakenSeatStyle(Button seatButton)
        {   
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0.5, 0);
            linearGradientBrush.EndPoint = new Point(0.5, 1);
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.PaleTurquoise, 0));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Teal, 1));
            seatButton.Background = linearGradientBrush;
            seatButton.Content = "Null";
        }
    }
}
