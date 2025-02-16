using UnityEngine;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        int _towerIndex;
        [SerializeField]
        GameObject _upperRingPlacement;
        [SerializeField]
        GameObject _middleRingPlacement;
        [SerializeField]
        GameObject _lowerRingPlacement;

        public int TowerIndex => _towerIndex;

        public void PlaceRing(GameObject ringPrefab, int ringIndex)
        {
            var ringPlacement = GetRingPlacement(ringIndex);
            var ring = Instantiate(ringPrefab, ringPlacement.transform.position, Quaternion.identity);
            ring.transform.SetParent(ringPlacement.transform);
        }
        
        public void RemoveRing(int ringIndex)
        {
            var ringPlacement = GetRingPlacement(ringIndex);
            var ring = ringPlacement.transform.GetChild(0).gameObject;
            Destroy(ring);
        }

        GameObject GetRingPlacement(int ringIndex)
        {
            return ringIndex switch
            {
                0 => _upperRingPlacement,
                1 => _middleRingPlacement,
                2 => _lowerRingPlacement,
                _ => null
            };
        }
    }
}