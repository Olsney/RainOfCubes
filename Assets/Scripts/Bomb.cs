using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody), typeof(Renderer))]
    public class Bomb : SpawnableObject<Bomb>
    {
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;

        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private Color _defaultColor;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.color;
            _targetColor = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0);
        }
        
        public override void Init()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _renderer.material.color = _defaultColor;
            gameObject.SetActive(true);

            StartCoroutine(Destroy());
        }

        private IEnumerator ChangeColor()
        {
            float duration = GetRandomDelay();

            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                _renderer.material.color = Color.Lerp(_defaultColor, _targetColor, i / duration);

                yield return null;
            }
        }

        private IEnumerator Destroy()
        {
            yield return ChangeColor();

            Explode();

            Disable();
        }

        private void Explode()
        {
            Collider[] colliders;
            colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.attachedRigidbody != null)
                    collider.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}