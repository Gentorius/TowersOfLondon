using System;
using Settings;
using UI.Basics;

namespace UI.Settings
{
    public class SettingsPresenter : BasicPresenter<SettingsView>
    {
        public event Action<Difficulty> OnDifficultySelected; 
        
        protected override void OnShow()
        {
            View.OnEasyButtonClicked += OnEasyButtonClickedHandler;
            View.OnMediumButtonClicked += OnMediumButtonClickedHandler;
            View.OnHardButtonClicked += OnHardButtonClickedHandler;
        }

        void OnEasyButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Easy);
        }

        void OnMediumButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Medium);
        }
        
        void OnHardButtonClickedHandler()
        {
            OnDifficultySelected?.Invoke(Difficulty.Hard);
        }
    }
}