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
        bool _isGoingBack;
        
        public T GetPresenter<T>() where T : IPresenter
        {
            if (_presenters.TryGetValue(typeof(T), out var presenter))
            {
                return (T)presenter;
            }

            var newPresenter = (T)Activator.CreateInstance(typeof(T));
            newPresenter.Initialize(this);
            _presenters.Add(typeof(T), newPresenter);
            return newPresenter;
        }
        
        public T LoadView<T>() where T : BasicView
        {
            var viewPrefab = _viewPrefabs.FindViewByType<T>();
            if (viewPrefab != null)
            {
                Instantiate(viewPrefab, _root.transform);
                viewPrefab.SetActive(false);
                return viewPrefab.GetComponent<T>();
            }

            Debug.LogError($"View prefab for {typeof(T)} not found");
            return null;
        }
        
        public T ShowView<T>(T component) where T : BasicView
        {
            component.gameObject.SetActive(true);
            
            if (!_isGoingBack)
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

            _isGoingBack = true;
            CurrentPresenter.HideWindow();
            _path.RemoveAt(_path.Count - 1);
            _path.LastOrDefault()?.ShowWindow();
        }
        
        public T GetMonoBehaviour<T>() where T : MonoBehaviour
        {
            return FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        }
    }
}