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

        List<String> myShots = new List<String>();
        int correctShotIterator = 0;
        Boolean isEnd = false;
        public Game(WriterReader TCPInterface, Boolean isServer, List<String> myShips)
        {
            this.TCPObject = TCPInterface;
            this.isServer = isServer;
            this.myShips = myShips;
            Console.WriteLine("my ships");
            foreach (String s in myShips) {
                Console.Write(s + " ");
            }
            Console.WriteLine("*************");
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
                        var recky = PlayerGrid.FindName(cellName) as System.Windows.Shapes.Rectangle;
                        recky.Fill = Helper.getBrushColor("#3366ff");
                    }

                }
            }
        }
        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isEnd)
            {
                MessageBox.Show("Game is over!");
            }
            else
            {

                Rectangle senderObj = sender as System.Windows.Shapes.Rectangle;
                String cellName = senderObj.Name;
                if (this.myShots.Contains(cellName))
                {
                    MessageBox.Show("You have already shot in this place!");
                }
                else
                {
                    myShots.Add(cellName);
                    MyMessageBox.Visibility = Visibility.Hidden;


                    // send a shoot
                    String colName = cellName.Substring(0, 1);
                    String rowName = cellName.Substring(1, (cellName.Length - 1));

                    char colChar = colName.ToCharArray()[0];
                    int rowInt = Int32.Parse(rowName);
                    String shotMessage = colChar + "" + (rowInt - 1) + "";
                    TCPObject.sendMessage(shotMessage);

                    // wait for the response 
                    String response = TCPObject.getMessage();

                    if (response.StartsWith("True"))
                    {
                        senderObj.Fill = Helper.getBrushColor("#33cc33");
                        correctShotIterator++;
                        if (correctShotIterator == myShips.Count())
                        {
                            MessageBox.Show("YOU WON!");
                            isEnd = true;
                        }
                    }
                    else
                    {
                        senderObj.Fill = Helper.getBrushColor("#cc0000");
                    }

                    // wait for the shot
                    String oponentShot = TCPObject.getMessage();
                    //oponent shot col row-1

                    char colCharOponent = oponentShot.ToCharArray()[0];
                    int rowIntOponent = Int32.Parse(oponentShot.ToCharArray()[1] + "");

                    String oponentShotCell = "grid" + colCharOponent + (rowIntOponent + 1);
                    // send if it was correct
                    String message;

                    var recky = PlayerGrid.FindName(oponentShotCell) as System.Windows.Shapes.Rectangle;

                    if (this.myShips.Contains(oponentShotCell))
                    {
                        message = "True";
                        recky.Stroke = Helper.getBrushColor("#33cc33");


                    }
                    else
                    {
                        message = "False";
                        recky.Stroke = Helper.getBrushColor("#cc0000");
                    }
                    TCPObject.sendMessage(message);
                }
            }
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            startBtn.Visibility = Visibility.Hidden;
            MyMessageBox.Visibility = Visibility.Visible;
            
            if (isServer)
            {
                MyMessageBox.Text = "You start!";
            }
            else {
                MyMessageBox.Visibility = Visibility.Hidden;
                //get first shot
                String shotCellName = TCPObject.getMessage();
                //send if it was matched
                String message;

                String colName = shotCellName.Substring(0, 1);
                String rowName = shotCellName.Substring(1, (shotCellName.Length - 1));

                char colChar = colName.ToCharArray()[0];
                int rowInt = Int32.Parse(rowName);

                String shotName = "grid" + colChar + "" + (rowInt + 1) + "";
                var recky = PlayerGrid.FindName(shotName) as System.Windows.Shapes.Rectangle;
                if (this.myShips.Contains(shotName))
                {
                    recky.Stroke = Helper.getBrushColor("#33cc33");
                    message = "True";
                }
                else
                {
                    recky.Stroke = Helper.getBrushColor("#cc0000");
                    message = "False";
                }
                TCPObject.sendMessage(message);

            }

           

        }
        //helpers


    }
}
