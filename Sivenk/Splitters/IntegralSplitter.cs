using MathLibrary.DataTypes;
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
        Point topRightPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[3]];
        
        Vector leftDirectionY = (topLeftPoint - bottomLeftPoint).Normalize();
        Vector rightDirectionY = (topRightPoint - bottomRightPoint).Normalize();
        
        double leftStepY = (topLeftPoint - bottomLeftPoint).Lenght() / iterationData.CurrentSplitY.IntervalsNum;
        double rightStepY = (topRightPoint - bottomRightPoint).Lenght() / iterationData.CurrentSplitY.IntervalsNum;
        
        for (int i = 0; i < iterationData.CurrentSplitY.PointsNum; ++i)
        {
            Point newBottomLeftPoint = bottomLeftPoint + leftStepY * i * leftDirectionY;
            Point newBottomRightPoint = bottomRightPoint + rightStepY * i * rightDirectionY;
            
            for (int j = 0; j < iterationData.CurrentSplitX.PointsNum; ++j)
            {
                Vector directionX = (newBottomRightPoint - newBottomLeftPoint).Normalize();
                double stepX = (newBottomRightPoint - newBottomLeftPoint).Lenght() / iterationData.CurrentSplitX.IntervalsNum;
                
                result[i * iterationData.CurrentSplitX.PointsNum + j] = newBottomLeftPoint + stepX * j * directionX;
            }
        }
        
        return result;
    }
}