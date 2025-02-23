using System.IO;
using UnityEngine;

namespace Levels
{
    public class LevelReader
    {
        public Level ReadLevelFromJson(string filePath)
        {
            if (!DoesFileExist(filePath))
            {
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