using Levels;
using UI.Basics;
using UI.Menu;
using UI.Pause;
using UI.Widgets;

namespace UI.Level
{
    public class LevelPresenter : BasicPresenter<LevelView>
    {
        PausePresenter _pausePresenter;
        LevelController _levelController;
        MenuPresenter _menuPresenter;
        ResultWidget ResultWidget => View.ResultWindow;
        
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            _pausePresenter = uiManager.GetPresenter<PausePresenter>();
            _levelController = uiManager.GetMonoBehaviour<LevelController>();
            _menuPresenter = uiManager.GetPresenter<MenuPresenter>();
        }

        protected override void OnShow()
        {
            View.OnPauseButtonClicked += OnPauseButtonClicked;
            View.SetTurnCounterText(_levelController.TurnsLeft.ToString());
            _levelController.OnVictory += OnVictoryHandler;
            _levelController.OnDefeat += OnDefeatHandler;
            _levelController.OnTurnCountChanged += OnTurnCountChangedHandler;
            ResultWidget.OnReturnToMenuButtonClicked += OnReturnToMenuButtonClickedHandler;
            ResultWidget.OnRestartButtonClicked += OnRestartButtonClickedHandler;
        }
        
        protected override void OnHide()
        {
            View.OnPauseButtonClicked -= OnPauseButtonClicked;
            _levelController.OnVictory -= OnVictoryHandler;
            _levelController.OnDefeat -= OnDefeatHandler;
            _levelController.OnTurnCountChanged -= OnTurnCountChangedHandler;
            ResultWidget.OnReturnToMenuButtonClicked -= OnReturnToMenuButtonClickedHandler;
            ResultWidget.OnRestartButtonClicked -= OnRestartButtonClickedHandler;
        }

        void OnPauseButtonClicked()
        {
            HideWindow();
            _pausePresenter.LoadAndShowWindow();
        }

        void OnVictoryHandler()
        {
            View.ShowResultWindow(true);
        }
        
        void OnDefeatHandler()
        {
            View.ShowResultWindow(false);
        }
        
        void OnTurnCountChangedHandler(int turnsLeft)
        {
            View.SetTurnCounterText(turnsLeft.ToString());
        }
        
        void OnReturnToMenuButtonClickedHandler()
        {
            HideWindow();
            _menuPresenter.LoadAndShowWindow();
        }
        
        void OnRestartButtonClickedHandler()
        {
            _levelController.RestartLevel();
            ResultWidget.gameObject.SetActive(false);
        }
    }
}