using System;
using Turns;

namespace Levels
{
    [Serializable]
    public class Level
    {
        public LevelLayout StartingLayout { get; private set; }
        public LevelLayout GoalLayout { get; private set; }
        public Solution Solution { get; private set; }
    }
}