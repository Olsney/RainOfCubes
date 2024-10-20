using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : SpawnableObject<Cube>
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isPlatformTouched;
    private Color _defaultColor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformTouched)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _renderer.material.color = GetRandomColor();
        _isPlatformTouched = true;

        StartCoroutine(DestroyWithDelay());
    }
    
    public override void Init()
    {
        if (_isPlatformTouched)
            _isPlatformTouched = false;
        
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = _defaultColor;
        gameObject.SetActive(true);
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(GetRandomDelay());

        Disable();
    }

    private Color GetRandomColor() =>
        Random.ColorHSV();
}