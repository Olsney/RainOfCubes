using TMPro;
using UnityEngine;

public class TextInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText(int spawnedAmount, int createdAmount, int activeAmount, string objectName)
    {
        _text.text = $"{objectName}:" +
                     $"\nSpawned amount: {spawnedAmount} " +
                     $"\nCreated amount: {createdAmount} " +
                     $"\nActive amount: {activeAmount}";
    }
}