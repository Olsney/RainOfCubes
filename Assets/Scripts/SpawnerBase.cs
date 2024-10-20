using DefaultNamespace;
using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerBase<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [field: SerializeField] protected Platform Platform { get; private set; }
    [field: SerializeField] protected T Prefab { get; private set; }
    [field: SerializeField] protected TextInfo TextInfo { get; private set; }
    
    private ObjectPool<T> _objectPool;
    
    protected abstract string SpawnerName { get; }

    private int _allCubesInHistory;
    
    private void Awake()
    {
        _objectPool = new ObjectPool<T>(
            createFunc: () => Instantiate(Prefab),
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: (T) => Destroy(T.gameObject)
        );
        
        UpdateTextInfo();
    }
    
    
    public void Spawn(Vector3 position)
    {
        _objectPool.Get().transform.position = position;
        UpdateTextInfo();
    }

    private void OnRelease(T cube)
    {
        cube.gameObject.SetActive(false);
        cube.Destroyed -= Release; 
        UpdateTextInfo();
    }

    private void OnGet(T spawnableObject)
    {
        _allCubesInHistory++;
        
        spawnableObject.Init();
        spawnableObject.Destroyed += Release;
        UpdateTextInfo();
    }
    
    protected virtual void Release(T spawnableObject)
    {
        _objectPool.Release(spawnableObject);
        UpdateTextInfo();
    }

    private void UpdateTextInfo() => 
        TextInfo.SetText(_allCubesInHistory, _objectPool.CountInactive + _objectPool.CountActive, _objectPool.CountActive, SpawnerName);
}