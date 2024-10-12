using Sivenk.DataTypes;

namespace Sivenk;

public readonly struct InputData
{
    public Point[,] Points { get; init; }
    public Area[] Area { get; init; }
    public Material[] Material { get; init; }
}
