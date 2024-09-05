using System;
using System.Collections;
using System.Collections.Generic;
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
    private Vector3 _velocity = new Vector3(0f, -5f, 0f);
    private Coroutine _coroutine;
    // private ObjectPool<Cube> _objectPool;

    // private void Awake()
    // {
    //     _platform.GetComponent<Platform>();
    //     _cubePrefab.GetComponent<Cube>();
    //     _objectPool = new ObjectPool<Cube>(
    //         createFunc: () => Instantiate(_cubePrefab), 
    //         actionOnGet: OnGet,
    //         actionOnRelease: OnRelease
    //         );
    // }
    //
    // private void OnRelease(Cube cube)
    // {
    //     _objectPool.Release(cube);
    //
    //     new WaitForSeconds(GetRandomDelay());
    //     
    //     cube.gameObject.SetActive(false);
    //     _objectPool.Release(cube);
    //     Destroy(cube);
    //     
    // }

    // private void OnGet(Cube cube)
    // {
    //     cube.transform.position = SetRandomPosition();
    //     cube.GetComponent<Rigidbody>().velocity = _velocity;
    //     cube.gameObject.SetActive(true);
    //     _pool.Get();
    // }

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
            SetRandomPoint();
    
            Instantiate(_cubePrefab, new Vector3(_randomPositionX, PositionY, _randomPositionZ), Quaternion.identity);
    
            yield return wait;
        }
    }
    
    private int GetRandomDelay()
    {
        int min = 2;
        int max = 5;

        return Random.Range(min, max);
    }
    
    private void SetRandomPoint()
    {
        //Почему создается за пределами платформы?
        _randomPositionX = Random.Range(0, _platform.transform.localScale.x);
        _randomPositionZ = Random.Range(0, _platform.transform.localScale.z);
    }

    // private Vector3 SetRandomPosition()
    // {
    //     //Почему создается за пределами платформы?
    //     _randomPositionX = Random.Range(0, _platform.transform.localScale.x);
    //     _randomPositionZ = Random.Range(0, _platform.transform.localScale.z);
    //
    //     return new Vector3(_randomPositionX, PositionY, _randomPositionZ);
    // }
}