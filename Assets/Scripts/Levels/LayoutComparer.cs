namespace Levels
{
    public static class LayoutComparer
    {
        public static bool Compare(LevelLayout layout1, LevelLayout layout2)
        {
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    if (layout1.Tiles[x, y].RingIndex != layout2.Tiles[x, y].RingIndex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}