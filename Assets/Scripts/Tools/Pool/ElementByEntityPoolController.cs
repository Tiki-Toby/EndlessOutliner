using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.Ui
{
    public class ElementByEntityPoolController<TKey, TViewValue> : IEnumerable<KeyValuePair<TKey, TViewValue>>, IDisposable 
        where TViewValue : Component
    {
        #region Fields

        private Transform _parentTransform;
        private TViewValue _viewElementPrefab;

        private readonly List<TViewValue> _elementPool;
        private readonly Dictionary<TKey, TViewValue> _elementViews;

        public int Count => _elementViews.Count;

        #endregion

        #region Class lifecycle

        public ElementByEntityPoolController()
        {
            _elementPool = new List<TViewValue>();
            _elementViews = new Dictionary<TKey, TViewValue>();
        }

        public void SetParent(Transform parentTransform, TViewValue windowSlotPrefab)
        {
            if (_parentTransform == parentTransform)
                return;

            _viewElementPrefab = windowSlotPrefab;

            _elementPool.Clear();
            _elementViews.Clear();

            _parentTransform = parentTransform;

            foreach (TViewValue element in _parentTransform.GetComponentsInChildren<TViewValue>())
            {
                if(element.transform != parentTransform)
                    _elementPool.Add(element);
            }
        }

        public TViewValue AddElement(TKey key)
        {
            TViewValue element = GetElementView();
            _elementViews.Add(key, element);

            return element;
        }

        public void ClearViews()
        {
            foreach (var elementEntityViewPair in _elementViews)
            {
                elementEntityViewPair.Value.gameObject.SetActive(false);
            }
            
            _elementViews.Clear();
        }

        public TViewValue AddEmptySlot()
        {
            var element = Object.Instantiate(_viewElementPrefab, _parentTransform);
            _elementPool.Add(element);
            return element;
        }

        public TViewValue ReleaseSlot(TKey key)
        {
            var element = _elementViews[key];
            _elementViews.Remove(key);
            element.transform.SetSiblingIndex(_elementPool.Count - 1);
            element.gameObject.SetActive(false);
            
            _elementPool.Remove(element);
            _elementPool.Add(element);

            return element;
        }

        public TViewValue GetElementView()
        {
            TViewValue element;
            if (_elementPool.Count > _elementViews.Count)
            {
                element = _elementPool[_elementViews.Count];
                element.gameObject.SetActive(true);
            }
            else
            {
                element = AddEmptySlot();
            }

            return element;
        }

        public bool TryGetElement(TKey key, out TViewValue elementView)
        {
            return _elementViews.TryGetValue(key, out elementView);
        }

        public void Dispose()
        {
            foreach (var element in _elementPool)
            {
                Object.Destroy(element.gameObject);
            }

            _elementPool.Clear();
            _elementViews.Clear();
        }

        #endregion

        public IEnumerator<KeyValuePair<TKey, TViewValue>> GetEnumerator()
        {
            return _elementViews.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}