using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Helper.PopupSystem.Scripts
{
    [DisallowMultipleComponent]
    public class PopupManager : MonoBehaviour
    {
        public event Action<Type> OnOpened;
        public event Action<Type> OnClosed;

        public Popup CurrentPopup { get; private set; }
        public Popup PreviousPopup { get; private set; }
        
        [SerializeField] private RectTransform _container;
        [SerializeField] private Popup[] _popups;
        
        private readonly Stack<Popup> _activePopupStack = new Stack<Popup>();

        public T Get<T>() where T : Popup
        {
            return _activePopupStack.Get<T>();
        }
        
        public void Open<T>() where T : Popup
        {
            if (CurrentPopup != null && CurrentPopup.GetType() == typeof(T)) return;
            if (_activePopupStack.Any<T>()) return;
            
            Popup popup = Instantiate(_popups.Get<T>(), _container);
            popup.Init();
            
            if(CurrentPopup != null && CurrentPopup.BaseSettings.PriorityLevel > _popups.Get<T>().BaseSettings.PriorityLevel)
            {
                _activePopupStack.Push(popup);
                return;
            }
            
            Register(popup);
            Open(popup);
        }
        
        public void Close<T>() where T : Popup
        {
            if (_activePopupStack.IsEmpty() || _activePopupStack.Peek().GetType() != typeof(T)) return;
            
            Popup popup = _activePopupStack.Get<T>();
            popup.AnimateOut(() =>
            {
                _activePopupStack.Pop();
                
                PreviousPopup = CurrentPopup;
                CurrentPopup = null;
                    
                OnClosed?.Invoke(popup.GetType());
                popup.Clear();

                if (_activePopupStack.Count > 0) OpenFromStack();
            });
        }

        private void Register(Popup popup)
        {
            if (_activePopupStack.Count > 0)
            {
                if (popup.BaseSettings.ClosePopupsUnderneath)
                {
                    foreach (var item in _activePopupStack) item.Close(true);
                }
                else if (popup.BaseSettings.DisablePopupsUnderneath)
                {
                    foreach (var item in _activePopupStack) item.Disable();
                }
            }
            
            CurrentPopup = popup;
            _activePopupStack.Push(popup);
        }
        
        private void Open(Popup popup)
        {
            popup.Setup(() =>
            {
                typeof(PopupManager)
                    .GetMethod("Close")
                    ?.MakeGenericMethod(popup.GetType())
                    .Invoke(this, null);
            });
                        
            popup.AnimateIn(() =>
            {
                OnOpened?.Invoke(popup.GetType());
            });
        }
        
        private void OpenFromStack()
        {
            Popup popup = _activePopupStack.Pop();
            
            Register(popup);
            if(!popup.gameObject.activeSelf) popup.Enable();
        }        
    }
}
