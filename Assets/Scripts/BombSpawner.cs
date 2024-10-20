using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class BombSpawner : SpawnerBase<Bomb>
    {
        protected override string SpawnerName => "BombSpawner";
    }
}