using System.Linq;
using UI;
using UI.Basics;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ViewPrefabs", menuName = "Scriptable Objects/ViewPrefabs", order = 1)]
    public class ViewPrefabs : ScriptableObject
    {
        [SerializeField]
        GameObject[] _viewPrefabs;
        
        public GameObject FindViewByType<T>() where T : BasicView
        {
            return _viewPrefabs.FirstOrDefault(prefab => prefab.GetComponent<T>() != null);
        }
    }
}