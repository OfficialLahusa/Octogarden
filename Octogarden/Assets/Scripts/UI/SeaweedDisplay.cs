using TMPro;
using UnityEngine;

public class SeaweedDisplay : MonoBehaviour
{
    private TMP_Text _text;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = $"Seaweed: {PlayerInventory.Instance.Seaweed}";
    }
}
