namespace BattleshipsOnline.Sources
{
    public class Field
    {
        public Coords coordinatas { get; }
        public FieldType type { get; set; }

        public Field(Coords _coordinatas)
        {
            this.coordinatas = _coordinatas;
            this.type = FieldType.Empty;
        }

        public FieldType GetStatus()
        {
            return this.type;
        }

        public bool IsOccupiedByShip()
        {
            return this.type == FieldType.Battleship ||
                   this.type == FieldType.Carrier ||
                   this.type == FieldType.Cruiser ||
                   this.type == FieldType.Destroyer ||
                   this.type == FieldType.Submarine;
        }
        
        
    }
    
}