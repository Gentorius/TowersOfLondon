using System;
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

        LevelLayout _currentLevelLayout;
        LevelLayout _goalLevelLayout;
        readonly List<Ring> _rings = new();
        int _turnGoal;
        int _turnCount;
        readonly SolutionFinder _solutionFinder = new();
        public int TurnsLeft => _turnGoal - _turnCount;

        public event Action OnVictory;
        public event Action OnDefeat;
        public event Action<int> OnTurnCountChanged;

        public void StartLevel()
        {
            var level = LevelReader.ReadLevelFromJson(_settings.LevelPath);
            BuildLevel(level);
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
            
            _ringManager.DestroyAllRings();
            _turnCount = 0;
            
            for (var row = 0; row < 3; row++)
            {
                for (var column = 0; column < 3; column++)
                {
                    if (!level.StartingLayout.Tiles[row, column].IsOccupied)
                    {
                        continue;
                    }
                    
                    var ringIndex = level.StartingLayout.Tiles[row, column].RingIndex;
                    _towerManager.PlaceRingInStartingPosition(ringIndex, row, column, out var ring);
                    _rings.Add(ring);
                    ring.OnRingRemoved += OnRingRemoved;
                }
            }

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var ringIndex = level.GoalLayout.Tiles[x, y].RingIndex;
                    
                    if (ringIndex == -1)
                    {
                        continue;
                    }
                    
                    _towerManager.PlaceRingInGoalTower(ringIndex, x, y);
                }
            }

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
            var column = (int)position.x;
            var row = (int)position.y;
            var ringIndex = _ringManager.SelectedRing.RingIndex;
            
            _currentLevelLayout.TryPlaceRingByCoordinates(ringIndex, row, column);
            _turnCount++;
            OnTurnCountChanged?.Invoke(TurnsLeft);
            
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
            OnVictory?.Invoke();
        }
        
        void Defeat()
        {
            CommonCleanup();
            OnDefeat?.Invoke();
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