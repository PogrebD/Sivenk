namespace MathLibrary.DataTypes.Internal;

public class Interval
{
    public Point LeftBorder;
    public Point RightBorder;
    public Interval(Point leftBorder, Point rightBorder)
    {
        LeftBorder = leftBorder;
        RightBorder = rightBorder;
    }
}