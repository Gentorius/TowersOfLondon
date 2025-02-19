using System.Collections.Generic;
using NUnit.Framework;

namespace Turns
{
    public class PathByRingIndex
    {
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
    }
}