using System;
using UI.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MenuView : BasicView
    {
        [SerializeField]
        Button _startButton;
        [SerializeField]
        Button _settingsButton;
        [SerializeField]
        Button _exitButton;
        
        public event Action OnStartButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnExitButtonClicked;

        void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonClickedHandler);
            _settingsButton.onClick.AddListener(OnSettingsButtonClickedHandler);
            _exitButton.onClick.AddListener(OnExitButtonClickedHandler);
        }
        
        void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartButtonClickedHandler);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClickedHandler);
            _exitButton.onClick.RemoveListener(OnExitButtonClickedHandler);
        }
        
        void OnStartButtonClickedHandler()
        {
            OnStartButtonClicked?.Invoke();
        }
        
        void OnSettingsButtonClickedHandler()
        {
            OnSettingsButtonClicked?.Invoke();
        }
        
        void OnExitButtonClickedHandler()
        {
            OnExitButtonClicked?.Invoke();
        }
    }
}