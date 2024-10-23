using MathLibrary.DataTypes;
using Sivenk.DataTypes;

namespace Sivenk.Splitters.PointSplitters;

public class IntegralSplitter : IPointSplitter
{
    public Point[] SplitPoints(Point startPoint, Point endPoint, Split splitInfo)
    {
        Point[] result = new Point[splitInfo.PointsNum];
        
        Vector direction = endPoint - startPoint;
        Vector normalizedDirection = direction.Normalize();
        double length = direction.Lenght();
        
        double step = length / splitInfo.IntervalsNum;
        for (int i = 0; i < splitInfo.PointsNum; i++)
        {
            result[i] = startPoint + step * i * normalizedDirection;
        }
        
        return result;
    }
}