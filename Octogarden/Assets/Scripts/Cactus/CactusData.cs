public class CactusData
{
    // Flavor text name
    public string Name { get; set; }

    // Combinatorial traits
    public CactusClass Class { get; set; }
    public CactusOutfitType OutfitType { get; set; }
    public CactusFlowerColor FlowerColor { get; set; }
    public CactusPotType PotType { get; set; }
    public CactusAffix Affixes { get; set; }

    // TODO: Stat system
    public uint MaxHealth { get; set; }
    public uint CurrentHealth { get; set; }
    public float AttackIntervalSeconds { get; set; }
    public uint AttackDamage { get; set; }

    public CactusData(string name, CactusClass mechanicalClass, uint maxHealth, float attackIntervalSeconds, uint attackDamage)
    {
        Name = name;
        Class = mechanicalClass;
        OutfitType = CactusOutfitType.Basic;
        FlowerColor = CactusFlowerColor.Red;
        PotType = CactusPotType.Ceramic;
        Affixes = CactusAffix.None;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackIntervalSeconds = attackIntervalSeconds;
        AttackDamage = attackDamage;
    }

    public CactusData(CactusData other)
    {
        Name = (string)other.Name.Clone();
        Class = other.Class;
        OutfitType = other.OutfitType;
        FlowerColor = other.FlowerColor;
        PotType = other.PotType;
        Affixes = other.Affixes;
        MaxHealth = other.MaxHealth;
        CurrentHealth = other.CurrentHealth;
        AttackIntervalSeconds = other.AttackIntervalSeconds;
        AttackDamage = other.AttackDamage;
    }
}
