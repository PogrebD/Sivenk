namespace Sivenk.DataTypes;

public record struct Bounds(int ElementsNumX, int ElementsNumY)
{
    public readonly int ElementsNum => ElementsNumX * ElementsNumY;
    public readonly int PointsNumX => ElementsNumX + 1;
    public readonly int PointsNumY => ElementsNumY + 1;
    public readonly int PointsNum => PointsNumX * PointsNumY;
}