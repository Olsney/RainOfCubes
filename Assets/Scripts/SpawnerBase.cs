using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerBase<T> : MonoBehaviour where T : SpawnableObject<T>
{
    private int _allCubesInHistory;
    private ObjectPool<T> _objectPool;
    
    public event Action<int, int, int, string> ValueChanged;
    
    [field: SerializeField] protected Platform Platform { get; private set; }
    [field: SerializeField] protected T Prefab { get; private set; }
    
    protected abstract string SpawnerName { get; }
    
    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: (T) => Destroy(T.gameObject)
        );

        ValueChanged?.Invoke(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
    }
    
    public void Spawn(Vector3 position)
    {
        _objectPool.Get().transform.position = position;
        ValueChanged?.Invoke(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
    }

    private void OnRelease(T cube)
    {
        cube.gameObject.SetActive(false);
        cube.Destroyed -= Release; 
        ValueChanged?.Invoke(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
    }

    private void OnGet(T spawnableObject)
    {
        _allCubesInHistory++;
        
        spawnableObject.Init();
        spawnableObject.Destroyed += Release;
        ValueChanged?.Invoke(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
    }
    
    protected virtual void Release(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
        ValueChanged?.Invoke(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
    }
}