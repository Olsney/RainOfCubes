using DefaultNamespace;
using TMPro;
using UnityEngine;

public class TextInfo<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpawnerBase<T> _spawner;

    private void OnEnable()
    {
        _spawner.ValueChanged += SetText;
    }

    private void OnDisable()
    {
        _spawner.ValueChanged -= SetText;
    }

    public void SetText(SpawnerInfoKeeper spawnerInfoKeeper)
    {
        _text.text = $"{spawnerInfoKeeper.Name}:" +
                     $"\nSpawned amount: {spawnerInfoKeeper.SpawnedAmount} " +
                     $"\nCreated amount: {spawnerInfoKeeper.CreatedAmount} " +
                     $"\nActive amount: {spawnerInfoKeeper.ActiveAmount}";
    }
}