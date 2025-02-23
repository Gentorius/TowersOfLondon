using System;
using Turns;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class TileData
    {
        [SerializeField]
        int _ringIndex = -1;
        [SerializeField]
        bool _isOccupied;
        
        public int RingIndex { get; private set; }
        public bool IsOccupied { get; private set; }
        
        TileData (int ringIndex, bool isOccupied)
        {
            RingIndex = ringIndex;
            IsOccupied = isOccupied;
        }
        
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
        
        public void Deserialize()
        {
            RingIndex = _ringIndex;
            IsOccupied = _isOccupied;
        }

        public TileData Clone()
        {
            var tileData = new TileData(RingIndex, IsOccupied);
            return tileData;
        }
    }
}