using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RingPrefabs", menuName = "Scriptable Objects/RingPrefabs")]
    public class RingPrefabs : ScriptableObject
    {
        [SerializeField]
        GameObject[] _ringPrefabList;
        
        public GameObject[] RingPrefabList => _ringPrefabList;
    
        public bool IsEmpty => _ringPrefabList == null || _ringPrefabList.Length == 0;
    }
}
