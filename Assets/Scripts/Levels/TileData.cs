using System;

namespace Levels
{
    [Serializable]
    public class TileData
    {
        public int RingIndex { get; private set; }
        public bool IsOccupied { get; private set; }
        
        public void SetRing(int ringIndex)
        {
            RingIndex = ringIndex;
            IsOccupied = true;
        }
        
        public int RemoveRing()
        {
            var ringIndex = RingIndex;
            IsOccupied = false;
            RingIndex = -1;
            return ringIndex;
        }
    }
}