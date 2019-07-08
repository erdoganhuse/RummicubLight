using System;
using System.Collections.Generic;
using Constants;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TileVisualContainer", menuName = "ScriptableObject/Container/TileVisualContainer")]
    public class TileVisualContainer : ScriptableObject
    {
        [SerializeField] private List<IntSpritePair> _digitSprites;
        [SerializeField] private List<TileTypeColorPair> _tileTypeColors;
        [SerializeField] private Sprite _jokerSprite;

        public Sprite GetJokerSprite()
        {
            return _jokerSprite;
        }
        
        public Sprite GetDigitSprite(int digit)
        {
            return _digitSprites.Find(item => item.Key == digit).Value;
        }

        public Color GetTileTypeColor(TileType tileType)
        {            
            return _tileTypeColors.Find(item => item.Key == tileType).Value;
        }
    }

    [Serializable]
    public struct IntSpritePair
    {
        public int Key;
        public Sprite Value;
        
        public IntSpritePair(int key, Sprite value)
        {
            Key = key;
            Value = value;
        }
    }
    
    [Serializable]
    public struct TileTypeColorPair
    {
        public TileType Key;
        public Color Value;
        
        public TileTypeColorPair(TileType key, Color value)
        {
            Key = key;
            Value = value;
        }
    }    
}