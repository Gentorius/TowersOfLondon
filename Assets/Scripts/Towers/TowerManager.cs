using System;
using Rings;
using UnityEngine;

namespace Towers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField]
        Tower[] _towers;
        [SerializeField]
        RingManager _ringManager;
        
        public event Action<Vector2> OnRingPlaced; 
        
        void Start()
        {
            foreach (var tower in _towers)
            {
                tower.OnTowerClicked += OnTowerClicked;
            }
        }
        
        public void PlaceRingInSpecificPlacement(int ringIndex, int x, int y, out Ring ring)
        {
            var tower = _towers[x];
            var placementPosition = tower.GetPlacementPosition(y);
            _ringManager.TrySpawnRing(placementPosition, ringIndex, x, y, out ring);
            tower.PlaceRing(ring, y);
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
            _ringManager.PlaceRing(placementPosition, tower.TowerIndex, placementIndex);
            tower.PlaceRing(ring, placementIndex);
            OnRingPlaced?.Invoke(new Vector2(tower.TowerIndex, placementIndex));
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