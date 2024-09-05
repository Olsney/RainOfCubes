using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    // public event Action Destroyed;
    
    private const string PlatformTag = "Platform";
    
    private bool _isPlatformTouched;

    private void Update()
    {
        if (_isPlatformTouched)
            // Destroyed?.Invoke();
        Destroy(gameObject,GetRandomDelay());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformTouched)
            return;

        if (collision.gameObject.tag != PlatformTag)
            return;
        
        GetComponent<Renderer>().material.color = GetRandomColor();
        _isPlatformTouched = true;
    }
    
    private int GetRandomDelay()
    {
        int min = 2;
        int max = 5;

        return Random.Range(min, max);
    }

    private Color GetRandomColor() => 
        Random.ColorHSV();
}