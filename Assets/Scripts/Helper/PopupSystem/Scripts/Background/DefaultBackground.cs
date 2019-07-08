using System;
using DG.Tweening;
using Helper.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Helper.PopupSystem.Scripts.Background
{
    public class DefaultBackground : BackgroundItem
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _openAnimDuration = 0.25f;
        [SerializeField] private float _closeAnimDuration = 0.25f;

        
        public override void Setup()
        {
            _backgroundImage.color = Color.black.WithAlpha(0f);
        }

        public override void Clear()
        {
            CloseCallback = null;
            _backgroundImage.color = Color.black.WithAlpha(0f);
        }

        public override void Open(Action closeCallback, Action onOpened = null)
        {
            CloseCallback = closeCallback;
            
            Setup();
            _backgroundImage.DOFade(0.75f, _openAnimDuration).OnComplete(() => onOpened?.Invoke());
        }

        public override void Close(Action onClosed = null)
        {
            _backgroundImage.DOFade(0f, _closeAnimDuration).OnComplete(() =>
            {
                Clear();
                onClosed?.Invoke();
            });
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            CloseCallback?.Invoke();            
        }
    }
}
