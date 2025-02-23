using Turns;
using Unity.Collections;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Level Writer", menuName = "Scriptable Objects/LevelWriter")]
    public class LevelWriter : ScriptableObject
    {
        [SerializeField]
        string _levelName;
        [SerializeField]
        LevelLayout _targetLevelLayout;
        [SerializeField]
        LevelLayout _goalLevelLayout;
        [SerializeField][ReadOnly]
        Solution _solution;
        
        readonly SolutionFinder _solutionFinder = new();
        Level _generatedLevel;
        static string LevelDataPath => Application.dataPath + "/LevelData/TowerOfLondon-";
        static string LevelDataExtension => ".json";
        string LevelPath => LevelDataPath + _levelName + LevelDataExtension;
        
        
        public void WriteLevel()
        {
            _generatedLevel = new Level
            {
                StartingLayout = _targetLevelLayout,
                GoalLayout = _goalLevelLayout
            };
            
            _generatedLevel.OnWrite();
            _generatedLevel.Solution = _solutionFinder.FindBestSolution(_generatedLevel.StartingLayout, _generatedLevel.GoalLayout, out _);
            _generatedLevel.Solution.Serialize();
            _solution = _generatedLevel.Solution;
            
            var levelJson = JsonUtility.ToJson(_generatedLevel);
            System.IO.File.WriteAllText(LevelPath, levelJson);
            Debug.Log($"Level {_levelName} written to {LevelPath}");
        }
    }
}