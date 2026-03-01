using System;

[Flags]
public enum CactusAffix
{
    None = 0,
    Venomous = 1 << 0,
    Lucrative = 1 << 1,
    Movable = 1 << 2,
    Dominant = 1 << 3,
    Recessive = 1 << 4,
    Social = 1 << 5,
    Solitary = 1 << 6,
}
