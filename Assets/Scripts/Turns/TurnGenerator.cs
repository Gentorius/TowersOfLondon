using System.Collections.Generic;
using System.Numerics;
using Levels;

namespace Turns
{
    public class TurnGenerator : IRandomTurnGenerator
    {
        public void TakeARandomTurn(ref LevelLayout currentLayout, ref Solution solution)
        {
            var topRings = GetTopRings(currentLayout);
            var selectedRing = topRings[UnityEngine.Random.Range(0, topRings.Count)];
            currentLayout.RemoveRingByIndex(selectedRing);
            var lowestEmptyTiles = GetLowestEmptyTiles(currentLayout);
            var selectedTile = lowestEmptyTiles[UnityEngine.Random.Range(0, lowestEmptyTiles.Count)];
            currentLayout.TryPlaceRingByCoordinates(selectedRing, (int)selectedTile.X, (int)selectedTile.Y);
            solution.MoveRing(selectedRing, (int)selectedTile.X, (int)selectedTile.Y);
        }
        
        static List<int> GetTopRings(LevelLayout currentLayout)
        {
            var topRings = new List<int>();
            var tiles = currentLayout.Tiles;
            for (var row = 0; row < 3; row++)
            {
                for (var column = 0; column < 3; column++)
                {
                    if (!tiles[row, column].IsOccupied)
                        continue;

                    if (row == 0 || !tiles[row - 1, column].IsOccupied)
                    {
                        topRings.Add(tiles[row, column].RingIndex);
                    }
                }
            }
            
            return topRings;
        }
        
        static List<Vector2> GetLowestEmptyTiles(LevelLayout currentLayout)
        {
            var lowestEmptyTiles = new List<Vector2>();
            var tiles = currentLayout.Tiles;
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (!tiles[x, y].IsOccupied && (x == 2 || tiles[x + 1, y].IsOccupied))
                    {
                        lowestEmptyTiles.Add(new Vector2(x, y));
                    }
                }
            }
            
            return lowestEmptyTiles;
        }
    }
}