using System.Collections.Generic;
using Game.Controller;
using Game.Data;
using Helper.PopupSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Screens
{
    public class MainScreen : Popup<MainScreen>
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _calculateButton;
        
        private BoardController _boardController;
        private PopupManager _gamePopupManager;
        
        [Inject]
        private void Construct(
            BoardController boardController,
            PopupManager gamePopupManager)
        {
            _boardController = boardController;
            _gamePopupManager = gamePopupManager;
        }

        protected override void OnOpenBegan()
        {
            base.OnOpenBegan();

            _startButton.onClick.AddListener(StartButton_OnClick);
            _clearButton.onClick.AddListener(ClearButton_OnClick);
            _calculateButton.onClick.AddListener(CalculateButton_OnClick);
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            _startButton.onClick.RemoveListener(StartButton_OnClick);
            _clearButton.onClick.RemoveListener(ClearButton_OnClick);
            _calculateButton.onClick.RemoveListener(CalculateButton_OnClick);
        }

        #region UI Event Listener
        
        private void StartButton_OnClick()
        {
            _boardController.CreateHands();
        }

        private void ClearButton_OnClick()
        {
            _boardController.Clear();
        }

        private void CalculateButton_OnClick()
        {
            KeyValuePair<int, int> bestHandIdAndScore = _boardController.GetBestHandIdAndScore();
            _gamePopupManager.Open<ResultScreen>();
            _gamePopupManager.Get<ResultScreen>().Setup(bestHandIdAndScore.Key, bestHandIdAndScore.Value);
        }
        
        #endregion
    }
}
