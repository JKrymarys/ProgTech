using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BattleshipsOnline.Sources
{
    
    
    public class Board
    {
        private List<Field> fields;
        protected List<Field> Fields { get; set; }
            
        public Board()
        {
            for (int x = 1; x < 11; x++)
            {
                for (int y = 1; y < 11; y++)
                {
                    fields.Add(new Field( new Coords(x,y)));
                }
            }
        }

        public Field getFieldAt(int x, int y)
        {
            return Fields.ElementAt(y * 10 + x + 1);
        }

        public List<Field> GetNearFields(int startRow, int startColumn, int endRow, int endColumn)
        {
            
            //TODO: check if y and x are assigned properly
            return fields.Where(f => f.coordinatas.y >= startRow 
                                     && f.coordinatas.y >= startColumn 
                                     && f.coordinatas.x <= endRow 
                                     && f.coordinatas.y <= endColumn).ToList();
        }
    }
}