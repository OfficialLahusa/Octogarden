using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text tooltipText;

    [SerializeField]
    GameObject tooltipObj;

    public static CactusData currentShownTooltip;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentShownTooltip != null)
        {
            tooltipObj.SetActive(true);
            tooltipText.text = TooltipFormatter.FormatCactusTooltip(currentShownTooltip);
            currentShownTooltip = null;
        }
        else
        {
            tooltipObj.SetActive(false);
            tooltipText.text = "";
        }
    }

    public static void ShowTooltip(CactusData cactusData)
    {
        currentShownTooltip = cactusData;
    }
}