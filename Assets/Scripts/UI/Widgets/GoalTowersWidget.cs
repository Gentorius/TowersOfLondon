using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class GoalTowersWidget : MonoBehaviour
    {
        [SerializeField]
        Button _closeButton;

        public event Action OnCloseButtonClickedEvent;
        
        void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        
        void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }
        
        void OnCloseButtonClicked()
        {
            gameObject.SetActive(false);
            OnCloseButtonClickedEvent?.Invoke();
        }
    }
}