using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerBase<T> : MonoBehaviour where T : SpawnableObject<T>
{
    private int _allCubesInHistory;
    private ObjectPool<T> _objectPool;
    
    public event Action<SpawnerInfoKeeper> ValueChanged;
    
    [field: SerializeField] protected Platform Platform { get; private set; }
    [field: SerializeField] protected T Prefab { get; private set; }
    
    protected abstract string Name { get; }
    
    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: (T) => Destroy(T.gameObject)
        );
    }

    private void Start()
    {
        ValueChanged.Invoke(SaveInfo());

    }
    
    public void Spawn(Vector3 position)
    {
        _objectPool.Get().transform.position = position;
        ValueChanged.Invoke(SaveInfo());
    }

    private void OnRelease(T cube)
    {
        cube.gameObject.SetActive(false);
        cube.Destroyed -= Release; 
        ValueChanged.Invoke(SaveInfo());
    }

    private void OnGet(T spawnableObject)
    {
        _allCubesInHistory++;
        
        spawnableObject.Init();
        spawnableObject.Destroyed += Release;
        ValueChanged.Invoke(SaveInfo());
    }
    
    protected virtual void Release(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
        ValueChanged.Invoke(SaveInfo());
    }

    private SpawnerInfoKeeper SaveInfo() =>
        new SpawnerInfoKeeper(
            _allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive,
            Name);
}