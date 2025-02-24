using UI;
using UI.Menu;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        UIManager _uiManager;
        
        MenuPresenter _menuPresenter;

        void Start()
        {
            _menuPresenter = _uiManager.GetPresenter<MenuPresenter>();
            _menuPresenter.LoadAndShowWindow();
        }
    }
}