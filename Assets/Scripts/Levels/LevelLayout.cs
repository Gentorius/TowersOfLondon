using System;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class LevelLayout : ICloneable
    {
        [SerializeField]
        TileData[] _upperTiles = new TileData[3];
        [SerializeField]
        TileData[] _middleTiles = new TileData[3];
        [SerializeField]
        TileData[] _lowerTiles = new TileData[3];
        
        public TileData[,] Tiles { get; internal set; } = new TileData[3, 3];

        public bool TryPlaceRingByCoordinates (int ringIndex, int x, int y)
        {
            if (IsIndexPlaced(ringIndex))
            {
                return false;
            }
            
            Tiles[x, y].SetRing(ringIndex);
            return true;
        }
        
        public int RemoveRingByCoordinates (int x, int y)
        {
            return Tiles[x, y].RemoveRing();
        }

        public int RemoveRingByIndex(int ringIndex)
        {
            var removedRingIndex = -1;
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (Tiles[x, y].RingIndex == ringIndex)
                    {
                        removedRingIndex = Tiles[x, y].RemoveRing();
                    }
                }
            }
            
            return removedRingIndex;
        }
        
        public void Deserialize()
        {
            for (var x = 0; x < 3; x++)
            {
                Tiles[0, x] = _upperTiles[x];
                Tiles[1, x] = _middleTiles[x];
                Tiles[2, x] = _lowerTiles[x];
            }
            
            foreach (var tile in Tiles)
            {
                tile.Deserialize();
            }
        }
        
        public object Clone()
        {
            var clone = new LevelLayout
            {
                Tiles = new TileData[3, 3]
            };

            for (var x = 0; x < 3; x++)
            {
                clone.Tiles[0, x] = Tiles[0, x].Clone();
                clone.Tiles[1, x] = Tiles[1, x].Clone();
                clone.Tiles[2, x] = Tiles[2, x].Clone();
            }
            
            return clone;
        }

        bool IsIndexPlaced(int ringIndex)
        {
            if (ringIndex < 0)
            {
                return false;
            }
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (Tiles[x, y].RingIndex == ringIndex)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}