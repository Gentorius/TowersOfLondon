using System.Linq;
using UI;
using UI.Basics;
using UnityEngine;

namespace ScriptableObjects
{
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