using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters;

public class IntegrialSplitter : ISplitter
{
    private Split[] _splitsX;
    private Split[] _splitsY;
    private SplitData splitData;
    
    public IntegrialSplitter(Split[] splitsX, Split[] splitsY)
    {
        _splitsX = splitsX;
        _splitsY = splitsY;
        
        splitData = new SplitData();
        splitData.NewBounds = new Bounds(SumInterval(splitsX), SumInterval(splitsY));
    }
    public Grid Split(Grid sourceGrid)
    {
        IterationData iterationData = new IterationData();
        
        for (int i = 0; i < sourceGrid.bounds.ElementsNumY; ++i)
        {
            iterationData.CurrentSplitY = _splitsY[i];
            for (int j = 0; j < sourceGrid.bounds.ElementsNumX; ++j)
            {
                iterationData.CurrentSplitX = _splitsX[j];
                iterationData.CurrentElement = sourceGrid.elements[i * sourceGrid.bounds.ElementsNumX + j];
                
                Element[] splitedElements = SplitElement(splitData, iterationData);
                Point[] splitedPoints = SplitPoints(sourceGrid, iterationData);
                
                iterationData.PrevElemsX += iterationData.CurrentSplitX.IntervalsNum;
            }

            iterationData.PrevElemsX = 0;
            iterationData.PrevElemsY += iterationData.CurrentSplitY.IntervalsNum;
        }
        
        Grid result = sourceGrid;
        return result;
    }

    private Element[] SplitElement(SplitData splitData, IterationData iterationData)
    {
        Element[] result = new Element[iterationData.CurrentSplitX.IntervalsNum * iterationData.CurrentSplitY.IntervalsNum];

        for (int i = 0; i < iterationData.CurrentSplitY.IntervalsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.IntervalsNum; ++j)
            {
                int material = iterationData.CurrentElement.material;
                int[] idPoints =
                [
                    (iterationData.PrevElemsY + i) * splitData.NewBounds.ElementsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i) * splitData.NewBounds.ElementsNumX + iterationData.PrevElemsX + j + 1,
                    (iterationData.PrevElemsY + i + 1) * splitData.NewBounds.ElementsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i + 1) * splitData.NewBounds.ElementsNumX + iterationData.PrevElemsX + j + 1
                ];
                
                result[i * iterationData.CurrentSplitX.IntervalsNum + j] = new Element(idPoints, material);
            }
        }

        return result;
    }
    
    private Point[] SplitPoints(Grid sourceGrid, IterationData iterationData)
    {
        Point[] result = new Point[(iterationData.CurrentSplitX.PointsNum) * (iterationData.CurrentSplitY.PointsNum)];

        Point bottomLeftPoint = sourceGrid.points[iterationData.CurrentElement.IdPoints[0]];
        Point bottomRightPoint = sourceGrid.points[iterationData.CurrentElement.IdPoints[1]];
        Point topLeftPoint = sourceGrid.points[iterationData.CurrentElement.IdPoints[2]];
        
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
    
    private int SumInterval(Split[] intervals)
    {
        int sum = 0;
        foreach(Split split in intervals)
        {
            sum += split.IntervalsNum;
        }
        return sum;
    }
}