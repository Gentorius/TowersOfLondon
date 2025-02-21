using UI;
using UI.Menu;
using UnityEngine;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        UIManager _uiManager;

        void Start()
        {
            _uiManager.GetPresenter<MenuPresenter>().LoadAndShowWindow();
            Destroy(gameObject);
        }
    }
}