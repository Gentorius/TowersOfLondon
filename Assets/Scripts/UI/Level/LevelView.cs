using System;
using TMPro;
using UI.Basics;
using UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Level
{
    public class LevelView : BasicView
    {
        [SerializeField]
        GoalTowersWidget _goalTowersWidget;
        [SerializeField]
        Button _pauseButton;
        [SerializeField]
        TextMeshProUGUI _turnCounterText;
        [SerializeField]
        Button _showGoalTowersButton;
        [SerializeField]
        ResultWidget _resultWindow;
        
        public event Action OnPauseButtonClicked;
        public ResultWidget ResultWindow => _resultWindow;

        void OnEnable()
        {
            _showGoalTowersButton.onClick.AddListener(OnShowGoalTowersButtonClicked);
            _goalTowersWidget.OnCloseButtonClickedEvent += OnGoalTowersWidgetCloseButtonClicked;
            _pauseButton.onClick.AddListener(OnPauseButtonClickedInternal);
            _resultWindow.gameObject.SetActive(false);
            _resultWindow.OnRestartButtonClicked += OnRestartButtonClickedHandler;
        }
        
        void OnDisable()
        {
            _showGoalTowersButton.onClick.RemoveListener(OnShowGoalTowersButtonClicked);
            _goalTowersWidget.OnCloseButtonClickedEvent -= OnGoalTowersWidgetCloseButtonClicked;
            _pauseButton.onClick.RemoveListener(OnPauseButtonClickedInternal);
            _resultWindow.OnRestartButtonClicked -= OnRestartButtonClickedHandler;
        }

        public void SetTurnCounterText(string text)
        {
            _turnCounterText.text = text;
        }
        
        public void ShowResultWindow(bool win)
        {
            _resultWindow.gameObject.SetActive(true);
            _resultWindow.SetResultText(win);
        }
        
        void OnShowGoalTowersButtonClicked()
        {
            _showGoalTowersButton.gameObject.SetActive(false);
            _goalTowersWidget.gameObject.SetActive(true);
        }
        
        void OnGoalTowersWidgetCloseButtonClicked()
        {
            _goalTowersWidget.gameObject.SetActive(false);
            _showGoalTowersButton.gameObject.SetActive(true);
        }
        
        void OnPauseButtonClickedInternal()
        {
            OnPauseButtonClicked?.Invoke();
        }
        
        void OnRestartButtonClickedHandler()
        {
            _showGoalTowersButton.gameObject.SetActive(true);
        }
    }
}