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
        
        public int RingIndex { get; private set; }
        public bool IsOccupied => RingIndex != -1;
        
        TileData (int ringIndex)
        {
            RingIndex = ringIndex;
        }
        
        public void SetRing(int ringIndex)
        {
            RingIndex = ringIndex;
        }
        
        public int RemoveRing()
        {
            var ringIndex = RingIndex;
            RingIndex = -1;
            return ringIndex;
        }
        
        public void Deserialize()
        {
            RingIndex = _ringIndex;
        }

        public TileData Clone()
        {
            var tileData = new TileData(RingIndex);
            return tileData;
        }
    }
}