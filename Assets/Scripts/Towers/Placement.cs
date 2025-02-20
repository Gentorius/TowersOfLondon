using Rings;
using UnityEngine;

namespace Towers
{
    public class Placement : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }
        Ring _ring;
        
        public void SetRing(Ring ring)
        {
            _ring = ring;
            IsOccupied = true;
            _ring.OnRingRemoved += RemoveRing;
        }

        void RemoveRing(Vector2 position)
        {
            _ring.OnRingRemoved -= RemoveRing;
            _ring = null;
            IsOccupied = false;
        }
    }
}