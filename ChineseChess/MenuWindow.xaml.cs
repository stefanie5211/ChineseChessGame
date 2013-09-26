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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace ChineseChess
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MenuWindow : Window
    {
        private IPAddress serverIPAddress = null;
        private string playerName;

        public MenuWindow()
        {
            InitializeComponent();
        }

        public IPAddress ServerIPAddress
        {
            get { return serverIPAddress; }
        }

        public string PlayName
        {
            get { return playerName; }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This game is developed by Shu Li, Yilong Wang", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serverIPAddress = IPAddress.Parse(IPTextBox.Text);
                if (nameTextBox.Text == "")
                {
                    MessageBox.Show("Please enter player name！", "Wrong Message");
                    return;
                }
                else
                {
                    playerName = nameTextBox.Text;
                }             
            }
            catch (Exception)
            {
                MessageBox.Show("IP address is error！", "Error Message");
                return;
            }

            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
