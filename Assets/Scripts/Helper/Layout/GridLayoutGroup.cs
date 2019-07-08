using System.Collections.Generic;
using System.Linq;
using Helper.Extensions;
using UnityEngine;

namespace Helper.Layout
{
    public class GridLayoutGroup : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private RectOffset _padding;
        [SerializeField] private Vector2 _spacing;
        [SerializeField] private int _columnCount;
        [SerializeField] private int _rowCount;
        
        private readonly List<GameObject> _childList = new List<GameObject>();

        private Vector2 BackgroundSize => _background.bounds.size;
        private Vector2 LeftTopPosition => new Vector2(_background.transform.localPosition.x - BackgroundSize.x / 2f,
            _background.transform.localPosition.y + BackgroundSize.y / 2f);
        private float CellWidth => BackgroundSize.x / _columnCount;
        private float CellHeight => BackgroundSize.y / _rowCount;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RearrangeLayout();
            }
        }
        
        public void AddChild(GameObject child)
        {
            child.transform.SetParent(transform);
            child.transform.LocalReset();
            _childList.Add(child);
            
            RearrangeLayout();
        }

        public void RemoveChild(int index, out GameObject removedChild)
        {
            removedChild = _childList[index];
            removedChild.transform.SetParent(null);
            _childList.RemoveAt(index);
        }

        public void Clear()
        {
            for (int i = 0; i < _childList.Count; i++)
            {
                _childList[i].transform.SetParent(null);
            }
            _childList.Clear();
        }
        
        public int GetChildCount()
        {
            return _childList.Count;
        }
        
        private void RearrangeLayout()
        {
            for (int i = 0; i < _childList.Count; i++)
            {
                int columnIndex = i % _columnCount;
                int rowIndex = (i / _columnCount);
                float posX = LeftTopPosition.x + CellWidth * ((2 * columnIndex + 1) / 2f);
                float posY = LeftTopPosition.y - CellHeight * ((2 * rowIndex + 1) / 2f);
                
                _childList[i].transform.SetLocalPosX(posX);
                _childList[i].transform.SetLocalPosY(posY);
            }
        }
    }
}
