using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PoolConfig poolConfig;

    private ObjectPooler _objectPool;
    private readonly float[] _spawnPos = {5.5f, 10.5f, 15.5f, 20.5f};
    private const float MainSpawnPos = 20.5f;
    private float _screenLimit;
    private float _counter;
    private const float Offset = 1.7f; //platform size

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
        List<Platform> list = new List<Platform>();
        foreach (var spawnPos in _spawnPos)
        {
            var position = new Vector3(Random.Range(_screenLimit + Offset, -_screenLimit - Offset), 0, spawnPos);
            var obj = _objectPool.SpawnFromPool(PoolObject.Platform, position, Quaternion.identity);
            list.Add(obj.GetComponent<Platform>());
        }

        return list;
    }

    public void SpawnParticle(PoolObject bonusParticle)
    {
        switch (bonusParticle)
        {
            case PoolObject.CoinCollectEffect:
                var coinObj = _objectPool.SpawnFromPool(bonusParticle, GetRandomPosEffect(), Quaternion.identity);
                StartCoroutine(DisableTimer(coinObj));
                break;
            case PoolObject.JumpCollectEffect:
                var jumpObj = _objectPool.SpawnFromPool(bonusParticle, GetRandomPosEffect(), Quaternion.identity);
                StartCoroutine(DisableTimer(jumpObj));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(bonusParticle), bonusParticle, null);
        }
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
        Random.Range(-_screenLimit, _screenLimit),
        transform.position.y,
        MainSpawnPos);

    private Vector3 GetRandomPosEffect() => new Vector3(
        Random.Range(-_screenLimit, _screenLimit),
        transform.position.y + (Offset * Random.Range(4, 10)),
        MainSpawnPos);

    private IEnumerator DisableTimer(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.SetActive(false);
    }
}