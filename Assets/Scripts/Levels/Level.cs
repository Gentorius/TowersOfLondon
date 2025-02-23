using System;
using Turns;

namespace Levels
{
    [Serializable]
    public class Level
    {
        public LevelLayout StartingLayout { get; internal set; }
        public LevelLayout GoalLayout { get; internal set; }
        public Solution Solution;
        
        public void OnRead()
        {
            StartingLayout.Deserialize();
            GoalLayout.Deserialize();
            Solution.Deserialize();
        }
        
        public void OnWrite()
        {
            StartingLayout.Deserialize();
            GoalLayout.Deserialize();
        }
    }
}