using System;
using Rings;
using UnityEngine;

namespace Towers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField]
        RingManager _ringManager;
        [SerializeField]
        Tower[] _towers;
        [SerializeField]
        Tower[] _goalTowers;
        
        public event Action<Vector2> OnRingPlaced; 
        
        void Start()
        {
            foreach (var tower in _towers)
            {
                tower.OnTowerClicked += OnTowerClicked;
            }
        }
        
        public void PlaceRingInStartingPosition(int ringIndex, int row, int column, out Ring ring)
        {
            var tower = _towers[column];
            var placementPosition = tower.GetPlacementPosition(row);
            _ringManager.SpawnGameplayRing(placementPosition, ringIndex, row, column, out ring);
            tower.PlaceRing(ring, row);
        }
        
        public void PlaceRingInGoalTower(int ringIndex, int row, int column)
        {
            var tower = _goalTowers[column];
            var placementPosition = tower.GetPlacementPosition(row);
            var ring = _ringManager.SpawnRing(placementPosition, ringIndex, row, column);
            tower.PlaceRing(ring, row);
        }

        void OnTowerClicked(Tower tower)
        {
            if (!_ringManager.IsRingSelected)
            {
                return;
            }
            
            if (!tower.TryGetLowestOpenRingPlacement(out var placementPosition, out var placementIndex))
            {
                return;
            }
            
            var ring = _ringManager.SelectedRing;
            OnRingPlaced?.Invoke(new Vector2(tower.TowerIndex, placementIndex));
            _ringManager.PlaceRing(placementPosition, placementIndex, tower.TowerIndex);
            tower.PlaceRing(ring, placementIndex);
        }

        void OnDestroy()
        {
            foreach (var tower in _towers)
            {
                tower.OnTowerClicked -= OnTowerClicked;
            }
        }
    }
}