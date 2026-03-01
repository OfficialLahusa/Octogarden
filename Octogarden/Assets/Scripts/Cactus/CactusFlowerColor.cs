using UnityEngine;

public enum CactusFlowerColor
{
    Red,
    Yellow,
    White,
    Orange,
    Magenta,
    Blue,
    Black,
    Rainbow
}

public static class CactusFlowerColorExtensions
{
    public static Color ToColor(this CactusFlowerColor color)
    {
        return color switch
        {
            CactusFlowerColor.Red => Color.red,
            CactusFlowerColor.Yellow => Color.yellow,
            CactusFlowerColor.White => Color.white,
            CactusFlowerColor.Orange => new Color(1f, 0.5f, 0f),
            CactusFlowerColor.Magenta => Color.magenta,
            CactusFlowerColor.Blue => Color.blue,
            CactusFlowerColor.Black => Color.black,
            CactusFlowerColor.Rainbow => new Color(1f, 0.5f, 0f), // Placeholder for rainbow, will be altered at runtime
            _ => Color.white
        };
    }
}