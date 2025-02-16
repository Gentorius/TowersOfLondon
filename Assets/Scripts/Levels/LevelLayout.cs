using System;
using Rings;

namespace Levels
{
    [Serializable]
    public class LevelLayout
    {
        TileData[,] _tiles = new TileData[3, 3];

        public bool TryPlaceRingByColumn(int column, Ring ring)
        {
            for (var row = 2; row >= 0; row--)
            {
                if (_tiles[row, column].IsOccupied)
                    continue;

                _tiles[row, column].SetRing(ring);
                return true;
            }

            return false;
        }

        public bool TryTakeRingByCoordinates(int row, int column, out Ring ring)
        {
            if (!_tiles[row, column].IsOccupied)
            {
                ring = null;
                return false;
            }

            ring = _tiles[row, column].RemoveRing();
            return true;
        }
    }
}