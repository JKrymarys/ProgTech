using System;
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
using System.Windows.Shapes;
using BattleshipsOnline.Sources.TCPConnector;

namespace BattleshipsOnline
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private WriterReader TCPObject;
        private Boolean isServer;
        public Game(WriterReader TCPInterface, Boolean isServer)
        {
            this.TCPObject = TCPInterface;
            this.isServer = isServer;
            InitializeComponent();            
        }

        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle cell = sender as Rectangle;            
            cell.Fill = new SolidColorBrush(Colors.Blue);
        }

        private void shootBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Visibility = Visibility.Hidden;
            String text;
            if (isServer) {
                text = "#S:foo ";
            }
            else {
                text = "#C:foo ";
            }
            // send a shoot
            TCPObject.sendMessage(text);

            // vait for the response
            String message = TCPObject.getMessage();
            Console.WriteLine(message);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            startBtn.Visibility = Visibility.Hidden;
            MessageBox.Visibility = Visibility.Visible;
            
            if (isServer)
            {
                MessageBox.Text = "You start!";
            }
            else {
                MessageBox.Visibility = Visibility.Hidden;
                String message = TCPObject.getMessage();
                Console.WriteLine(message);
            }

           

        }

    }
}
