using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public abstract class SpawnerBase<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [field: SerializeField] protected Platform Platform { get; private set; }
    [field: SerializeField] protected T Prefab { get; private set; }
    
    private ObjectPool<T> _objectPool;

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: (T) => Destroy(T.gameObject)
        );
    }
    
    public void Spawn(Vector3 position)
    {
        _objectPool.Get().transform.position = position;
    }

    private void OnRelease(T cube)
    {
        cube.gameObject.SetActive(false);
        cube.Destroyed -= Release;
    }

    private void OnGet(T spawnableObject)
    {
        spawnableObject.Init();
        spawnableObject.Destroyed += Release;
    }
    
    protected virtual void Release(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
    }
}