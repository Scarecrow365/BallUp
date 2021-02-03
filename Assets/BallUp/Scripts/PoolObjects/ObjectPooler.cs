using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    private Dictionary<PoolObject, Queue<GameObject>> _poolDictionary;

    public void Init()
    {
        _poolDictionary = new Dictionary<PoolObject, Queue<GameObject>>();
    }

    public void SetDataForWork(PoolObject poolObject,Queue<GameObject> queue)
    {
        _poolDictionary.Add(poolObject, queue);
    }

    public GameObject SpawnFromPool(PoolObject poolObject, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(poolObject))
        {
            Debug.LogWarning($"Pool with tag {poolObject} doesn`t exist");
            return null;
        }

        var objectToSpawn = _poolDictionary[poolObject].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        _poolDictionary[poolObject].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}