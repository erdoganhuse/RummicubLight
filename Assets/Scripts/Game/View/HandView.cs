using System.Collections.Generic;
using Game.Data;
using Helper.Layout;
using Helper.ObjectPoolSystem;
using UnityEngine;

namespace Game.View
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private TextMesh _handId;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private Hand _hand;
        private GenericPoolManager<TileView> _tileViewPoolManager;

        private readonly List<TileView> _instantiatedTileViews = new List<TileView>();
        
        public void Initialize(Hand hand, GenericPoolManager<TileView> poolManager)
        {
            _hand = hand;
            _tileViewPoolManager = poolManager;
        }

        public void Setup()
        {
            _handId.text = $"Hand Id:{_hand.Id}";
            
            for (int i = 0; i < _hand.Tiles.Count; i++)
            {
                TileView tileView = _tileViewPoolManager.Spawn();
                tileView.Initialize(_hand.Tiles[i]);
                tileView.Setup();
                _instantiatedTileViews.Add(tileView);
                _gridLayoutGroup.AddChild(tileView.gameObject);
            }
            
            gameObject.SetActive(true);
        }

        public void Clear()
        {
            _hand = null;
            _gridLayoutGroup.Clear();
            for (int i = 0; i < _instantiatedTileViews.Count; i++)
            {
                _tileViewPoolManager.Recycle(_instantiatedTileViews[i]);
            }
            _instantiatedTileViews.Clear();
        }
    }
}