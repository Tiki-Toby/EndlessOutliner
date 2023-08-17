using System;
using System.Collections;
using System.Collections.Generic;
using Tools.Ui.Fabric;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.Ui
{
    public class ObjectControllerPool<TController, TView, TKey> : IEnumerable<KeyValuePair<TKey, TController>>,
        IDisposable
        where TController : IPoolableElementController<TKey, TView> 
        where TView : Component
    {
        #region Fields

        private Transform _parentTransform;
        private IControllerFabric<TController, TKey, TView> _fabric;

        private readonly Stack<TController> _pooledElements;
        private readonly Dictionary<TKey, TController> _activeElements;

        public int Count => _activeElements.Count;

        #endregion

        #region Class lifecycle

        public ObjectControllerPool(IControllerFabric<TController, TKey, TView> fabric)
        {
            _fabric = fabric;
            _pooledElements = new Stack<TController>();
            _activeElements = new Dictionary<TKey, TController>();
        }

        public void SetParent(Transform parentTransform)
        {
            if (_parentTransform == parentTransform)
                return;

            _parentTransform = parentTransform;

            Dispose();

            foreach (TView elementView in _parentTransform.GetComponentsInChildren<TView>())
            {
                TController controller = _fabric.CreateController();
                controller.SetView(elementView);
                _pooledElements.Push(controller);
            }
        }

        public void ReleaseSlot(TKey pinedElement)
        {
            var element = _activeElements[pinedElement];
            element.Close();

            element.Transform.gameObject.SetActive(false);
            
            _activeElements.Remove(pinedElement);
            element.Transform.SetAsLastSibling();

            _pooledElements.Push(element);
            _activeElements.Remove(pinedElement);
        }

        public void CloseAll()
        {
            foreach (var activeElementController in _activeElements.Values)
            {
                activeElementController.Transform.SetAsLastSibling();
                _pooledElements.Push(activeElementController);
                activeElementController.Close();
            }

            _activeElements.Clear();
        }

        public TController GetFreeElementController(TKey pinedElement)
        {
            if (!_pooledElements.TryPop(out TController elementController))
            {
                elementController = _fabric.CreateController();
            }

            elementController.Transform.gameObject.SetActive(true);

            elementController.Open(pinedElement);
            _activeElements.Add(pinedElement, elementController);
            return elementController;
        }

        public bool TryGetElement(TKey pinedElement, out TController elementController)
        {
            return _activeElements.TryGetValue(pinedElement, out elementController);
        }

        public void Dispose()
        {
            foreach (var slot in _pooledElements)
            {
                slot.Dispose();
            }

            foreach (var slot in _activeElements.Values)
            {
                slot.Dispose();
            }

            _pooledElements.Clear();
            _activeElements.Clear();
        }

        #endregion

        public IEnumerator<KeyValuePair<TKey, TController>> GetEnumerator()
        {
            return _activeElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}