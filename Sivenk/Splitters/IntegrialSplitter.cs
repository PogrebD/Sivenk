using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;

namespace Sivenk.Splitters;

public class IntegrialSplitter : ISplitter
{
    private Split[] _splitsX;
    private Split[] _splitsY;
    
    public IntegrialSplitter(Split[] splitsX, Split[] splitsY)
    {
        _splitsX = splitsX;
        _splitsY = splitsY;
    }
    public Grid Split(Grid sourceGrid)
    {
        Bounds bounds = new Bounds(SumInterval(_splitsX), SumInterval(_splitsY));

        Element[] elements = new Element[bounds.ElementsNum];
        Point[] points = new Point[bounds.PointsNum];
        
        IterationData iterationData = new IterationData();
        for (int i = 0; i < sourceGrid.Bounds.ElementsNumY; ++i)
        {
            iterationData.CurrentSplitY = _splitsY[i];
            for (int j = 0; j < sourceGrid.Bounds.ElementsNumX; ++j)
            {
                iterationData.CurrentSplitX = _splitsX[j];
                iterationData.CurrentElement = sourceGrid.Elements[i * sourceGrid.Bounds.ElementsNumX + j];
                
                Element[] splitedElements = SplitElement(bounds, iterationData);
                Point[] splitedPoints = SplitPoints(sourceGrid, iterationData);
                
                InsertElements(elements, splitedElements, bounds, iterationData);
                InsertPoints(points, splitedPoints, bounds, iterationData);
                
                iterationData.PrevElemsX += iterationData.CurrentSplitX.IntervalsNum;
            }

            iterationData.PrevElemsX = 0;
            iterationData.PrevElemsY += iterationData.CurrentSplitY.IntervalsNum;
        }
        
        Grid result = new Grid(bounds, elements, points);
        return result;
    }

    private void InsertElements(Element[] elements, Element[] splitedElements, Bounds bounds, IterationData iterationData)
    {
        for (int i = 0; i < iterationData.CurrentSplitY.IntervalsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.IntervalsNum; ++j)
            {
                elements[(iterationData.PrevElemsY + i) * bounds.ElementsNumX + iterationData.PrevElemsX + j] = splitedElements[j];
            }
        }
    }
    
    private void InsertPoints(Point[] points, Point[] splitedPoints, Bounds bounds, IterationData iterationData)
    {
        for (int i = 0; i < iterationData.CurrentSplitY.PointsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.PointsNum; ++j)
            {
                points[(iterationData.PrevElemsY + i) * bounds.PointsNumX + iterationData.PrevElemsX + j] = splitedPoints[j];
            }
        }
    }

    private Element[] SplitElement(Bounds bounds, IterationData iterationData)
    {
        Element[] result = new Element[iterationData.CurrentSplitX.IntervalsNum * iterationData.CurrentSplitY.IntervalsNum];

        for (int i = 0; i < iterationData.CurrentSplitY.IntervalsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.IntervalsNum; ++j)
            {
                int material = iterationData.CurrentElement.Material;
                int[] idPoints =
                [
                    (iterationData.PrevElemsY + i) * bounds.ElementsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i) * bounds.ElementsNumX + iterationData.PrevElemsX + j + 1,
                    (iterationData.PrevElemsY + i + 1) * bounds.ElementsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i + 1) * bounds.ElementsNumX + iterationData.PrevElemsX + j + 1
                ];
                
                result[i * iterationData.CurrentSplitX.IntervalsNum + j] = new Element(idPoints, material);
            }
        }

        return result;
    }
    
    private Point[] SplitPoints(Grid sourceGrid, IterationData iterationData)
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