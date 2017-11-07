using System.Runtime.InteropServices;

namespace BattleshipsOnline.Sources
{
    public class Coords
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coords(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
        
    }
}