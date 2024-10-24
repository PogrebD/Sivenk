namespace Sivenk.DataTypes;

public record struct Bounds(int ElementsNumX, int ElementsNumY)
{
    public readonly int ElementsNum => ElementsNumX * ElementsNumY;
    public readonly int PointsNumX => ElementsNumX + 1;
    public readonly int PointsNumY => ElementsNumY + 1;
    public readonly int PointsNum => PointsNumX * PointsNumY;
    public readonly int EdgesHorizontalNumX => PointsNumX;
    public readonly int EdgesVerticalNumX => ElementsNumX;
    public readonly int EdgesHorizontalNumY => PointsNumY;
    public readonly int EdgesVerticalNumY => ElementsNumY;
    public readonly int EdgesNumX => EdgesHorizontalNumX + EdgesVerticalNumX;
    public readonly int EdgesNumY => EdgesHorizontalNumY + EdgesVerticalNumY;
    public readonly int EdgesNum => EdgesNumX * ElementsNumY + EdgesHorizontalNumX;
}