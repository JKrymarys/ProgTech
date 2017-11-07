using System;

namespace BattleshipsOnline.Sources
{
    public abstract class Ship
    {
//        private String name; //do we need a name of ship? For GUI eg? 
        
        private int length;
        private int hitsTaken;
        private int IsSunk;
        public FieldType FieldType { get; set; }
        
        public int Length
        {
            get { return length; }
            set {
                if(length == 0) //it is possible to set Length only once, when its 0 (default value for int)
                    length= value; 
            }
        }

//        public String Name
//        {
//            get
//            {
//                return name;
//            }
//        }


        public bool isSunk
        {
            get
            {
                return hitsTaken >= length;
            }
           
        }

        public void Hit()
        {
            hitsTaken++;
        }
       
    }
}