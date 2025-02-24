using Rings;
using UnityEngine;

namespace Towers
{
    public class Placement : MonoBehaviour
    {
        public bool IsOccupied => _ring != null;
        Ring _ring;
        
        public void SetRing(Ring ring)
        {
            _ring = ring;
            _ring.OnRingRemoved += RemoveRing;
        }

        void RemoveRing(Vector2 position)
        {
            _ring.OnRingRemoved -= RemoveRing;
            _ring = null;
        }
    }
}