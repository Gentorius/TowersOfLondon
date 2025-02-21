using UI.Basics;
using UI.Level;
using UI.Settings;

namespace UI.Menu
{
    public class MenuPresenter : BasicPresenter<MenuView>
    {
        LevelPresenter _levelPresenter;
        SettingsPresenter _settingsPresenter;

        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            _levelPresenter = UIManager.GetPresenter<LevelPresenter>();
            _settingsPresenter = UIManager.GetPresenter<SettingsPresenter>();
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
            _levelPresenter.LoadAndShowWindow();
            HideWindow();
        }

        void OnSettingsButtonClickedHandler()
        {
            _settingsPresenter.LoadAndShowWindow();
            HideWindow();
        }
        
        void OnExitButtonClickedHandler()
        {
            UnityEngine.Application.Quit();
        }
    }
}