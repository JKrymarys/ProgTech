using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BattleshipsOnline.Sources;
using BattleshipsOnline.Sources.TCPConnector;
using System.Windows.Shapes;
using System.Drawing;




namespace BattleshipsOnline
{
    /// <summary>
    /// Interaction logic for SetupShips.xaml
    /// </summary>
    public partial class SetupShips : Window
    {
        WriterReader TCPObject;
        private Boolean isServer;
        public ObservableCollection<Field> fields = new ObservableCollection<Field>();
        string checkedShipName = null;
        HashSet <String> placedShips = new HashSet <String>();
        Boolean isHorizontal = true;
        List<String> takenCells = new List<String>();

        public SetupShips(WriterReader TCPInterface, Boolean isServer)
        {
            this.TCPObject = TCPInterface;
            this.isServer = isServer;
            InitializeComponent();
            
        }

        private void gridMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.checkedShipName == null)
            {
                MessageBox.Show("Choose a ship to be placed first!");
            } else {
                int shipSize = Helper.getShipSize(this.checkedShipName);
                var shipBrush = getBrushColor(Helper.getShipColor(this.checkedShipName));
                var senderObj = sender as System.Windows.Shapes.Rectangle;
                String cellName = senderObj.Name;
                String colName = cellName.Substring(4,1);
                String rowName = cellName.Substring(5, (cellName.Length - 5));

                char colChar = colName.ToCharArray()[0];
                int rowInt = Int32.Parse(rowName);

                Boolean isLimitValid = checkIfOutsideLimits(colChar, rowInt, shipSize, isHorizontal);
                if (!isLimitValid)
                {
                    MessageBox.Show("Ship has to fit in a grid!");
                }
                else if (placedShips.Contains(this.checkedShipName)) {
                    MessageBox.Show("This ship is already placed!");
                }
                else
                {
                    //check if does not overlap with any other ship 
                    Boolean isCorrectlyPlaced = true;
                    if (isHorizontal)
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            int incrementedRow = rowInt + i;
                            String tmpCellName = "grid" + colChar + incrementedRow;
                            if (this.takenCells.Contains(tmpCellName))
                            {
                                isCorrectlyPlaced = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        char incrementedCol = colChar;
                        for (int i = 0; i < shipSize; i++)
                        {
                            String tmpCellName = "grid" + incrementedCol + rowInt;
                            if (this.takenCells.Contains(tmpCellName))
                            {
                                isCorrectlyPlaced = false;
                                break;
                            }
                            incrementedCol = incrementCharacter(incrementedCol);
                           
                        }
                    }
                    if (!isCorrectlyPlaced)
                    {
                        MessageBox.Show("Ships cannot overlap!");
                    }
                    else
                    {
                        // place ships
                        placedShips.Add(this.checkedShipName);

                        if (isHorizontal)
                        {
                            for (int i = 0; i < shipSize; i++)
                            {
                                int incrementedRow = rowInt + i;
                                changeRectangleColor(colChar, incrementedRow);
                                String tmpCellName = "grid" + colChar + incrementedRow;
                                this.takenCells.Add(tmpCellName);
                            }
                        }
                        else
                        {
                            char incrementedCol = colChar;
                            for (int i = 0; i < shipSize; i++)
                            {
                                changeRectangleColor(incrementedCol, rowInt);
                                String tmpCellName = "grid" + incrementedCol + rowInt;
                                this.takenCells.Add(tmpCellName);
                                incrementedCol = incrementCharacter(incrementedCol);
                            }
                        }
                    
                    
                    }
                    
                }
                
            }
        }
        private void changeRectangleColor(char colName, int rowName)
        {
            String cellName = "grid" + colName + rowName;
            var recky = shipyardGrid.FindName(cellName) as System.Windows.Shapes.Rectangle;
            recky.Fill = getBrushColor(Helper.getShipColor(this.checkedShipName));
        }
        private void resetGrid()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (char j = 'A'; j <= 'J'; j++)
                {
                    
                    String cellName = "grid" + j + "" + i;
                    Console.WriteLine(cellName);
                    var recky = shipyardGrid.FindName(cellName) as System.Windows.Shapes.Rectangle;
                    recky.Fill = getBrushColor("#FFFFFF");
                }
            }
        }
        private void orientationMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void ship_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            var senderObj = sender as Path;

            String shipType = Helper.findObjectName(sender, this);
            this.checkedShipName = shipType;

            String colorToChange = Helper.getShipColor(shipType);
            var shipBrush = getBrushColor(colorToChange);
            changeAllShipsToDefault();
            senderObj.Fill = shipBrush;

            

        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (this.placedShips.Count < 5)
            {
                MessageBox.Show("Place all ships first!");
            }
            else
            {
                Game GameWindow = new Game(TCPObject, isServer);
                GameWindow.Show();
                this.Close();
            }

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.placedShips.Clear();
            resetGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DirectionButtonHorizontal_Checked(object sender, RoutedEventArgs e)
        {
            this.isHorizontal = true;
        }

        private void DirectionButtonVerical_Checked(object sender, RoutedEventArgs e)
        {
            this.isHorizontal = false;
        }


        // helpers

        private Boolean checkIfOutsideLimits(Char colChar, int rowNum, int size, Boolean isHorizontal)
        {
            if (isHorizontal)
            {
                return checkIfOutsideLimitsRow(rowNum, size);
            }
            else
            {
                return checkIfOutsideLimitsCol(colChar, size);
            }


        }
        private Boolean checkIfOutsideLimitsCol(char colChar, int size)
        {
            size = size - 1;
            // colNum A - J 
            char incrementedCharacter = colChar;
            for (int i = 0; i < size; i++)
            {
                incrementedCharacter = incrementCharacter(incrementedCharacter);
            }
            Console.Write(incrementedCharacter);
            if (incrementedCharacter.CompareTo('J') > 0) return false;
            return true;
        }
        private Boolean checkIfOutsideLimitsRow(int rowInt, int size)
        {
            size = size - 1;
            // row num 1 - 10
            int incremenetedInt = rowInt + size;
            Console.Write(incremenetedInt);
            if (incremenetedInt > 10) return false;
            return true;
        }
        private char incrementCharacter(char input)
        {
            return (input == 'z' ? 'a' : (char)(input + 1));
        }
        private void changeAllShipsToDefault()
        {
            String defaultColor = Helper.DEFAULT_COLOR;
            var defaultBrush = getBrushColor(defaultColor);
            destroyer.Fill = defaultBrush;
            cruiser.Fill = defaultBrush;
            submarine.Fill = defaultBrush;
            battleship.Fill = defaultBrush;
            carrier.Fill = defaultBrush;

        }
        private System.Windows.Media.Brush getBrushColor(String colorName)
        {
            var converter = new System.Windows.Media.BrushConverter();
            return (System.Windows.Media.Brush)converter.ConvertFromString(colorName);
        }
    }
}
