using System.Collections.Generic;

namespace Game.Data
{
    public class Hand
    {
        public readonly int Id;
        public readonly List<Tile> Tiles;

        public Hand(int id, List<Tile> tiles)
        {
            Id = id;
            Tiles = tiles;
        }
        
        ~Hand(){}
    }
}