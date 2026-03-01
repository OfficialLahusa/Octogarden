using System.Text;
using UnityEngine;

public class TooltipFormatter
{
    public static string FormatCactusTooltip(CactusData cactusData)
    {
        StringBuilder sb = new StringBuilder();

        // Name
        sb.Append("<align=\"center\"><b><color=green>");
        sb.Append(cactusData.Name);
        sb.AppendLine("</color></b>");

        // Description
        sb.Append("<size=70%><align=\"left\">");
        sb.AppendLine("\n<align=\"center\">Traits</align>");
        sb.AppendLine("Class: " + cactusData.Class.ToString());
        sb.AppendLine("Variant: " + cactusData.OutfitType.ToString());
        //sb.AppendLine("Flower Color: " + cactusData.FlowerColor.ToString());
        sb.AppendLine("Pot: " + cactusData.PotType.ToString());
        //sb.AppendLine("Affixes: " + cactusData.Affixes.ToString());
        sb.AppendLine("\n<align=\"center\">Base Stats</align>");
        sb.AppendLine($"Health: {cactusData.CurrentHealth}/{cactusData.MaxHealth}");
        sb.AppendLine($"Attack: {cactusData.AttackDamage} damage every {cactusData.AttackIntervalSeconds:n2}s");
        sb.AppendLine("</size>");

        return sb.ToString();
    }
}
