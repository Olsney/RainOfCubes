using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    private const float PositionY = 10;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    [SerializeField] private Platform _platform;
    [SerializeField] private Cube _cubePrefab;

    private float _randomPositionX;
    private float _randomPositionZ;
    private Coroutine _coroutine;
    private ObjectPool<Cube> _objectPool;

    private void Awake()
    {
        _platform.GetComponent<Platform>();
        _cubePrefab.GetComponent<Cube>();
        _objectPool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab), 
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: (cube) => Destroy(cube.gameObject)
            );
    }
    
    private void OnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.Destroyed -= Release;
    }

    private void OnGet(Cube cube)
    {
        cube.Init(GetRandomPosition());
        cube.Destroyed += Release;
    }

    private void Release(Cube cube)
    {
        _objectPool.Release(cube);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _coroutine == null)
            _coroutine = StartCoroutine(Spawn());
        
        if (Input.GetKeyDown(KeyCode.F) && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    
    private IEnumerator Spawn()
    {
        float delay = 1;
    
        var wait = new WaitForSeconds(delay);
        
        while (true)
        {
            _objectPool.Get();
    
            yield return wait;
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        float offsetCoefficientX = _platform.transform.localScale.x / 2;
        float offsetCoefficientZ = _platform.transform.localScale.z / 2;
        
        float randomPositionX = Random.Range(_platform.transform.position.x - offsetCoefficientX, _platform.transform.position.x + offsetCoefficientX);
        float randomPositionZ = Random.Range(- _platform.transform.position.z - offsetCoefficientZ, _platform.transform.position.z + offsetCoefficientZ);
    
        return new Vector3(randomPositionX, PositionY, randomPositionZ);
    }
}