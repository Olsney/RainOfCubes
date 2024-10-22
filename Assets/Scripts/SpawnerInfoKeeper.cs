﻿namespace DefaultNamespace
{
    public class SpawnerInfoKeeper
    {
        public int SpawnedAmount { get; }
        public int CreatedAmount { get; }
        public int ActiveAmount { get; }
        public string Name { get; }

        public SpawnerInfoKeeper(int spawnedAmount, int createdAmount, int activeAmount, string name)
        {
            SpawnedAmount = spawnedAmount;
            CreatedAmount = createdAmount;
            ActiveAmount = activeAmount;
            Name = name;
        }
    }
}