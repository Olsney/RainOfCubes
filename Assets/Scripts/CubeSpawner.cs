using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : SpawnerBase<Cube>
{
    private const float PositionY = 10;
    private const KeyCode SpawnKey = KeyCode.Space;
    private const KeyCode StopSpawnKey = KeyCode.F;
    
    private float _randomPositionX;
    private float _randomPositionZ;
    private Coroutine _coroutine;
    private bool _isSpawning = false;
    
    public event Action<Vector3> CubeDestroyed;
    
    protected override string Name => "CubeSpawner";

    private void Update()
    {
        HandleSpawnKey();
        HandleStopSpawnKey();
    }

    private void HandleSpawnKey()
    {
        if (Input.GetKeyDown(SpawnKey) && _isSpawning == false)
        {
            _coroutine = StartCoroutine(Spawning()); 
            _isSpawning = true;
        }
    }
    
    private void HandleStopSpawnKey()
    {
        if (Input.GetKeyDown(StopSpawnKey) && _isSpawning)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            _isSpawning = false;
        }
    }
    
    protected override void Release(Cube cube)
    {
        base.Release(cube);
        CubeDestroyed?.Invoke(cube.transform.position);
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

    private Vector3 DefinePosition()
    {
        float offsetCoefficientX = Platform.transform.localScale.x / 2;
        float offsetCoefficientZ = Platform.transform.localScale.z / 2;
        
        float randomPositionX = Random.Range(Platform.transform.position.x - offsetCoefficientX, Platform.transform.position.x + offsetCoefficientX);
        float randomPositionZ = Random.Range(- Platform.transform.position.z - offsetCoefficientZ, Platform.transform.position.z + offsetCoefficientZ);
    
        return new Vector3(randomPositionX, PositionY, randomPositionZ);
    }
}