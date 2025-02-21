using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RingPrefabs", menuName = "Scriptable Objects/RingPrefabs")]
    public class RingPrefabs : ScriptableObject
    {
        public GameObject[] RingPrefabList;
    
        public bool IsEmpty => RingPrefabList == null || RingPrefabList.Length == 0;
    }
}
