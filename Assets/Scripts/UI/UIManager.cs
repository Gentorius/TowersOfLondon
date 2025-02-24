using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UI.Basics;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        ViewPrefabs _viewPrefabs;
        [SerializeField]
        GameObject _root;

        readonly Dictionary<Type, IPresenter> _presenters = new();
        readonly List<IPresenter> _path = new();
        IPresenter CurrentPresenter => _presenters.FirstOrDefault(x => x.Value.IsShown).Value;
        
        public T GetPresenter<T>() where T : class, IPresenter, new()
        {
            if (_presenters.TryGetValue(typeof(T), out var presenter))
            {
                return (T)presenter;
            }

            var newPresenter = new T();
            _presenters.Add(typeof(T), newPresenter);
            newPresenter.Initialize(this);
            return newPresenter;
        }
        
        public T LoadView<T>() where T : BasicView
        {
            var viewPrefab = _viewPrefabs.FindViewByType<T>();
            if (viewPrefab != null)
            {
                var instance = Instantiate(viewPrefab, _root.transform);
                instance.SetActive(false);
                return instance.GetComponent<T>();
            }

            Debug.LogError($"View prefab for {typeof(T)} not found");
            return null;
        }
        
        public T ShowView<T>(T component) where T : BasicView
        {
            component.gameObject.SetActive(true);
            
            _path.Add(CurrentPresenter);
            
            return component;
        }
        
        public static void HideView(BasicView view)
        {
            view.gameObject.SetActive(false);
        }
        
        public static void DestroyView(BasicView view)
        {
            Destroy(view.gameObject);
        }

        public void GoBack()
        {
            if (_path.Count == 0)
            {
                return;
            }
            
            CurrentPresenter.HideWindow();
            _path[^2].ShowWindow();
        }
        
        public static T GetMonoBehaviour<T>() where T : MonoBehaviour
        {
            return FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        }
    }
}