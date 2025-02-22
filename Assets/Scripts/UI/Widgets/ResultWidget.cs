using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ResultWidget : MonoBehaviour
    {
        const string _winText = "You won!";
        const string _loseText = "You lost!";   
        
        [SerializeField]
        TextMeshProUGUI _resultText;
        [SerializeField]
        Button _returnToMenuButton;
        [SerializeField]
        Button _restartButton;
        
        public event System.Action OnReturnToMenuButtonClicked;
        public event System.Action OnRestartButtonClicked;
        
        void OnEnable()
        {
            _returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonClickedInternal);
            _restartButton.onClick.AddListener(OnRestartButtonClickedInternal);
        }
        
        void OnDisable()
        {
            _returnToMenuButton.onClick.RemoveListener(OnReturnToMenuButtonClickedInternal);
            _restartButton.onClick.RemoveListener(OnRestartButtonClickedInternal);
        }
        
        public void SetResultText(bool win)
        {
            _resultText.text = win ? _winText : _loseText;
        }
        
        void OnReturnToMenuButtonClickedInternal()
        {
            OnReturnToMenuButtonClicked?.Invoke();
        }
        
        void OnRestartButtonClickedInternal()
        {
            OnRestartButtonClicked?.Invoke();
        }
    }
}