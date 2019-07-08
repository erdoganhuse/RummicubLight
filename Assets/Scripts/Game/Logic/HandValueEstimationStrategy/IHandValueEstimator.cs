using System.Collections.Generic;
using Game.Data;

namespace Game.Logic.HandValueEstimationStrategy
{
    interface IHandValueEstimator
    {
        int GetEstimatedHandScore(Hand hand, Tile okeyTile);
    }
}
