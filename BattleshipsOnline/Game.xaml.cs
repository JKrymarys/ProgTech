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
using BattleshipsOnline.Sources;

namespace BattleshipsOnline
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private WriterReader TCPObject;
        private Boolean isServer;
        List<String> myShips = new List<String>();
        public Game(WriterReader TCPInterface, Boolean isServer, List<String> myShips)
        {
            this.TCPObject = TCPInterface;
            this.isServer = isServer;
            this.myShips = myShips;
            InitializeComponent();
            markMyShips();
            
        }
        private void markMyShips()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (char j = 'A'; j <= 'J'; j++)
                {

                    String cellName = "grid" + j + "" + i;
                    if (this.myShips.Contains(cellName))
                    {
                        Console.WriteLine(cellName);
                        var recky = PlayerGrid.FindName(cellName) as System.Windows.Shapes.Rectangle;
                        recky.Fill = Helper.getBrushColor("#3366ff");
                    }

                }
            }
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
        //helpers


    }
}
