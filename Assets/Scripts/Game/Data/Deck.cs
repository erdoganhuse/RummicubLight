using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace Game.Data
{
    public static class Deck
    {        
        public const int  NumberCountInEachType= 13;
        
        public static readonly List<Tile> Tiles = new List<Tile>()
        {
            {new Tile(1, TileType.Yellow)},
            {new Tile(2, TileType.Yellow)},
            {new Tile(3, TileType.Yellow)},
            {new Tile(4, TileType.Yellow)},
            {new Tile(5, TileType.Yellow)},
            {new Tile(6, TileType.Yellow)},
            {new Tile(7, TileType.Yellow)},
            {new Tile(8, TileType.Yellow)},
            {new Tile(9, TileType.Yellow)},
            {new Tile(10, TileType.Yellow)},
            {new Tile(11, TileType.Yellow)},
            {new Tile(12, TileType.Yellow)},
            {new Tile(13, TileType.Yellow)},
            
            {new Tile(1, TileType.Blue)},
            {new Tile(2, TileType.Blue)},
            {new Tile(3, TileType.Blue)},
            {new Tile(4, TileType.Blue)},
            {new Tile(5, TileType.Blue)},
            {new Tile(6, TileType.Blue)},
            {new Tile(7, TileType.Blue)},
            {new Tile(8, TileType.Blue)},
            {new Tile(9, TileType.Blue)},
            {new Tile(10, TileType.Blue)},
            {new Tile(11, TileType.Blue)},
            {new Tile(12, TileType.Blue)},
            {new Tile(13, TileType.Blue)},

            {new Tile(1, TileType.Black)},
            {new Tile(2, TileType.Black)},
            {new Tile(3, TileType.Black)},
            {new Tile(4, TileType.Black)},
            {new Tile(5, TileType.Black)},
            {new Tile(6, TileType.Black)},
            {new Tile(7, TileType.Black)},
            {new Tile(8, TileType.Black)},
            {new Tile(9, TileType.Black)},
            {new Tile(10, TileType.Black)},
            {new Tile(11, TileType.Black)},
            {new Tile(12, TileType.Black)},
            {new Tile(13, TileType.Black)},
            
            {new Tile(1, TileType.Red)},
            {new Tile(2, TileType.Red)},
            {new Tile(3, TileType.Red)},
            {new Tile(4, TileType.Red)},
            {new Tile(5, TileType.Red)},
            {new Tile(6, TileType.Red)},
            {new Tile(7, TileType.Red)},
            {new Tile(8, TileType.Red)},
            {new Tile(9, TileType.Red)},
            {new Tile(10, TileType.Red)},
            {new Tile(11, TileType.Red)},
            {new Tile(12, TileType.Red)},
            {new Tile(13, TileType.Red)},
            
            {new Tile(TileType.Joker)}
        };

        public static Tile GetRandomTile()
        {
            return new Tile(Random.Range(0, NumberCountInEachType), (TileType)Random.Range((int)TileType.Yellow, (int)TileType.Red));
        }
        
        public static Tile GetOkeyTile(Tile indicatorTile)
        {
            return new Tile(indicatorTile.Number + 1 % NumberCountInEachType, indicatorTile.Type);
        }
    }
}