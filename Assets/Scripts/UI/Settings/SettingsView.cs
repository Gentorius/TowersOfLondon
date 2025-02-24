using System;
using Settings;
using UI.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SettingsView : BasicView
    {
        [SerializeField]
        Button _easyButton;
        [SerializeField]
        Button _mediumButton;
        [SerializeField]
        Button _hardButton;
        [SerializeField]
        GameObject _highlighter;

        public event Action OnEasyButtonClicked;
        public event Action OnMediumButtonClicked;
        public event Action OnHardButtonClicked;

        void OnEnable()
        {
            _easyButton.onClick.AddListener(OnEasyButtonClickedHandler);
            _mediumButton.onClick.AddListener(OnMediumButtonClickedHandler);
            _hardButton.onClick.AddListener(OnHardButtonClickedHandler);
        }
        
        void OnDisable()
        {
            _easyButton.onClick.RemoveListener(OnEasyButtonClickedHandler);
            _mediumButton.onClick.RemoveListener(OnMediumButtonClickedHandler);
            _hardButton.onClick.RemoveListener(OnHardButtonClickedHandler);
        }
        
        public void PlaceHighlighterOnButton(Difficulty difficulty)
        {
            _highlighter.transform.position = difficulty switch
            {
                Difficulty.Easy => _easyButton.gameObject.transform.position,
                Difficulty.Medium => _mediumButton.gameObject.transform.position,
                Difficulty.Hard => _hardButton.gameObject.transform.position,
                _ => _mediumButton.transform.position
            };
        }
        
        void OnEasyButtonClickedHandler()
        {
            _highlighter.transform.position = _easyButton.gameObject.transform.position;
            OnEasyButtonClicked?.Invoke();
        }
        
        void OnMediumButtonClickedHandler()
        {
            _highlighter.transform.position = _mediumButton.gameObject.transform.position;
            OnMediumButtonClicked?.Invoke();
        }
        
        void OnHardButtonClickedHandler()
        {
            _highlighter.transform.position = _hardButton.gameObject.transform.position;
            OnHardButtonClicked?.Invoke();
        }
    }
}