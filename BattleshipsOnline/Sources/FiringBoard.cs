using System.Collections.Generic;
using System.Linq;

namespace BattleshipsOnline.Sources
{
    public class FiringBoard : Board
    {
        
        //TODO implement all these methods
        public List<Coords> GetOpenRandomPanels()
        {
            return null;
        }
        
        

        public List<Field> GetHitNeighbors()
        {
            List<Field> toReturn = new List<Field>();
            var hits = Fields.Where(x => x.GetStatus() == FieldType.Hit);
            foreach(var hit in hits)
            {
                Fields.AddRange(GetNeighbors(hit.coordinatas).ToList());
            }
            return toReturn.Distinct().Where(x => x.GetStatus() == FieldType.Empty).ToList();
   
        }

        public List<Field> GetNeighbors(Coords coordinates)
        {
            int row = coordinates.y;
            int column = coordinates.x;
            List<Field> panels = new List<Field>();
            if (column > 1)
            {
                panels.Add(this.getFieldAt(row, column - 1));
            }
            if (row > 1)
            {
                panels.Add(this.getFieldAt(row - 1, column));
            }
            if (row < 10)
            {
                panels.Add(this.getFieldAt(row + 1, column));
            }
            if (column < 10)
            {
                panels.Add(this.getFieldAt(row, column + 1));
            }
            return panels;
        }
    }
}