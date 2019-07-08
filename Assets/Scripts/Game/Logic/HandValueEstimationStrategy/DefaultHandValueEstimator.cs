using System.Collections.Generic;
using System.Linq;
using Constants;
using Game.Data;

namespace Game.Logic.HandValueEstimationStrategy
{
    public class DefaultHandValueEstimator : IHandValueEstimator
    {
        private const float OkeyTileValueMultiplier = 5f;
        private const float DoubleMatchValue = 1f;

        private const float ThreeNumberMatchValue = 1.5f;
        private const float EachTileValueAfterThreeNumberMatch = 0.5f;
            
        private const float ThreeColorMatchValue = 1.5f;        
        private const float EachTileValueAfterThreeColorMatch = 0.5f;
        
        public int GetEstimatedHandScore(Hand hand, Tile okeyTile)
        {
            float estimatedHandScore = 0;
            
            estimatedHandScore += OkeyTileValueMultiplier * GetOkeyTileCount(ref hand, okeyTile);
            
            ReplaceJokerTiles(ref hand, okeyTile);
                        
            estimatedHandScore += DoubleMatchValue * GetDoubleMatchCount(ref hand, okeyTile);

            List<int> numberMatchList = GetColorMatchList(ref hand, okeyTile);
            for (int i = 0; i < numberMatchList .Count; i++)
            {
                estimatedHandScore += ThreeNumberMatchValue;
                estimatedHandScore += (numberMatchList [i] - 3) * EachTileValueAfterThreeNumberMatch;                
            }

            List<int> colorMatchList = GetColorMatchList(ref hand, okeyTile);
            for (int i = 0; i < colorMatchList.Count; i++)
            {
                estimatedHandScore += ThreeColorMatchValue;
                estimatedHandScore += (colorMatchList[i] - 3) * EachTileValueAfterThreeColorMatch;                
            }
            
            return (int)estimatedHandScore;
        }

        private int GetOkeyTileCount(ref Hand hand, Tile okeyTile)
        {
            return hand.Tiles.Count(item => item.Number == okeyTile.Number && item.Type == okeyTile.Type);
        }

        private int GetDoubleMatchCount(ref Hand hand, Tile okeyTile)
        {
            int doubleMatchCount = 0;
            for (int num = 1; num < Deck.NumberCountInEachType + 1; num++)
            {
                int numberMatchCount = hand.Tiles.Count(item => item.Number == num);
                doubleMatchCount += num / 2;
            }
            return doubleMatchCount;
        }
                
        private List<int> GetNumberMatchList(ref Hand hand, Tile okeyTile)
        {
            List<int> numberMatchList = new List<int>();

            IEnumerable<Tile> yellowTiles =
                hand.Tiles.FindAll(item => item.Type == TileType.Yellow).OrderBy(item => item.Number);
            numberMatchList.Concat(GetNumberMatchList(ref yellowTiles));
            
            IEnumerable<Tile> blueTiles =
                hand.Tiles.FindAll(item => item.Type == TileType.Blue).OrderBy(item => item.Number);
            numberMatchList.Concat(GetNumberMatchList(ref blueTiles));
            
            IEnumerable<Tile> blackTiles =
                hand.Tiles.FindAll(item => item.Type == TileType.Black).OrderBy(item => item.Number);
            numberMatchList.Concat(GetNumberMatchList(ref blackTiles));

            IEnumerable<Tile> redTiles =
                hand.Tiles.FindAll(item => item.Type == TileType.Red).OrderBy(item => item.Number);
            numberMatchList.Concat(GetNumberMatchList(ref redTiles));
            
            return numberMatchList;
        }

        private List<int> GetNumberMatchList(ref IEnumerable<Tile> orderedTileList)
        {
            List<int> numberMatchList = new List<int>();
   
            int consecutiveNumberCount = 0;
            for (int i = 0; i < orderedTileList.Count(); i++)
            {
                if (consecutiveNumberCount == 0 || IsPreviousTileConsecutive(ref orderedTileList, i))
                    consecutiveNumberCount++;

                if (consecutiveNumberCount > 3 && !IsNextTileConsecutive(ref orderedTileList, i))
                {
                    numberMatchList.Add(consecutiveNumberCount);
                    consecutiveNumberCount = 0;
                }
            }
            
            return numberMatchList;
        }   
        
        private List<int> GetColorMatchList(ref Hand hand, Tile okeyTile)
        {
            List<int> colorMatchList = new List<int>();
            
            for (int num = 1; num < Deck.NumberCountInEachType + 1; num++)
            {
                int matchCount = hand.Tiles.Count(item => item.Number == num);
                if(matchCount >= 3) colorMatchList.Add(matchCount);
            }
            
            return colorMatchList;
        }

        private void ReplaceJokerTiles(ref Hand hand, Tile okeyTile)
        {
            if (hand.Tiles.Any(item => item.Type == TileType.Joker))
            {
                int jokerCount = hand.Tiles.Count(item => item.Type == TileType.Joker);
                hand.Tiles.RemoveAll(item => item.Type == TileType.Joker);
                for (int i = 0; i < jokerCount; i++)
                {
                    hand.Tiles.Add(okeyTile);
                }
            }
        }
        
        private bool IsConsecutiveNumbers(int firstNumber, int secondNumber)
        {
            if (firstNumber == 13 && secondNumber == 1)
            {
                return true;
            }

            return firstNumber + 1 == secondNumber;
        }

        private bool IsNextTileConsecutive(ref IEnumerable<Tile> tiles, int currentIndex)
        {
            if (currentIndex == tiles.Count() - 1)
                return IsConsecutiveNumbers(tiles.ElementAt(currentIndex).Number, tiles.ElementAt(0).Number);
                
            return IsConsecutiveNumbers(tiles.ElementAt(currentIndex).Number, tiles.ElementAt(currentIndex + 1).Number);
        }        
        
        private bool IsPreviousTileConsecutive(ref IEnumerable<Tile> tiles, int currentIndex)
        {
            if (currentIndex == 0)
                return IsConsecutiveNumbers(tiles.ElementAt(0).Number, tiles.ElementAt(tiles.Count() - 1).Number);
                
            return IsConsecutiveNumbers(tiles.ElementAt(currentIndex - 1).Number, tiles.ElementAt(currentIndex).Number);
        }
    }
}
