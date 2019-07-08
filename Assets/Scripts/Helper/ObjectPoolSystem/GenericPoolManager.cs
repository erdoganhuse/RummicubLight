using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Helper.ObjectPoolSystem
{
    public class GenericPoolManager<T> where T : MonoBehaviour
    {
        #region Variables

        private const int DefaultPoolSize = 5;
        private const int DefaultIncreaseAmount = 3;

        private readonly T _prefab;
        private readonly Transform _container;

        private readonly Queue<T> _availableItems;
        private readonly List<T> _busyItems;

        public int Capacity => _availableItems.Count;

        #endregion

        #region Class Methods

        public GenericPoolManager(T prefab, Transform container, int capacity = DefaultPoolSize)
        {
            _prefab = prefab;
            _container = container;

            _availableItems = new Queue<T>();
            _busyItems = new List<T>();

            Create(capacity);
        }

        public void Create(int capacity)
        {
            for (int i = 0; i < capacity; i++)
            {
                T instantiatedItem = UnityEngine.Object.Instantiate(_prefab, _container) as T;
                instantiatedItem.gameObject.SetActive(false);
                _availableItems.Enqueue(instantiatedItem);
            }
        }

        public void IncreaseCapacity(int increaseAmount)
        {
            for (int i = 0; i < increaseAmount; i++)
            {
                T instantiatedItem = Object.Instantiate(_prefab, _container) as T;
                instantiatedItem.gameObject.SetActive(false);
                _availableItems.Enqueue(instantiatedItem);
            }
        }

        public T Spawn()
        {
            return Spawn(_container, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public T Spawn(Transform parent)
        {
            return Spawn(parent, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        public T Spawn(Vector3 position)
        {
            return Spawn(_container, position, Quaternion.identity, Vector3.one);
        }

        public T Spawn(Vector3 position, Vector3 scale)
        {
            return Spawn(_container, position, Quaternion.identity, scale);
        }

        public T Spawn(Transform parent, Vector3 position)
        {
            return Spawn(parent, position, Quaternion.identity, Vector3.one);
        }

        public T Spawn(Vector3 position, Quaternion rotation)
        {
            return Spawn(_container, position, rotation, Vector3.one);
        }

        public T Spawn(Transform parent, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Assert.IsTrue(_availableItems != null);

            if (Capacity <= 0)
            {
                IncreaseCapacity(DefaultIncreaseAmount);
            }

            T item = _availableItems.Dequeue();
            item.transform.SetParent(parent, false);
            item.transform.localPosition = position;
            item.transform.localRotation = rotation;
            item.transform.localScale = scale;
                
            _busyItems.Add(item);

            return item;
        }

        public void Recycle(T item)
        {
            Assert.IsTrue(item != null);

            item.gameObject.SetActive(false);
            item.transform.SetParent(_container);

            if (_busyItems.Contains(item)) _busyItems.Remove(item);

            _availableItems.Enqueue(item);
        }

        #endregion
    }
}
