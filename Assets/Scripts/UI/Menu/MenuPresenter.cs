using Levels;
using UI.Basics;
using UI.Level;
using UI.Settings;

namespace UI.Menu
{
    public class MenuPresenter : BasicPresenter<MenuView>
    {
        LevelPresenter _levelPresenter;
        SettingsPresenter _settingsPresenter;
        LevelController _levelController;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            _levelPresenter = UIManager.GetPresenter<LevelPresenter>();
            _settingsPresenter = UIManager.GetPresenter<SettingsPresenter>();
            _levelController = UIManager.GetMonoBehaviour<LevelController>();
        }

        protected override void OnShow()
        {
            View.OnStartButtonClicked += OnStartButtonClickedHandler;
            View.OnSettingsButtonClicked += OnSettingsButtonClickedHandler;
            View.OnExitButtonClicked += OnExitButtonClickedHandler;
        }
        
        protected override void OnHide()
        {
            View.OnStartButtonClicked -= OnStartButtonClickedHandler;
            View.OnSettingsButtonClicked -= OnSettingsButtonClickedHandler;
            View.OnExitButtonClicked -= OnExitButtonClickedHandler;
        }
        
        void OnStartButtonClickedHandler()
        {
            _levelController.StartLevel();
            _levelPresenter.LoadAndShowWindow();
            HideWindow();
        }

        void OnSettingsButtonClickedHandler()
        {
            _settingsPresenter.LoadAndShowWindow();
            HideWindow();
        }

        static void OnExitButtonClickedHandler()
        {
            UnityEngine.Application.Quit();
        }
    }
}