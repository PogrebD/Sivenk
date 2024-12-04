using MathLibrary.DataTypes;
using Sivenk.DataTypes;
using Sivenk.Splitters.DataTypes;
using Sivenk.Splitters.PointSplitters;

namespace Sivenk.Splitters.GridSplitters;

public class DefaultGridSplitter : IGridSplitter
{
    private readonly Split[] _splitsX;
    private readonly Split[] _splitsY;
    
    private IPointSplitter[] _pointSplitters;
    
    public DefaultGridSplitter(Split[] splitsX, Split[] splitsY)
    {
        _splitsX = splitsX;
        _splitsY = splitsY;
        
        _pointSplitters = [new IntegralSplitter(), new DischargeSplitter()];
    }
    
    public Grid Split(Grid sourceGrid)
    {
        Bounds bounds = new(SumInterval(_splitsX), SumInterval(_splitsY));

        Element[] elements = new Element[bounds.ElementsNum];
        Point[] points = new Point[bounds.PointsNum];
        
        IterationData iterationData = new();
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
        
        Grid result = new(bounds, elements, points, sourceGrid.Materials);
        return result;
    }
    
    public Element[] SplitElement(Bounds bounds, IterationData iterationData)
    {
        Element[] result = new Element[iterationData.CurrentSplitX.IntervalsNum * iterationData.CurrentSplitY.IntervalsNum];

        for (int i = 0; i < iterationData.CurrentSplitY.IntervalsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.IntervalsNum; ++j)
            {
                int material = iterationData.CurrentElement.Material;
                int[] idPoints =
                [
                    (iterationData.PrevElemsY + i) * bounds.PointsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i) * bounds.PointsNumX + iterationData.PrevElemsX + j + 1,
                    (iterationData.PrevElemsY + i + 1) * bounds.PointsNumX + iterationData.PrevElemsX + j,
                    (iterationData.PrevElemsY + i + 1) * bounds.PointsNumX + iterationData.PrevElemsX + j + 1
                ];
                
                result[i * iterationData.CurrentSplitX.IntervalsNum + j] = new Element(idPoints, material);
            }
        }

        return result;
    }

    public Point[] SplitPoints(Grid sourceGrid, IterationData iterationData)
    {
        Point[] result = new Point[(iterationData.CurrentSplitX.PointsNum) * (iterationData.CurrentSplitY.PointsNum)];
        
        Point bottomLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[0]];
        Point bottomRightPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[1]];
        Point topLeftPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[2]];
        Point topRightPoint = sourceGrid.Points[iterationData.CurrentElement.IdPoints[3]];

        IPointSplitter pointSplitterY = GetPointsSplitter(iterationData.CurrentSplitY);
        Point[] leftPointsY = pointSplitterY.SplitPoints(bottomLeftPoint, topLeftPoint, iterationData.CurrentSplitY);
        Point[] rightPointsY = pointSplitterY.SplitPoints(bottomRightPoint, topRightPoint, iterationData.CurrentSplitY);

        for (int i = 0; i < leftPointsY.Length; ++i)
        {
            IPointSplitter pointSplitterX = GetPointsSplitter(iterationData.CurrentSplitX);
            Point[] pointsX = pointSplitterX.SplitPoints(leftPointsY[i], rightPointsY[i], iterationData.CurrentSplitX);
            for (int j = 0; j < pointsX.Length; ++j)
            {
                result[i * iterationData.CurrentSplitX.PointsNum + j] = pointsX[j];
            }
        }

        return result;
    }

    private IPointSplitter GetPointsSplitter(Split splitData)
    {
        return Math.Abs(splitData.DischargeCoefficient - 1) < 1e-6 ? _pointSplitters[0] : _pointSplitters[1];
    }

    protected virtual void InsertElements(Element[] elements, Element[] splitedElements, Bounds bounds, IterationData iterationData)
    {
        for (int i = 0; i < iterationData.CurrentSplitY.IntervalsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.IntervalsNum; ++j)
            {
                elements[(iterationData.PrevElemsY + i) * bounds.ElementsNumX + iterationData.PrevElemsX + j] = splitedElements[iterationData.CurrentSplitX.IntervalsNum * i + j];
            }
        }
    }
    
    protected virtual void InsertPoints(Point[] points, Point[] splitedPoints, Bounds bounds, IterationData iterationData)
    {
        for (int i = 0; i < iterationData.CurrentSplitY.PointsNum; ++i)
        {
            for (int j = 0; j < iterationData.CurrentSplitX.PointsNum; ++j)
            {
                points[(iterationData.PrevElemsY + i) * bounds.PointsNumX + iterationData.PrevElemsX + j] = splitedPoints[iterationData.CurrentSplitX.PointsNum * i + j];
            }
        }
    }
    
    protected virtual int SumInterval(Split[] intervals)
    {
        int sum = 0;
        foreach(Split split in intervals)
        {
            sum += split.IntervalsNum;
        }
        return sum;
    }
}