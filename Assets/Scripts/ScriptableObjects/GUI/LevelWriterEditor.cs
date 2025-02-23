using Levels;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.GUI
{
    [CustomEditor(typeof(LevelWriter))]
    public class LevelWriterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelWriter = (LevelWriter) target;
            if (GUILayout.Button("Write Level"))
            {
                levelWriter.WriteLevel();
            }
        }
    }
}