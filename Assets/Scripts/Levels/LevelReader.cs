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
            return JsonUtility.FromJson<Level>(json);
        }
        
        bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}