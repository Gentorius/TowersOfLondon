using Levels;
using UI.Basics;
using UI.Menu;

namespace UI.Pause
{
    public class PausePresenter : BasicPresenter<PauseView>
    {
        MenuPresenter _menuPresenter;
        LevelController _levelController;
        
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            _menuPresenter = UIManager.GetPresenter<MenuPresenter>();
            _levelController = UIManager.GetMonoBehaviour<LevelController>();
        }
        
        protected override void OnShow()
        {
            View.OnResumeButtonClicked += OnResumeButtonClickedHandler;
            View.OnRestartButtonClicked += OnRestartButtonClickedHandler;
            View.OnReturnToMenuButtonClicked += OnReturnToMenuButtonClickedHandler;
            View.OnExitButtonClicked += OnExitButtonClickedHandler;
        }
        
        protected override void OnHide()
        {
            View.OnResumeButtonClicked -= OnResumeButtonClickedHandler;
            View.OnRestartButtonClicked -= OnRestartButtonClickedHandler;
            View.OnReturnToMenuButtonClicked -= OnReturnToMenuButtonClickedHandler;
            View.OnExitButtonClicked -= OnExitButtonClickedHandler;
        }
        
        void OnResumeButtonClickedHandler()
        {
            UIManager.GoBack();
        }
        
        void OnRestartButtonClickedHandler()
        {
            _levelController.StartLevel();
            UIManager.GoBack();
        }
        
        void OnReturnToMenuButtonClickedHandler()
        {
            HideWindow();
            _menuPresenter.ShowWindow();
        }

        static void OnExitButtonClickedHandler()
        {
            UnityEngine.Application.Quit();
        }
    }
}