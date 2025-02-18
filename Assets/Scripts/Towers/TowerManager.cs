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
        
        void Start()
        {
            foreach (var tower in _towers)
            {
                tower.OnTowerClicked += OnTowerClicked;
            }
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