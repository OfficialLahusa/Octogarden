using System.Text;
using UnityEngine;

public class TooltipFormatter
{
    public static string FormatCactusTooltip(CactusData cactusData)
    {
        StringBuilder sb = new StringBuilder();

        // Name
        sb.Append("<align=\"center\"><b>");
        sb.Append(cactusData.Name);
        sb.AppendLine("</b>\r");

        // Description
        sb.Append("<size=70%><align=\"left\">");
        sb.AppendLine("Class: " + cactusData.Class.ToString());
        sb.AppendLine("Variant: " + cactusData.OutfitType.ToString());
        sb.AppendLine("Flower Color: " + cactusData.FlowerColor.ToString());
        sb.AppendLine("Pot: " + cactusData.PotType.ToString());
        sb.AppendLine("Affixes: " + cactusData.Affixes.ToString());
        sb.AppendLine($"Health: {cactusData.CurrentHealth}/{cactusData.MaxHealth}");
        sb.AppendLine("</size>");

        return sb.ToString();
    }
}
