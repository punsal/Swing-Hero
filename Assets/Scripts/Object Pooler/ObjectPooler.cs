using System;
using System.Collections.Generic;
using Assets.Scripts.Object_Pooler;
using UnityEngine;
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ParameterHidesMember

namespace Object_Pooler
{
    public class ObjectPooler : MonoBehaviour
    {
        /// <summary>
        /// Use SharedInstance to cache or access pooler.
        /// </summary>
        public static ObjectPooler SharedInstance;

        [SerializeField] private List<GameObject> pooledObjects;
        [SerializeField] private List<ObjectPoolItem> itemsToPool;

        private void Awake()
        {
            SharedInstance = this;
        }

        public void Pool() 
        {
            pooledObjects = new List<GameObject>();
            foreach (var item in itemsToPool)
            {
                for (var i = 0; i < item.amountToPool; i++)
                {
                    var obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
            }
        }

        public bool IsActiveObjectInScene(string tag = "default")
        {
            if (tag == "default")
            {
                for (var i = 0; i < pooledObjects.Count; i++)
                {
                    if (pooledObjects[i].activeInHierarchy)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (var i = 0; i < pooledObjects.Count; i++)
                {
                    if (pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GameObject GetPooledObject(string tag)
        {
            for (var i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
                {
                    return pooledObjects[i];
                }
            }
            foreach (var item in itemsToPool)
            {
                if (item.objectToPool.CompareTag(tag))
                {
                    if (item.poolingType == PoolingType.Expandable)
                    {
                        var obj = Instantiate(item.objectToPool);
                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                        return obj;
                    }
                }
            }
        
            throw new Exception($"{tag} Pooler is stuck.");
        }

        public int GetActiveObjectCount(string tag = "default")
        {
            var count = 0;
            if (tag == "default")
            {
                for (var i = 0; i < pooledObjects.Count; i++)
                {
                    if (pooledObjects[i].activeInHierarchy)
                    {
                        count++;
                    }
                }

                return count;
            }
            
            for (var i = 0; i < pooledObjects.Count; i++)
            {
                try
                {
                    if (pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
                    {
                        count++;
                    }
                }
                catch (Exception e)
                {
                    foreach (var objectPoolItem in itemsToPool)
                    {
                        var minorCount = 0;
                        for (var index = 0; index < pooledObjects.Count; index++)
                        {
                            try
                            {
                                var isActive = pooledObjects[index].activeInHierarchy;
                                var isObject = pooledObjects[index].CompareTag(objectPoolItem.objectToPool.tag);
                                
                                if (isActive && isObject)
                                {
                                    minorCount++;
                                }
                            }
                            catch (Exception exception)
                            {
                                pooledObjects[index] = Instantiate(objectPoolItem.objectToPool);
                                pooledObjects[index].SetActive(false);
                                Debug.Log(exception.Message);
                            }
                        }

                        Debug.Log($"Name : {objectPoolItem.objectToPool.name}\nCount : {objectPoolItem.amountToPool}\nActives : {minorCount}");
                    }
                    Debug.Log("Tag : " + tag + "\n" + e.Message);
                }
            }

            return count;
        }
    }
}