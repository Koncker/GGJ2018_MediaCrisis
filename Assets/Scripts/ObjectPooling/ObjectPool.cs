using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util.ObjectPooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Transform _objectPoolContainer;
        private Transform ObjectPoolContainer
        {
            get
            {
                if (_objectPoolContainer == null)
                {
                    var gameObjectContainer = new GameObject(typeof(T).Name + " Pool", new Type[] { typeof(ObjectPoolContainer) });
                    UnityEngine.Object.DontDestroyOnLoad(gameObjectContainer);
                    _objectPoolContainer = gameObjectContainer.transform;
                }
                return _objectPoolContainer;
            }
        }


        private Stack<T> _pool = new Stack<T>();

        public void PoolObjects(int objectNumber, T objectPrefab)
        {
            for (int i = _pool.Count; i < objectNumber; i++)
            {
                var gameObject = UnityEngine.Object.Instantiate(objectPrefab, ObjectPoolContainer) as T;
                gameObject.gameObject.SetActive(false);
                _pool.Push(gameObject);
            }
        }

        public T GetObject(T objectPrefab)
        {
            if (_pool.Count == 0)
            {
                var gameObject = UnityEngine.Object.Instantiate(objectPrefab, ObjectPoolContainer) as T;
                return gameObject;
            }
            else
            {
                var gameObject = _pool.Pop();
                gameObject.gameObject.SetActive(true);
                return gameObject;
            }
        }

        public void DestroyObject(T gameObject)
        {
            if (gameObject != null)
            {
                gameObject.transform.SetParent(ObjectPoolContainer, false);
                gameObject.gameObject.SetActive(false);
                _pool.Push(gameObject);
            }
        }
    }
}