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

    public void SetText(int spawnedAmount, int createdAmount, int activeAmount, string objectName)
    {
        _text.text = $"{objectName}:" +
                     $"\nSpawned amount: {spawnedAmount} " +
                     $"\nCreated amount: {createdAmount} " +
                     $"\nActive amount: {activeAmount}";
    }
}