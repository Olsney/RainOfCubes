using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubeSpawner : SpawnerBase<Cube>
{
    private const float PositionY = 10;
   
    public event Action<Vector3> CubeDestroyed;

    private float _randomPositionX;
    private float _randomPositionZ;
    private Coroutine _coroutine;

    protected override void Release(Cube cube)
    {
        base.Release(cube);
        CubeDestroyed?.Invoke(cube.transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _coroutine == null)
            _coroutine = StartCoroutine(Spawning());
        
        if (Input.GetKeyDown(KeyCode.F) && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    
    private IEnumerator Spawning()
    {
        float delay = 0.5f;
    
        var wait = new WaitForSeconds(delay);
        
        while (true)
        {
            Spawn(DefinePosition());
    
            yield return wait;
        }
    }
    
    protected Vector3 DefinePosition()
    {
        float offsetCoefficientX = Platform.transform.localScale.x / 2;
        float offsetCoefficientZ = Platform.transform.localScale.z / 2;
        
        float randomPositionX = Random.Range(Platform.transform.position.x - offsetCoefficientX, Platform.transform.position.x + offsetCoefficientX);
        float randomPositionZ = Random.Range(- Platform.transform.position.z - offsetCoefficientZ, Platform.transform.position.z + offsetCoefficientZ);
    
        return new Vector3(randomPositionX, PositionY, randomPositionZ);
    }
}