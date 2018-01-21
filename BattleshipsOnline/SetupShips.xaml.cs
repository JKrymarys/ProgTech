using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BattleshipsOnline.Sources.TCPConnector;

namespace BattleshipsOnline
{
    /// <summary>
    /// Interaction logic for SetupShips.xaml
    /// </summary>
    public partial class SetupShips : Window
    {
        WriterReader TCPObject;
        public SetupShips(WriterReader TCPInterface)
        {
            this.TCPObject = TCPInterface;
            InitializeComponent();
            
        }

        private void gridMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void orientationMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void ship_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Game GameWindow = new Game();
            GameWindow.Show();
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
