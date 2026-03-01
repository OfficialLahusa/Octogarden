using TMPro;
using UnityEngine;

public class MutateButton : MonoBehaviour
{
    [SerializeField]
    TMP_Text buttonText;

    [SerializeField]
    ShopHandler shopHandler;

    void FixedUpdate()
    {
        buttonText.text = $"Mutate\n<size=60%>({shopHandler.GetMutationCost()} Seaweed)";
    }
}
