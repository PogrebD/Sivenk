namespace Sivenk.DataTypes;

public readonly struct InputData
{
    public Point[,] Points { get; init; }
    public Area[] Area { get; init; }
    public Material[] Material { get; init; }
}
