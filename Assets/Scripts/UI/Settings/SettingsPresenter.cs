using System;
using Settings;
using UI.Basics;

namespace UI.Settings
{
    public class SettingsPresenter : BasicPresenter<SettingsView>
    {
        public event Action<Difficulty> OnDifficultySelected;
        Difficulty _currentDifficulty = Difficulty.Medium;
        
        protected override void OnShow()
        {
            View.OnEasyButtonClicked += OnEasyButtonClickedHandler;
            View.OnMediumButtonClicked += OnMediumButtonClickedHandler;
            View.OnHardButtonClicked += OnHardButtonClickedHandler;
            View.PlaceHighlighterOnButton(_currentDifficulty);
        }

        protected override void OnHide()
        {
            View.OnEasyButtonClicked -= OnEasyButtonClickedHandler;
            View.OnMediumButtonClicked -= OnMediumButtonClickedHandler;
            View.OnHardButtonClicked -= OnHardButtonClickedHandler;
        }

        void OnEasyButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Easy);
            _currentDifficulty = Difficulty.Easy;
        }

        void OnMediumButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Medium);
            _currentDifficulty = Difficulty.Medium;
        }
        
        void OnHardButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Hard);
            _currentDifficulty = Difficulty.Hard;
        }
    }
}