using System.Collections.Generic;
using Rings;
using Towers;
using Turns;
using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        TowerManager _towerManager;
        [SerializeField]
        RingManager _ringManager;
        [SerializeField]
        Settings.Settings _settings;
        
        Level _currentLevel;
        LevelLayout _currentLevelLayout;
        LevelLayout _goalLevelLayout;
        readonly List<Ring> _rings = new();
        int _turnGoal;
        int _turnCount;
        readonly SolutionFinder _solutionFinder = new();
        readonly LevelReader _levelReader = new();

        public void StartLevel(string levelName)
        {
            var level = _levelReader.ReadLevelFromJson(levelName);
            BuildLevel(level);
        }
        
        public void RestartLevel()
        {
            foreach (var ring in _rings)
            {
                ring.OnRingRemoved -= OnRingRemoved;
                Destroy(ring.gameObject);
            }
            
            _rings.Clear();
            _towerManager.OnRingPlaced -= OnRingPlaced;
            BuildLevel(_currentLevel);
        }
        
        void BuildLevel(Level level)
        {
            if (!LevelValidator.IsLayoutValid(level.StartingLayout) )
            {
                Debug.LogError("Invalid level starting layout");
                return;
            }
            
            if (!LevelValidator.IsLayoutValid(level.GoalLayout) )
            {
                Debug.LogError("Invalid level goal layout");
                return;
            }
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var ringIndex = level.StartingLayout.Tiles[x, y].RingIndex;
                    _towerManager.PlaceRingInSpecificPlacement(ringIndex, x, y, out var ring);
                    _rings.Add(ring);
                    ring.OnRingRemoved += OnRingRemoved;
                }
            }
            
            _currentLevel = level;
            _currentLevelLayout = level.StartingLayout;
            _goalLevelLayout = level.GoalLayout;
            _towerManager.OnRingPlaced += OnRingPlaced;

            var bestSolution = level.Solution;
            
            if (level.Solution == null || level.Solution.TurnCount < 1)
            {
                bestSolution = _solutionFinder.FindBestSolution(_currentLevelLayout, _goalLevelLayout, out _);
            }
            
            _turnGoal = _settings.GetTurnSum(bestSolution.TurnCount);
        }

        void OnRingPlaced(Vector2 position)
        {
            var x = (int)position.x;
            var y = (int)position.y;
            var ringIndex = _ringManager.SelectedRing.RingIndex;
            
            _currentLevelLayout.TryPlaceRingByCoordinates(ringIndex, x, y);
            _turnCount++;
            
            if (_currentLevelLayout.Equals(_goalLevelLayout))
            {
                Victory();
            }
            
            if (_turnCount >= _turnGoal)
            {
                Defeat();
            }
        }

        void OnRingRemoved(Vector2 position)
        {
            _currentLevelLayout.RemoveRingByCoordinates((int) position.x, (int) position.y);
        }
        
        void Victory()
        {
            CommonCleanup();
            Debug.Log("Level complete!");
        }
        
        void Defeat()
        {
            CommonCleanup();
            Debug.Log("Level failed!");
        }
        
        void CommonCleanup()
        {
            foreach (var ring in _rings)
            {
                ring.OnRingRemoved -= OnRingRemoved;
            }
            
            _towerManager.OnRingPlaced -= OnRingPlaced;
        }
    }
}