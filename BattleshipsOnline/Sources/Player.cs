using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipsOnline.Sources
{
    public class Player
    {
        private string name;
        private static FiringBoard firingBoard;
        private static Board ownBoard;
        private List<Ship> ships;


        public string Name
        {
            get { return name; }
        }


        public bool HasLost()
        {
            return ships.All(x => x.isSunk); //check if all of player's ships are gone
        }

        public Player(string name)
        {
            this.name = name;

            //inizialize all variables
            ownBoard = new Board();
            firingBoard = new FiringBoard();

            ships = new List<Ship>()
            {
                new Destroyer(),
                new Submarine(),
                new Patrol(),
                new Battleship(),
                new Carrier()
            };
        }


        public void PlaceShips()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode()); // to improve randomness of choice
            foreach (var ship in ships)
            {
       

                bool isOpen = true;
                while (isOpen)
                {
                
                    var startcolumn = rand.Next(1, 11); //second value is exclusive, whilst first is included
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2; //0 for Horizontal
                        
                    List<int> panelNumbers = new List<int>();
                   
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Length; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Length; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //We cannot place ships beyond the boundaries of the board
                    if (endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue; //Restart the while loop to select a new random panel
                    }

                    //Check if specified panels are occupied
                    var fieldsNearby = ownBoard.GetNearFields(startrow, startcolumn, endrow, endcolumn);
                    if (fieldsNearby.Any(x => x.IsOccupiedByShip()))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var field in fieldsNearby)
                    {
                        field.type = ship.FieldType;
                    }
                    isOpen = false;
                }
            }
        }


        public Coords FireShot()
        {
            //If there are hits on the board with neighbors which don't have shots, we should fire at those first.
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            Coords coords;
            if (hitNeighbors.Any())
            {
                coords = SearchingShot();
            }
            else
            {
                coords = RandomShot();
            }
            Console.WriteLine(Name + " says: \"Firing shot at " + coords.y.ToString() + ", " + coords.x.ToString() + "\"");
            return coords;
        }

        private Coords RandomShot()
        {
            return null;
        }

        private Coords SearchingShot()
        {
            return null;
        }
        
        
        
        
        // for debbugging
        //TODO doing this method I've changed some methods to static ones, reverse to normal at some point please
        public void OutputBoards()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Own Board:                          Firing Board:");
            for(int row = 1; row <= 10; row++)
            {
                for(int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(ownBoard.getFieldAt(row, ownColumn).GetStatus() + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(firingBoard.getFieldAt(row, firingColumn).GetStatus() + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

    }
}