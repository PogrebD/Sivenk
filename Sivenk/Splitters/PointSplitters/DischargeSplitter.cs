using MathLibrary.DataTypes;
using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters.PointSplitters;

public class DischargeSplitter : IPointSplitter
{
    public Point[] SplitPoints(Point startPoint, Point endPoint, Split splitInfo)
    {
        Point[] result = new Point[splitInfo.PointsNum];
        
        Vector direction = endPoint - startPoint;
        Vector normalizedDirection = direction.Normalize();
        double length = direction.Lenght();
        
        double dischargeCoeff = splitInfo.DischargeCoefficient > 0 
            ? splitInfo.DischargeCoefficient 
            : 1 / -splitInfo.DischargeCoefficient;
        
        double initialStep = length * (1 - dischargeCoeff) / (1 - double.Pow(dischargeCoeff, splitInfo.IntervalsNum));
        for (int i = 0; i < splitInfo.PointsNum; i++)
        {
            double step = initialStep * (1 - double.Pow(dischargeCoeff, i)) / (1 - dischargeCoeff);
            result[i] = startPoint + step * normalizedDirection;
        }
        
        return result;
    }
}