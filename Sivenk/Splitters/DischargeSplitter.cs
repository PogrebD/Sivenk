using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters;

public class DischargeSplitter : ElementSplitter
{
    public DischargeSplitter(Split[] splitsX, Split[] splitsY): base(splitsX, splitsY)
    {
    }

    protected override Point[] SplitPoints(Grid sourceGrid, IterationData iterationData)
    {
        Point[] result = new Point[(iterationData.CurrentSplitX.PointsNum) * (iterationData.CurrentSplitY.PointsNum)];

        Point bottomLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[0]];
        Point bottomRightPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[1]];
        Point topLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[2]];
        
        double intervalX = bottomRightPoint.X - bottomLeftPoint.X;
        double intervalY = topLeftPoint.Y - bottomLeftPoint.Y;
        
        double dischargeCoeffX = iterationData.CurrentSplitX.DischargeCoefficient > 0 
            ? iterationData.CurrentSplitX.DischargeCoefficient 
            : 1 / -iterationData.CurrentSplitX.DischargeCoefficient;
        
        double dischargeCoeffY = iterationData.CurrentSplitY.DischargeCoefficient > 0 
            ? iterationData.CurrentSplitY.DischargeCoefficient 
            : 1 / -iterationData.CurrentSplitY.DischargeCoefficient;
        
        double initialStepX = intervalX * (1 - dischargeCoeffX) / (1 - double.Pow(dischargeCoeffX, iterationData.CurrentSplitX.IntervalsNum));
        double initialStepY = intervalY * (1 - dischargeCoeffY) / (1 - double.Pow(dischargeCoeffY, iterationData.CurrentSplitY.IntervalsNum));
        
        for (int i = 0; i < iterationData.CurrentSplitY.PointsNum; ++i)
        {
            double stepY = initialStepY * (1 - double.Pow(dischargeCoeffY, i)) / (1 - dischargeCoeffY);
            for (int j = 0; j < iterationData.CurrentSplitX.PointsNum; ++j)
            {
                double stepX = initialStepX * (1 - double.Pow(dischargeCoeffX, j)) / (1 - dischargeCoeffX);
                result[i * iterationData.CurrentSplitX.PointsNum + j] = new Point(bottomLeftPoint.X + stepX, bottomLeftPoint.Y + stepY);
            }
        }
        
        return result;
    }
}