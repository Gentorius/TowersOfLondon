using System;
using Turns;

namespace Levels
{
    [Serializable]
    public class Level
    {
        public LevelLayout StartingLayout;
        public LevelLayout GoalLayout;
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