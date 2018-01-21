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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleshipsOnline
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid grid = new Grid();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            String name = PlayerName.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }
            SetupShips setupShipsWindow = new SetupShips();
            setupShipsWindow.Show();
            this.Close();
        }

        private void JoinGame_Checked(object sender, RoutedEventArgs e)
        {
            IPPanel.Visibility = Visibility.Visible;
        }
        private void HostGame_Checked(object sender, RoutedEventArgs e)
        {
            IPPanel.Visibility = Visibility.Hidden;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            // put the rules of the game here
            MessageBox.Show("Lorem Ipsum");
        }
    }
}
