using UnityEngine;

namespace Rings
{
    [CreateAssetMenu(fileName = "RingPrefabs", menuName = "Scriptable Objects/RingPrefabs")]
    public class RingPrefabs : ScriptableObject
    {
        public GameObject[] ringPrefabs;
    
        public bool IsEmpty => ringPrefabs == null || ringPrefabs.Length == 0;
    }
}
