using System.Collections.Generic;

namespace Levels
{
    public static class LevelValidator
    {
        public static bool IsLayoutValid(LevelLayout levelLayout)
        {
            if (DoesLayoutHaveDuplicateRings(levelLayout))
                return false;

            return !DoRingsHaveVerticalGaps(levelLayout);
        }

        static bool DoRingsHaveVerticalGaps(LevelLayout levelLayout)
        {
            for (var x = 0; x < 3; x++)
            {
                if (levelLayout.Tiles[x, 0].IsOccupied && !levelLayout.Tiles[x, 1].IsOccupied)
                {
                    return true;
                }
                
                if (levelLayout.Tiles[x, 1].IsOccupied && !levelLayout.Tiles[x, 2].IsOccupied)
                {
                    return true;
                }
            }
            
            return false;
        }

        static bool DoesLayoutHaveDuplicateRings(LevelLayout levelLayout)
        {
            List<int> ringIndexes = new();
            
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (!levelLayout.Tiles[x, y].IsOccupied)
                    {
                        continue;
                    }
                    
                    var ringIndex = levelLayout.Tiles[x, y].RingIndex;
                    
                    if (ringIndexes.Contains(ringIndex))
                    {
                        return true;
                    }
                    
                    ringIndexes.Add(ringIndex);
                }
            }
            
            return false;
        }
    }
}