using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rings
{
    public class RingManager : MonoBehaviour
    {
        [SerializeField]
        RingPrefabs _ringPrefabs;
    
        List<Ring> _rings;
        bool[] _isRingIndexUsed;
        public Ring SelectedRing { get; private set; }
        
        public bool IsRingSelected => SelectedRing != null;
    
        void Start()
        {
            if (_ringPrefabs.IsEmpty)
            {
                Debug.LogError("RingPrefabs is empty!");
            }
        
            _rings = FindObjectsByType<Ring>(FindObjectsSortMode.None).ToList();
            
            foreach (var ring in _rings)
            {
                Destroy(ring.gameObject);
            }
        }
        
        public bool TrySpawnRing(Vector3 position, int ringIndex, int x, int y, out Ring ring)
        {
            if (!TryGetUniqueRingPrefab(out var ringPrefab))
            {
                ring = null;
                return false;
            }
        
            ring = new GameObject().AddComponent<Ring>();
            ring.transform.position = position;
            ring.SetPosition(x, y);
            ring.SetRingPrefab(ringPrefab);
            ring.OnRingClicked += OnRingClicked;
            _rings.Add(ring);
            ring.RingIndex = ringIndex;
            return true;
        }
        
        public void PlaceRing(Vector3 position, int x, int y)
        {
            if (SelectedRing == null)
            {
                return;
            }
            
            SelectedRing.transform.position = position;
            SelectedRing.SetPosition(x, y);
            SelectedRing.OnRingClicked += OnRingClicked;
            SelectedRing = null;
        }

        void OnRingClicked(Ring clickedRing)
        {
            if (SelectedRing != null)
            {
                return;
            }
            
            if (!IsTopRing(clickedRing))
            {
                return;
            }
            
            SelectedRing = clickedRing;
            clickedRing.OnRingClicked -= OnRingClicked;
            clickedRing.RemovePosition();
        }

        bool TryGetUniqueRingPrefab(out GameObject ringPrefab)
        {
            _isRingIndexUsed = new bool[_ringPrefabs.RingPrefabList.Length];
        
            var randomIndex = Random.Range(0, _ringPrefabs.RingPrefabList.Length);
        
            if (!_isRingIndexUsed[randomIndex])
            {
                _isRingIndexUsed[randomIndex] = true;
                ringPrefab = _ringPrefabs.RingPrefabList[randomIndex];
                return true;
            }

            for (var i = 0; i < _ringPrefabs.RingPrefabList.Length; i++)
            {
                if (_isRingIndexUsed[i])
                    continue;

                _isRingIndexUsed[i] = true;
                ringPrefab = _ringPrefabs.RingPrefabList[i];
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
