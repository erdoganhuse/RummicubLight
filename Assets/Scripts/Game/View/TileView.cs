using Constants;
using Game.Data;
using Helper.Extensions;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.View
{
    public class TileView : MonoBehaviour
    {
        private const float DistanceBetweenDigits = 0.17f;
        
        [SerializeField] private SpriteRenderer _firstDigitRenderer;
        [SerializeField] private SpriteRenderer _secondDigitRenderer;
        [SerializeField] private SpriteRenderer _heartRenderer;
        [SerializeField] private SpriteRenderer _jokerRenderer;

        private TileVisualContainer _tileVisualContainer;
        private Tile _tile;

        [Inject]
        private void Construct(TileVisualContainer tileVisualContainer)
        {
            _tileVisualContainer = tileVisualContainer;
        }
        
        public void Initialize(Tile tile)
        {
            _tile = tile;
        }
        
        public void Setup()
        {
            _jokerRenderer.gameObject.SetActive(false);
            _firstDigitRenderer.gameObject.SetActive(false);
            _secondDigitRenderer.gameObject.SetActive(false);
            _heartRenderer.gameObject.SetActive(false);

            if (_tile.Type == TileType.Joker)
            {
                _jokerRenderer.gameObject.SetActive(true);
                _jokerRenderer.sprite = _tileVisualContainer.GetJokerSprite();
                _jokerRenderer.color = Color.magenta;
            }
            else if (_tile.Number < 10)
            {
                Setup(_tileVisualContainer.GetDigitSprite(_tile.Number),
                    _tileVisualContainer.GetTileTypeColor(_tile.Type));
            }
            else
            {
                int firstDigit = _tile.Number / 10;
                int secondDigit = _tile.Number % 10;
                Setup(_tileVisualContainer.GetDigitSprite(firstDigit), _tileVisualContainer.GetDigitSprite(secondDigit),
                    _tileVisualContainer.GetTileTypeColor(_tile.Type));
            }
            
            gameObject.SetActive(true);
        }
        
        public void Setup(Sprite firstDigitSprite, Color color)
        {
            _firstDigitRenderer.gameObject.SetActive(true);
            _heartRenderer.gameObject.SetActive(true);
            
            _firstDigitRenderer.sprite = firstDigitSprite;
            _firstDigitRenderer.color = color;
            
            _heartRenderer.color = color;

            ArrangeDigitLayout();
        }
        
        public void Setup(Sprite firstDigitSprite, Sprite secondDigitSprite, Color color)
        {
            _firstDigitRenderer.gameObject.SetActive(true);
            _secondDigitRenderer.gameObject.SetActive(true);
            _heartRenderer.gameObject.SetActive(true);
            
            _firstDigitRenderer.sprite = firstDigitSprite;
            _firstDigitRenderer.color = color;            

            _secondDigitRenderer.sprite = secondDigitSprite;
            _secondDigitRenderer.color = color;
            
            _heartRenderer.color = color;

            ArrangeDigitLayout();
        }

        public void Clear() { }
        
        private void ArrangeDigitLayout()
        {
            if (_secondDigitRenderer.gameObject.activeSelf)
            {
                _firstDigitRenderer.transform.SetLocalPosX(-1f * DistanceBetweenDigits / 2f);
                _secondDigitRenderer.transform.SetLocalPosX(DistanceBetweenDigits / 2f);                
            }
            else
            {
                _firstDigitRenderer.transform.SetLocalPosX(0f);
            }
        }
    }
}
