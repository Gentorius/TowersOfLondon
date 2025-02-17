using System.Linq;
using UnityEngine;

namespace Rings
{
    public class RingManager : MonoBehaviour
    {
        [SerializeField]
        RingPrefabs _ringPrefabs;
    
        Ring[] _rings;
        bool[] _isRingIndexUsed;
        Ring _selectedRing;
    
        void Start()
        {
            if (_ringPrefabs.IsEmpty)
            {
                Debug.LogError("RingPrefabs is empty!");
            }
        
            _rings = FindObjectsByType<Ring>(FindObjectsSortMode.None);
            
            foreach (var ring in _rings)
            {
                Destroy(ring.gameObject);
            }
        }
        
        public bool TrySpawnRing(Vector3 position, int x, int y)
        {
            if (!TryGetUniqueRingPrefab(out var ringPrefab))
                return false;
        
            var ring = new GameObject().AddComponent<Ring>();
            ring.transform.position = position;
            ring.SetPosition(x, y);
            ring.SetRingPrefab(ringPrefab);
            ring.OnRingClicked += OnRingClicked;
            return true;
        }

        void OnRingClicked(Ring clickedRing)
        {
            if (_selectedRing != null)
            {
                return;
            }
            
            if (!IsTopRing(clickedRing))
            {
                return;
            }
            
            _selectedRing = clickedRing;
            clickedRing.OnRingClicked -= OnRingClicked;
            clickedRing.RemovePosition();
        }

        bool TryGetUniqueRingPrefab(out GameObject ringPrefab)
        {
            _isRingIndexUsed = new bool[_ringPrefabs.ringPrefabs.Length];
        
            var randomIndex = Random.Range(0, _ringPrefabs.ringPrefabs.Length);
        
            if (!_isRingIndexUsed[randomIndex])
            {
                _isRingIndexUsed[randomIndex] = true;
                ringPrefab = _ringPrefabs.ringPrefabs[randomIndex];
                return true;
            }

            for (var i = 0; i < _ringPrefabs.ringPrefabs.Length; i++)
            {
                if (_isRingIndexUsed[i])
                    continue;

                _isRingIndexUsed[i] = true;
                ringPrefab = _ringPrefabs.ringPrefabs[i];
                return true;
            }
            
            Debug.LogWarning("All ring prefabs are used!");
            ringPrefab = null;
            return false;
        }
        
        bool IsTopRing(Ring ring)
        {
            var x = ring.X;
            var y = ring.Y;
            return !_rings.Any(r => r.X == x && r.Y == y - 1);
        }
    }
}
