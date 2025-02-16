using System;
using Rings;

namespace Levels
{
    [Serializable]
    public class TileData
    {
        Ring _ring;
        public bool IsOccupied { get; private set; }
        
        public void SetRing(Ring ring)
        {
            _ring = ring;
            IsOccupied = true;
        }
        
        public Ring RemoveRing()
        {
            var ring = _ring;
            _ring = null;
            IsOccupied = false;
            return ring;
        }
    }
}