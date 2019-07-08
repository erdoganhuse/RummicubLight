using System.Collections.Generic;
using Game.Data;
using Game.Logic.HandValueEstimationStrategy;
using Game.View;
using Helper.Extensions;
using Helper.Layout;
using Helper.ObjectPoolSystem;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Controller
{
    public class BoardController : MonoBehaviour
    {
        private const int HandDefaultTileCount = 14;
        
        [SerializeField] private int _handCount;
        [SerializeField] private Transform _indicatorTileContainer;
        [SerializeField] private TileView _tileViewPrefab;
        [SerializeField] private HandView _handViewPrefab;
        [SerializeField] private Transform _boardContainer;
        [SerializeField] private GridLayoutGroup _handGridLayoutGroup;
        
        private IHandValueEstimator _handValueEstimator;

        private readonly List<Hand> _hands = new List<Hand>();
        private Tile _indicatorTile, _okeyTile;
        private List<Tile> _doubleDeck;
        
        private GenericPoolManager<HandView> _handViewPoolManager;
        private GenericPoolManager<TileView> _tileViewPoolManager;

        private TileView _indicatorTileView;
        private readonly List<HandView> _currentHandViews = new List<HandView>();

        [Inject]
        private void Construct(IHandValueEstimator handValueEstimator)
        {
            _handValueEstimator = handValueEstimator;
        }
        
        private void Start()
        {
            Initialize();
        }
        
        public void Initialize()
        {   
            _handViewPoolManager = new GenericPoolManager<HandView>(_handViewPrefab, _boardContainer, _handCount);
            _tileViewPoolManager = new GenericPoolManager<TileView>(_tileViewPrefab, _boardContainer);
        }

        public void Setup()
        {
            _doubleDeck = CreateDoubleDeck();
            _indicatorTile = Deck.GetRandomTile();
            _okeyTile = Deck.GetOkeyTile(_indicatorTile);   
        }
        
        public void Clear()
        {
            for (int i = 0; i < _currentHandViews.Count; i++)
            {
                _currentHandViews[i].Clear();
                _handViewPoolManager.Recycle(_currentHandViews[i]);
            }            
            
            _hands.Clear();
            _currentHandViews.Clear();
            _handGridLayoutGroup.Clear();
            
            if(_indicatorTileView != null) _tileViewPoolManager.Recycle(_indicatorTileView);
            _indicatorTileView = null;
        }
        
        public void CreateHands()
        {
            if (!_hands.IsEmpty()) return;
            
            Setup();
            
            int handIdWithExtraTile = Random.Range(0, _handCount);

            _indicatorTileView = _tileViewPoolManager.Spawn(_indicatorTileContainer);
            _indicatorTileView.Initialize(_okeyTile);
            _indicatorTileView.Setup();
            
            for (int i = 0; i < _handCount; i++)
            {
                Hand hand =  new Hand(i, GetHandTiles(handIdWithExtraTile == i ? HandDefaultTileCount + 1 : HandDefaultTileCount));
                _hands.Add(hand);
                
                HandView handView = _handViewPoolManager.Spawn(_boardContainer);     
                _currentHandViews.Add(handView);
                _handGridLayoutGroup.AddChild(handView.gameObject);
                handView.Initialize(hand, _tileViewPoolManager);
                handView.Setup();
            }
        }
        
        private List<Tile> CreateDoubleDeck()
        {
            List<Tile> doubleDeck = new List<Tile>();
            doubleDeck.AddRange(Deck.Tiles);
            doubleDeck.AddRange(Deck.Tiles);
            doubleDeck.Shuffle();
            return doubleDeck;
        }
        
        public List<Tile> GetHandTiles(int tileCount)
        {
            List<Tile> handTiles = new List<Tile>();
            for (int i = 0; i < tileCount; i++)
            {
                handTiles .Add(_doubleDeck.RemoveFirst());
            }
    
            return handTiles;;
        }

        public KeyValuePair<int, int> GetBestHandIdAndScore()
        {
            if (_hands.IsEmpty()) return new KeyValuePair<int, int>(-1, -1);
            
            int handIdWithMaxScore = 0;
            int maxScore = 0;

            for (int i = 0; i < _hands.Count; i++)
            {
                int handScore = _handValueEstimator.GetEstimatedHandScore(_hands[i], _okeyTile);
                if (maxScore < handScore)
                {
                    maxScore = handScore;
                    handIdWithMaxScore = _hands[i].Id;
                }
            }
            
            return new KeyValuePair<int, int>(handIdWithMaxScore, maxScore);
        }
    }
}
