using System;
using Rings;

namespace Levels
{
    [Serializable]
    public class LevelLayout
    {
        public TileData[,] Tiles { get; private set; } = new TileData[3, 3];

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