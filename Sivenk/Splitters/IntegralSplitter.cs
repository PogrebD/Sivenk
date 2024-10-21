using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters;

public class IntegralSplitter : ElementSplitter
{
    private readonly Split[] _splitsX;
    private readonly Split[] _splitsY;

    public IntegralSplitter(Split[] splitsX, Split[] splitsY) : base(splitsX, splitsY)
    {
        
    }
    
    protected override Point[] SplitPoints(Grid sourceGrid, IterationData iterationData)
    {
        Point[] result = new Point[(iterationData.CurrentSplitX.PointsNum) * (iterationData.CurrentSplitY.PointsNum)];

        Point bottomLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[0]];
        Point bottomRightPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[1]];
        Point topLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[2]];
        
        double stepX = (bottomRightPoint.X - bottomLeftPoint.X) / iterationData.CurrentSplitX.IntervalsNum;
        double stepY = (topLeftPoint.Y - bottomLeftPoint.Y) / iterationData.CurrentSplitY.IntervalsNum;
        
        for (int i = 0; i < iterationData.CurrentSplitY.PointsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.PointsNum; ++j)
            {
                result[i * iterationData.CurrentSplitX.PointsNum + j] = new Point(bottomLeftPoint.X + stepX * j, bottomLeftPoint.Y + stepY * i);
            }
        }
        
        return result;
    }
}