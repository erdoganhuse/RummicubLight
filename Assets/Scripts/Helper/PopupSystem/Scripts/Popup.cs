using System;
using Helper.PopupSystem.Scripts.AnimationStrategy;
using Helper.PopupSystem.Scripts.Background;
using UnityEngine;

namespace Helper.PopupSystem.Scripts
{
    public class Popup<T> : Popup where T : Popup<T>
    {
        private bool _shouldCloseImmediately;

        public override void Init()
        {
            SetAnimationStrategy(new DefaultAnimationStrategy());
        }

        public override void Clear()
        {
            DestroyImmediate(gameObject);
            if (Background != null)
            {
                DestroyImmediate(Background.gameObject);
                Background = null;
            }
        }

        public override void Setup(Action closeCallback = null)
        {
            CloseCallback = closeCallback;
            CanvasGroup.interactable = true;

            if (BaseSettings.HasBackground && BaseSettings.BackgroundPrefab != null)
            {
                Background = Instantiate(BaseSettings.BackgroundPrefab, transform.parent);
                Background.gameObject.transform.SetAsLastSibling();
            }
        }
        
        public override void Close(bool shouldCloseImmediately = false)
        {
            _shouldCloseImmediately = shouldCloseImmediately;
            CloseCallback?.Invoke();
        }
        
        public override void Enable()
        {
            OpenBackground(() => 
            {
                gameObject.SetActive(true);
                CanvasGroup.interactable = false;
                AnimationStrategy.PerformOpenAnimation(this, () =>
                {
                    CanvasGroup.interactable = true;
                });            
            });
        }

        public override void Disable()
        {
            gameObject.SetActive(false);
            if(Background != null) 
                Background.gameObject.SetActive(false);
        }
        
        public override void AnimateIn(Action onOpenAnimationEnded = null)
        {               
            OpenBackground(() =>
            {
                transform.SetAsLastSibling();
                gameObject.SetActive(true);
                CanvasGroup.interactable = false;
                
                OnOpenBegan();
                AnimationStrategy.PerformOpenAnimation(this, () =>
                {
                    CanvasGroup.interactable = true;
                    OnOpened();
                    onOpenAnimationEnded?.Invoke();;
                });
            });            
        }

        public override void AnimateOut(Action onCloseAnimationEnded = null)
        {
            OnCloseBegan();

            if (_shouldCloseImmediately)
            {
                _shouldCloseImmediately = false;

                if(Background != null) 
                    Background.Clear();
                
                gameObject.SetActive(false);
                OnClosed();
                onCloseAnimationEnded?.Invoke();                
            }
            else
            {  
                CanvasGroup.interactable = false;
                AnimationStrategy.PerformCloseAnimation(this, () =>
                {
                    gameObject.SetActive(false);
                    CanvasGroup.interactable = true;
                    CloseBackground(() => 
                    {
                        OnClosed();
                        onCloseAnimationEnded?.Invoke();;                    
                    });
                });
            }
        }

        private void OpenBackground(Action onOpened = null)
        {
            if(Background != null)
            {
                Background.gameObject.SetActive(true);
                Background.Open(
                    () => { if(BaseSettings.BackgroundClosable) Close(); }, 
                    () => onOpened?.Invoke()
                );
            }
            else
            {
                onOpened?.Invoke();
            }
        }

        private void CloseBackground(Action onClosed = null)
        {
            if(Background != null) 
                Background.Close(() => onClosed?.Invoke());
            else 
                onClosed?.Invoke();
        }
    }
    
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField] private Settings _baseSettings;
        public Settings BaseSettings => _baseSettings;

        protected Action CloseCallback;  
        protected BackgroundItem Background;
        protected IAnimationStrategy AnimationStrategy;
        
        private CanvasGroup _canvasGroup;
        protected CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null) _canvasGroup = GetComponentInChildren<CanvasGroup>(true);
                return _canvasGroup;
            }
        }

        public abstract void Init();
        public abstract void Clear();
        public abstract void Setup(Action closeCallback = null);
        public abstract void Close(bool closeImmediately = false);
        
        public abstract void Enable();
        public abstract void Disable();
        
        public abstract void AnimateIn(Action onOpenAnimationEnded = null);
        public abstract void AnimateOut(Action onCloseAnimationEnded = null);

        protected virtual void OnOpened(){}
        protected virtual void OnOpenBegan(){}
        protected virtual void OnClosed(){}
        protected virtual void OnCloseBegan(){}

        protected void SetAnimationStrategy(IAnimationStrategy animationStrategy)
        {
            AnimationStrategy = animationStrategy;
        }
    }
    
    public enum PriorityLevel { None, Low, Normal, High, Alert }

    [Serializable]
    public class Settings
    {
        public PriorityLevel PriorityLevel;
        public bool ClosePopupsUnderneath;
        public bool DisablePopupsUnderneath;
        public bool HasBackground;
        public BackgroundItem BackgroundPrefab;
        public bool BackgroundClosable;        
    }
    
}
