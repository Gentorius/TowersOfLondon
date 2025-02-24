using System.IO;
using UnityEngine;

namespace Levels
{
    public class LevelReader
    {
        public static Level ReadLevelFromJson(string filePath)
        {
            if (!DoesFileExist(filePath))
            {
                Debug.LogError($"File does not exist at path: {filePath}");
                return null;
            }
            
            var json = File.ReadAllText(filePath);
            var level = JsonUtility.FromJson<Level>(json);
            level.OnRead();
            return level;
        }

        static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}