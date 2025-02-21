using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField]
        Button _button;
        
        UIManager _uiManager;
        
        void Awake()
        {
            _uiManager = FindObjectsByType<UIManager>(FindObjectsSortMode.None).FirstOrDefault();
            
            if (_uiManager == null)
            {
                Debug.LogError("UIManager not found in scene");
            }
        }
        
        void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        void OnClick()
        {
            _uiManager.GoBack();
        }
    }
}