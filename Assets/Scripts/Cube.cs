using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private const float Delay = 1f;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _isPlatformTouched;
    private Color _defaultColor;

    public event Action<Cube> Destroyed;

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
    
    public void Init(Vector3 position)
    {
        if (_isPlatformTouched)
            _isPlatformTouched = false;
        
        transform.position = position;
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = _defaultColor;
        gameObject.SetActive(true);
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(GetRandomDelay());

        Destroyed?.Invoke(this);
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