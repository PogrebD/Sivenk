namespace Sivenk.DataTypes;

public readonly struct InputData
{
    public Line[] Lines { get; init; }
    public Area[] Area { get; init; }
    public Material[] Material { get; init; }
    public Split[] SplitX { get; init; }
    public Split[] SplitY { get; init; }
}
