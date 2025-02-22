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
        string LevelDataPath => Application.dataPath + "/LevelData/";
        string LevelDataExtension => ".json";
        public string LevelPath { get; private set; }

        void Awake()
        {
            _settingsPresenter = _uiManager.GetPresenter<SettingsPresenter>();
            _settingsPresenter.OnDifficultySelected += OnDifficultySelectedHandler;
            LevelPath = LevelDataPath + "TowerOfLondon-Medium" + LevelDataExtension;
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
                Difficulty.Easy => LevelDataPath + "TowerOfLondon-Easy" + LevelDataExtension,
                Difficulty.Medium => LevelDataPath + "TowerOfLondon-Medium" + LevelDataExtension,
                Difficulty.Hard => LevelDataPath + "TowerOfLondon-Hard" + LevelDataExtension,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}