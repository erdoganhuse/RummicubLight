using System;
using Constants;

namespace Game.Data
{
    public class Tile
    {
        public readonly int Number;
        public readonly TileType Type;

        public Tile(){}

        public Tile(TileType type) : this()
        {
            Type = type;
        }
        
        public Tile(int number, TileType type) : this(type)
        {
            Number = number;
        }
        
        ~Tile(){}
    }
}