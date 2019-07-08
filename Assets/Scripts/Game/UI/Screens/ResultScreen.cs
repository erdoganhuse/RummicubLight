using Helper.PopupSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Screens
{
    public class ResultScreen : Popup<ResultScreen>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Text _resultText;
                
        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();

            _closeButton.onClick.AddListener(CloseButton_OnClick);
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            _closeButton.onClick.RemoveListener(CloseButton_OnClick);
        }
        
        public void Setup(int handId, int handScore)
        {
            _resultText.text = $"Best Hand Id: {handId}\nScore:{handScore}";
        }
        
        #region UI Event Listeners

        private void CloseButton_OnClick()
        {
            Close();
        }

        #endregion
    }
}
