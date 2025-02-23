using System;
using System.Collections.Generic;
using UnityEngine;

namespace Turns
{
    [Serializable]
    public class PathByRingIndex
    {
        [SerializeField]
        int _ringIndex;
        [SerializeField]
        List<int> _x = new();
        [SerializeField]
        List<int> _y = new();
        [SerializeField]
        List<int> _turnIndexes = new();

        public int RingIndex { get; private set; }
        public List<int> X { get; private set; } = new();
        public List<int> Y { get; private set; } = new();
        public List<int> TurnIndexes { get; private set; } = new();

        public void SetRingIndex(int ringIndex)
        {
            RingIndex = ringIndex;
        }
        
        public void AddPathPoint(int x, int y, int turnIndex)
        {
            X.Add(x);
            Y.Add(y);
            TurnIndexes.Add(turnIndex);
        }
        
        public void Serialize()
        {
            _ringIndex = RingIndex;
            _x = X;
            _y = Y;
            _turnIndexes = TurnIndexes;
        }
        
        public void Deserialize()
        {
            RingIndex = _ringIndex;
            X = _x;
            Y = _y;
            TurnIndexes = _turnIndexes;
        }
    }
}