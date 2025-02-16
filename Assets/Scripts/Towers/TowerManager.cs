using UnityEngine;

namespace Towers
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField]
        Tower[] _towers;
        
        public bool TryPlaceRing(int towerIndex, GameObject ringPrefab, int ringIndex)
        {
            if (towerIndex < 0 || towerIndex >= _towers.Length)
                return false;
            
            _towers[towerIndex].PlaceRing(ringPrefab, ringIndex);
            return true;
        }
        
        public bool TryRemoveRing(int towerIndex, int ringIndex)
        {
            if (towerIndex < 0 || towerIndex >= _towers.Length)
                return false;
            
            _towers[towerIndex].RemoveRing(ringIndex);
            return true;
        }
    }
}