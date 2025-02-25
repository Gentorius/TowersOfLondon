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
        readonly Dictionary<int, int> _ringToPrefab = new();
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
        
        public Ring SpawnGameplayRing(Vector3 position, int ringIndex, int x, int y)
        {
            if (!TryGetUniqueRingPrefab(ringIndex, out var ringPrefab))
                return null;
            
            var ring = SpawnRing(position, ringIndex, x, y, ringPrefab);
            _rings.Add(ring);
            return ring;
        }
        
        public Ring SpawnGoalRing(Vector3 position, int ringIndex, int x, int y)
        {
            return !TryGetPlacedRingPrefab(ringIndex, out var ringPrefab) 
                ? null 
                : SpawnRing(position, ringIndex, x, y, ringPrefab);
        }

        Ring SpawnRing(Vector3 position, int ringIndex, int x, int y, GameObject ringPrefab)
        {
            var ring = new GameObject().AddComponent<Ring>();
            ring.transform.position = position;
            ring.SetPosition(x, y);
            ring.SetRingPrefab(ringPrefab);
            ring.OnRingClicked += OnRingClicked;
            ring.RingIndex = ringIndex;
            return ring;
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
        
        public void DestroyAllRings()
        {
            if (_rings == null || _rings.Count == 0)
            {
                return;
            }
            
            foreach (var ring in _rings)
            {
                ring.OnRingClicked -= OnRingClicked;
                ring.DestroyRing();
            }
        
            _rings.Clear();
            
            if (SelectedRing != null)
            {
                SelectedRing.OnRingClicked -= OnRingClicked;
                SelectedRing.DestroyRing();
                SelectedRing = null;
            }

            _ringToPrefab.Clear();
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

        bool TryGetUniqueRingPrefab(int ringIndex, out GameObject ringPrefab)
        {
            var randomIndex = Random.Range(0, _ringPrefabs.RingPrefabList.Length);
        
            if (!_ringToPrefab.ContainsValue(randomIndex))
            {
                ringPrefab = _ringPrefabs.RingPrefabList[randomIndex];
                _ringToPrefab.Add(ringIndex, randomIndex);
                return true;
            }

            for (var i = 0; i < _ringPrefabs.RingPrefabList.Length; i++)
            {
                if (_ringToPrefab.ContainsValue(i))
                    continue;

                ringPrefab = _ringPrefabs.RingPrefabList[i];
                _ringToPrefab.Add(ringIndex, i);
                return true;
            }
            
            Debug.LogWarning("All ring prefabs are used!");
            ringPrefab = null;
            return false;
        }
        
        bool TryGetPlacedRingPrefab(int ringIndex, out GameObject ringPrefab)
        {
            if (_ringToPrefab.TryGetValue(ringIndex, out var prefabIndex))
            {
                ringPrefab = _ringPrefabs.RingPrefabList[prefabIndex];
                return true;
            }

            Debug.LogError($"Ring index {ringIndex} not found!");
            ringPrefab = null;
            return false;

        }
        
        bool IsTopRing(Ring ring)
        {
            var x = ring.X;
            var y = ring.Y;
            return !_rings.Any(r => r.X == x - 1 && r.Y == y);
        }
    }
}
