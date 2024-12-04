namespace MathLibrary.DataTypes.Internal;

public class Interval1D : Interval
{
    public double Start => LeftBorder[0];
    public double End => RightBorder[0];
    
    public Interval1D(Point leftBorder, Point rightBorder) : base(leftBorder, rightBorder)
    {
    }
}