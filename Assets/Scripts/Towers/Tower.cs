using System;
using Rings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Towers
{
    public class Tower : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        int _towerIndex;
        [SerializeField]
        Placement _upperRingPlacement;
        [SerializeField]
        Placement _middleRingPlacement;
        [SerializeField]
        Placement _lowerRingPlacement;

        public int TowerIndex => _towerIndex;
        public event Action<Tower> OnTowerClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnTowerClicked?.Invoke(this);
        }
        
        public void PlaceRing(Ring ring, int placementIndex)
        {
            var ringPlacement = GetRingPlacement(placementIndex);
            ring.transform.SetParent(ringPlacement.gameObject.transform);
        }
        
        public void RemoveRing(int ringIndex)
        {
            var ringPlacement = GetRingPlacement(ringIndex);
            var ring = ringPlacement.gameObject.transform.GetChild(0).gameObject;
            Destroy(ring);
        }
        
        public bool TryGetLowestOpenRingPlacement(out Vector3 position, out int placementIndex)
        {
            if (!_lowerRingPlacement.IsOccupied)
            {
                position = _lowerRingPlacement.transform.position;
                placementIndex = 2;
                return true;
            }
            
            if (!_middleRingPlacement.IsOccupied)
            {
                position = _middleRingPlacement.transform.position;
                placementIndex = 1;
                return true;
            }
            
            if (!_upperRingPlacement.IsOccupied)
            {
                position = _upperRingPlacement.transform.position;
                placementIndex = 0;
                return true;
            }

            position = Vector3.zero;
            placementIndex = -1;
            return false;
        }
        
        public Vector3 GetPlacementPosition(int placementIndex)
        {
            return GetRingPlacement(placementIndex).transform.position;
        }

        Placement GetRingPlacement(int placementIndex)
        {
            return placementIndex switch
            {
                0 => _upperRingPlacement,
                1 => _middleRingPlacement,
                2 => _lowerRingPlacement,
                _ => null
            };
        }
    }
}