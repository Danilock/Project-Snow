using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

namespace ObjectPooling
{
    [DictionaryDrawerSettings(KeyLabel = "Pool Key", ValueLabel = "Pool")]
    public class ObjectPooler : PersistantSerializedSingleton<ObjectPooler>
    {
        [SerializeField] private Dictionary<string, Pool> Pools = new Dictionary<string, Pool>();

        private void Start()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (Pool currentPool in Pools.Values)
            {
                currentPool.InitializePool();
            }
        }

        /// <summary>
        /// Gets an object from the specified pool.
        /// </summary>
        /// <param name="poolKey"></param>
        /// <returns></returns>
        public GameObject GetObjectFromPool(string poolKey)
        {
            return Pools[poolKey].GetObject();
        }
    }
}