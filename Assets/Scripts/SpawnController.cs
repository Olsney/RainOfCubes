using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private BombSpawner _bombSpawner;
        [SerializeField] private CubeSpawner _cubeSpawner;

        private void OnEnable()
        {
            _cubeSpawner.CubeDestroyed += OnCubeDestroyed;
        }

        private void OnDisable()
        {
            _cubeSpawner.CubeDestroyed -= OnCubeDestroyed;
        }

        private void OnCubeDestroyed(Vector3 position)
        {
            _bombSpawner.Spawn(position);
        }
    }
}