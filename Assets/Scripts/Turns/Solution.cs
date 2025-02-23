using System;
using System.Collections.Generic;
using System.Linq;
using Levels;
using UnityEngine;

namespace Turns
{
    [Serializable]
    public class Solution
    {
        [SerializeField]
        PathByRingIndex[] _paths;
        [SerializeField]
        int _turnCount;
        
        public int TurnCount { get; private set; }
        
        public void SetStartingLayout(LevelLayout startingLayout)
        {
            var tiles = startingLayout.Tiles;
            var ringIndexes = new List<int>();
            var initialX = new List<int>();
            var initialY = new List<int>();

            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (!tiles[x, y].IsOccupied)
                        continue;

                    ringIndexes.Add(tiles[x, y].RingIndex);
                    initialX.Add(x);
                    initialY.Add(y);
                }
            }
            
            GeneratePathForEachRing(ringIndexes, initialX, initialY);
        }
        
        public void MoveRing(int ringIndex, int x, int y)
        {
            var path = GetPathByRingIndex(ringIndex);
            TurnCount++;
            path.AddPathPoint(x, y, TurnCount);
        }
        
        public void Serialize()
        {
            _turnCount = TurnCount;
            
            foreach (var path in _paths)
            {
                path.Serialize();
            }
        }
        
        public void Deserialize()
        {
            TurnCount = _turnCount;
            
            foreach (var path in _paths)
            {
                path.Deserialize();
            }
        }
        
        void GeneratePathForEachRing(List<int> ringIndexes, List<int> initialX, List<int> initialY)
        {
            _paths = new PathByRingIndex[ringIndexes.Count];
            for (var i = 0; i < ringIndexes.Count; i++)
            {
                _paths[i] = new PathByRingIndex();
                _paths[i].SetRingIndex(ringIndexes[i]);
                _paths[i].AddPathPoint(initialX[i], initialY[i], TurnCount);
            }
        }

        PathByRingIndex GetPathByRingIndex(int ringIndex)
        {
            return _paths.FirstOrDefault(t => t.RingIndex == ringIndex);
        }
    }
}