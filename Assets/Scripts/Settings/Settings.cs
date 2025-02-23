using System;
using UI;
using UI.Settings;
using UnityEngine;

namespace Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        UIManager _uiManager;
        
        SettingsPresenter _settingsPresenter;
        Difficulty _difficulty = Difficulty.Medium;
        static string LevelDataPath => Application.dataPath + "/LevelData/TowerOfLondon-";
        static string LevelDataExtension => ".json";
        public string LevelPath { get; private set; }

        void Awake()
        {
            _settingsPresenter = _uiManager.GetPresenter<SettingsPresenter>();
            _settingsPresenter.OnDifficultySelected += OnDifficultySelectedHandler;
            LevelPath = LevelDataPath + "Medium" + LevelDataExtension;
        }

        void OnDestroy()
        {
            _settingsPresenter.OnDifficultySelected -= OnDifficultySelectedHandler;
        }

        public int GetTurnSum(int turnCount)
        {
            return _difficulty switch
            {
                Difficulty.Easy => turnCount + 4,
                Difficulty.Medium => turnCount + 2,
                Difficulty.Hard => turnCount,
                _ => turnCount + 2
            };
        }
        
        void OnDifficultySelectedHandler(Difficulty difficulty)
        {
            _difficulty = difficulty;

            LevelPath = _difficulty switch
            {
                Difficulty.Easy => LevelDataPath + "Easy" + LevelDataExtension,
                Difficulty.Medium => LevelDataPath + "Medium" + LevelDataExtension,
                Difficulty.Hard => LevelDataPath + "Hard" + LevelDataExtension,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}