using MathLibrary.DataTypes;
using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters.PointSplitters;

public interface IPointSplitter
{
    Point[] SplitPoints(Point startPoint, Point endPoint, Split splitInfo);
}