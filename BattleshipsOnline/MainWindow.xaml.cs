﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BattleshipsOnline.Sources.TCPConnector;

namespace BattleshipsOnline
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid grid = new Grid();
        MyServer server;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartHosting_Click(object sender, RoutedEventArgs e)
        {
            this.server = new MyServer();
            HostIP.Text = server.myIPAddress.AddressList[1].ToString();
        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            String name = PlayerName.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }
            Boolean? isClient = clientBtn.IsChecked;
            if (isClient.Value)
            {
                String IPAddress = OpponentIP.Text;
                MyClient client = new MyClient(IPAddress);
                SetupShips setupShipsWindow = new SetupShips(client);
                setupShipsWindow.Show();
                this.Close();
            }
            else
            {
               if (server == null || server.tcpClient == null)
                {
                    MessageBox.Show("Wait for a client to Join");
                    return;
                }
                else
                {
                    SetupShips setupShipsWindow = new SetupShips(server);
                    setupShipsWindow.Show();
                    this.Close();
                }
            }

        }

        private void JoinGame_Checked(object sender, RoutedEventArgs e)
        {
            IPPanel.Visibility = Visibility.Visible;
            MyIPPanel.Visibility = Visibility.Hidden;
            hostBtnStart.Visibility = Visibility.Hidden;
        }
        private void HostGame_Checked(object sender, RoutedEventArgs e)
        {
            IPPanel.Visibility = Visibility.Hidden;
            MyIPPanel.Visibility = Visibility.Visible;
            hostBtnStart.Visibility = Visibility.Visible;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // put the rules of the game here
            MessageBox.Show("Lorem Ipsum");
        }
    }
}
