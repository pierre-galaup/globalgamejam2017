using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    /// <summary>
    /// One pooled object.
    /// </summary>
    [System.Serializable]
    public class PooledObject
    {
        /// <summary>
        /// The object pooled.
        /// </summary>
        public Object ObjectToPool;

        /// <summary>
        /// Initial number of object that the pool will instantiate.
        /// </summary>
        public int PooledAmount = 20;

        /// <summary>
        /// The pool can grow the pooled amount ?
        /// </summary>
        public bool CanGrow = true;
    }

    /// <summary>
    /// The pool that contains all the object instantiate during loading the scene.
    /// </summary>
    public class ObjectsPool : MonoBehaviour
    {
        /// <summary>
        /// Get the instance of ObjectsPool (static).
        /// </summary>
        public static ObjectsPool Current;

        /// <summary>
        /// Types of the differents objects that will be pooled.
        /// </summary>
        public PooledObject[] PooledObjectTypes;

        /// <summary>
        /// The objects pool.
        /// </summary>
        private Dictionary<string, List<GameObject>> _pool;

        /// <summary>
        /// Function call just after the constructor
        /// </summary>
        private void Awake()
        {
            if (Current == null)
            {
                Current = this;
            }
        }

        /// <summary>
        /// Function call one time after Awake and when the class is initialized.
        /// </summary>
        private void Start()
        {
            _pool = new Dictionary<string, List<GameObject>>();
            foreach (PooledObject pooledObjectType in PooledObjectTypes)
            {
                _pool.Add(pooledObjectType.ObjectToPool.name, new List<GameObject>());
                for (int i = 0; i < pooledObjectType.PooledAmount; i++)
                {
                    AddPooledObject(pooledObjectType.ObjectToPool);
                }
            }
        }

        /// <summary>
        /// Add an object to the ObjectPool.
        /// </summary>
        /// <param name="newObject">The object to add in the pool.</param>
        /// <returns>The new object instantied.</returns>
        private GameObject AddPooledObject(Object newObject)
        {
            GameObject pooledObject = Instantiate(newObject) as GameObject;
            if (pooledObject == null)
                return null;
            if (Current.gameObject.transform != null)
            {
                pooledObject.transform.parent = Current.gameObject.transform;
                if (pooledObject.GetComponent<ObjectInPool>() == null)
                    pooledObject.AddComponent<ObjectInPool>();
            }
            pooledObject.SetActive(false);
            _pool[newObject.name].Add(pooledObject);
            return pooledObject;
        }

        /// <summary>
        /// Get a wanted object in the pool.
        /// </summary>
        /// <param name="gameObjectWanted">The wanted object.</param>
        /// <returns>The wanted object or null if none is available.</returns>
        private List<GameObject> GetPooledObjects(Object gameObjectWanted)
        {
            return !_pool.ContainsKey(gameObjectWanted.name) ? null : _pool[gameObjectWanted.name];
        }

        /// <summary>
        /// Get a wanted object in the pool or add it if the object can grow.
        /// </summary>
        /// <param name="gameObjectWanted">The wanted object.</param>
        /// <returns>The wanted object or null if none is available and the object can't grow.</returns>
        public GameObject GetPooledObject(Object gameObjectWanted)
        {
            var pooledObjects = GetPooledObjects(gameObjectWanted);
            if (pooledObjects == null)
            {
                return (Instantiate(gameObjectWanted) as GameObject);
            }

            foreach (GameObject pooledObject in pooledObjects)
            {
                if (pooledObject != null && !pooledObject.activeInHierarchy)
                {
                    return pooledObject;
                }
            }

            foreach (PooledObject pooledObjectType in PooledObjectTypes)
            {
                if (pooledObjectType.ObjectToPool.name == gameObjectWanted.name)
                {
                    if (pooledObjectType.CanGrow)
                    {
                        return AddPooledObject(gameObjectWanted);
                    }
                    break;
                }
            }

            return null;
        }

        /// <summary>
        /// Disable all the objects in the ObjectsPool.
        /// </summary>
        public void DisableAllPooledObjects()
        {
            foreach (string key in _pool.Keys)
            {
                foreach (GameObject pooledObject in _pool[key])
                {
                    pooledObject.SetActive(false);
                }
            }
        }
    }
}