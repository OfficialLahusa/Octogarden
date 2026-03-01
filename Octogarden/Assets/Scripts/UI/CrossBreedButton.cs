using TMPro;
using UnityEngine;

public class CrossBreedButton : MonoBehaviour
{
    [SerializeField]
    TMP_Text buttonText;

    [SerializeField]
    ShopHandler shopHandler;

    void FixedUpdate()
    {
        buttonText.text = $"Crossbreed\n<size=60%>({shopHandler.GetCrossBreedCost()} Seaweed)";
    }
}
