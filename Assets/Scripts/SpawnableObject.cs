using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public abstract class SpawnableObject<T> : MonoBehaviour where T : SpawnableObject<T>
    {
        public event Action<T> Destroyed;

        public abstract void Init();

        protected int GetRandomDelay()
        {
            int min = 2;
            int max = 5;

            return Random.Range(min, max + 1);
        }

        protected void Disable()
        {
            Destroyed?.Invoke((T)this);
        }
    }
}