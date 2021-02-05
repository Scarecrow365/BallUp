using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PoolConfig poolConfig;

    private ObjectPooler _objectPool;
    private readonly float[] _spawnPos = {5.5f, 10.5f, 15.5f, 20.5f};
    private const float MainSpawnPos = 20.5f;
    private float _screenLimit;
    private float _counter;
    private const float Offset = 0.85f; // half size platform

    public void Init()
    {
        _objectPool = new ObjectPooler();
        _objectPool.Init();
        InitPool();
        SetBorder();
    }

    private void InitPool()
    {
        foreach (var pool in poolConfig.pools)
        {
            var objects = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }

            _objectPool.SetDataForWork(pool.poolObject, objects);
        }
    }

    private void SetBorder()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            _screenLimit = 1;
            return;
        }

        var zDistance = mainCamera.transform.position.z - transform.position.z;
        _screenLimit = mainCamera.ScreenToWorldPoint(new Vector3(0, 0,
            -zDistance / Mathf.Cos(mainCamera.transform.localEulerAngles.x * Mathf.Deg2Rad))).x;
    }

    public List<Platform> InitStartPlatforms()
    {
        return (
            from spawnPos 
            in _spawnPos 
            select GetRandomPosPlatform()
            into position 
            select _objectPool.SpawnFromPool(PoolObject.Platform, position, Quaternion.identity)
            into obj select obj.GetComponent<Platform>()).ToList();
    }

    public Platform SpawnPlatform()
    {
        if (_counter > Random.Range(1, 5))
        {
            var obj = _objectPool.SpawnFromPool(PoolObject.RichPlatform, GetRandomPosPlatform(), Quaternion.identity);
            obj.transform.GetChild(0).gameObject.SetActive(true);
            _counter = 0;
            return obj.GetComponent<Platform>();
        }
        else
        {
            var obj = _objectPool.SpawnFromPool(PoolObject.Platform, GetRandomPosPlatform(), Quaternion.identity);
            _counter += 1;
            return obj.GetComponent<Platform>();
        }
    }

    private Vector3 GetRandomPosPlatform() => new Vector3(
        Random.Range(-_screenLimit+Offset, _screenLimit - Offset),
        transform.position.y,
        MainSpawnPos);
}