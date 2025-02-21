using UI.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Pause
{
    public class PauseView : BasicView
    {
        [SerializeField]
        Button _resumeButton;
        [SerializeField]
        Button _restartButton;
        [SerializeField]
        Button _returnToMenuButton;
        [SerializeField]
        Button _exitButton;
        
        public event System.Action OnResumeButtonClicked;
        public event System.Action OnRestartButtonClicked;
        public event System.Action OnReturnToMenuButtonClicked;
        public event System.Action OnExitButtonClicked;
        
        void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonClickedHandler);
            _restartButton.onClick.AddListener(OnRestartButtonClickedHandler);
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonClickedHandler);
            _exitButton.onClick.AddListener(OnExitButtonClickedHandler);
        }
        
        void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClickedHandler);
            _restartButton.onClick.RemoveListener(OnRestartButtonClickedHandler);
            _returnToMenuButton.onClick.RemoveListener(OnReturnToMenuButtonClickedHandler);
            _exitButton.onClick.RemoveListener(OnExitButtonClickedHandler);
        }
        
        void OnResumeButtonClickedHandler()
        {
            OnResumeButtonClicked?.Invoke();
        }
        
        void OnRestartButtonClickedHandler()
        {
            OnRestartButtonClicked?.Invoke();
        }
        
        void OnReturnToMenuButtonClickedHandler()
        {
            OnReturnToMenuButtonClicked?.Invoke();
        }
        
        void OnExitButtonClickedHandler()
        {
            OnExitButtonClicked?.Invoke();
        }
    }
}