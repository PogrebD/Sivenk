namespace Sivenk.DataTypes;

public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public readonly override string ToString()
    {
        return new string($"{X} {Y}");
    }
}