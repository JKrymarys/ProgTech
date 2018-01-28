using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BattleshipsOnline.Sources;

namespace BattleshipsOnline.Sources
{
    class Helper
    {
        public static String DESTROYER_COLOR = "#0000cc";
        public static String CRUISER_COLOR = "#339933";
        public static String SUBMARINE_COLOR = "#9933ff";
        public static String BATTLESHIP_COLOR = "#663300";
        public static String CARRIER_COLOR = "#ff0000";
        public static String DEFAULT_COLOR = "#FFA2A2F1";

        public static string findObjectName(object obj, object csObject)
        {
            List<FieldInfo> localFields = new List<FieldInfo>(csObject.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
            return localFields.Find(info => obj == info.GetValue(csObject)).Name;
        }
        public static string getShipColor(String shipName)
        {
            switch (shipName)
            {
                case "destroyer" : return DESTROYER_COLOR;
                case "cruiser": return CRUISER_COLOR;
                case "submarine": return SUBMARINE_COLOR;
                case "battleship": return BATTLESHIP_COLOR;
                case "carrier": return CARRIER_COLOR;           
            }
            return DEFAULT_COLOR;
        }
        public static int getShipSize(String shipName)
        {
            switch (shipName)
            {
                case "destroyer": return Destroyer.getLength();
                case "cruiser": return Patrol.getLength();
                case "submarine": return Submarine.getLength();
                case "battleship": return Battleship.getLength();
                case "carrier": return Carrier.getLength();
            }
            return 0;
        }
        public static System.Windows.Media.Brush getBrushColor(String colorName)
        {
            var converter = new System.Windows.Media.BrushConverter();
            return (System.Windows.Media.Brush)converter.ConvertFromString(colorName);
        }

    }
}
