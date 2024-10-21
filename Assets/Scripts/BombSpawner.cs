using UnityEngine;

namespace DefaultNamespace
{
    public class BombSpawner : SpawnerBase<Bomb>
    {
        protected override string SpawnerName => "BombSpawner";
        
        [SerializeField] private CubeSpawner _cubeSpawner;
        
        private void OnEnable()
        {
            _cubeSpawner.CubeDestroyed += OnCubeDestroyed;
        }

        private void OnDisable()
        {
            _cubeSpawner.CubeDestroyed -= OnCubeDestroyed;
        }

        private void OnCubeDestroyed(Vector3 position) => 
            Spawn(position);
    }
}