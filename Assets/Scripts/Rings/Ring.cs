using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rings
{
    public class Ring : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Ring> OnRingClicked;
        public int X { get;  private set;}
        public int Y { get; private set; }
        bool _isDragging;
        Camera _camera;

        void Start()
        {
            _camera = Camera.main;
            
            if (_camera == null)
            {
                Debug.LogError("Main camera not found!");
            }
        }

        void Update()
        {
            if (!_isDragging)
                return;

            var mousePosition = Input.mousePosition;
            mousePosition.z = _camera.nearClipPlane;
            var worldPosition = _camera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
        
        public void SetRingPrefab(GameObject ringPrefab)
        {
            var ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
            ring.transform.SetParent(transform);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnRingClicked?.Invoke(this);
        }
        
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public void RemovePosition()
        {
            X = -1;
            Y = -1;
        }
    }
}
