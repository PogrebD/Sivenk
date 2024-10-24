using MathLibrary.DataTypes;

namespace Sivenk.DataTypes;

public class Grid
{
    public Bounds Bounds;
    public Element[] Elements;
    public Point[] Points;

    public Grid(Bounds bounds, Element[] elements, Point[] points)
    {
        Bounds = bounds;
        Elements = elements;
        Points = points;
    }

    public int[] GetEdgeIds(int elementId)
    {
        return Elements[elementId].Edges;
    }

    public IList<int> GetElementIds(int edgeId)
    {
        int[] pointIds = GetPointsId(edgeId);
        int pointsIdDifference = pointIds[1] - pointIds[0];
        int layerId = pointIds[0] / Bounds.PointsNumX;

        var result = new List<int>();
        int[] elementIds;

        if (pointsIdDifference == Bounds.PointsNumX)
        {
            elementIds = [pointIds[0] - (layerId + 1), pointIds[0] - layerId];
        }
        else if (pointsIdDifference == 1 && pointIds[1] % Bounds.PointsNumX != 0)
        {
            elementIds = [pointIds[0] - layerId, pointIds[0] - Bounds.ElementsNumX - layerId];
        }
        else
        {
            throw new ArgumentException("Invalid edgeId specified.");
        }

        foreach (int elementId in elementIds)
        {
            if (IsElementIdValid(elementId))
            {
                result.Add(elementId);
            }
        }

        return result;
    }

    private bool IsElementIdValid(int elementId)
    {
        return elementId >= 0 && elementId < Bounds.ElementsNum;
    }

    public int GetEdgeId(int firstPointId, int secondPointId)
    {
        if (secondPointId < firstPointId)
        {
            (firstPointId, secondPointId) = (secondPointId, firstPointId);
        }

        int lineLevel = -1;
        int pointsIdDifference = secondPointId - firstPointId;
        if (pointsIdDifference == Bounds.PointsNumX)
        {
            lineLevel = 1;
        }
        
        if (pointsIdDifference == 1 && secondPointId % Bounds.PointsNumX != 0)
        {
            lineLevel = 0;
        }

        if (lineLevel != -1)
        {
            int edgeId = firstPointId + (firstPointId / Bounds.PointsNumX + lineLevel) * Bounds.ElementsNumX;
            Console.WriteLine($"EdgeId: {edgeId}");
            return edgeId;
        }
        
        throw new ArgumentException();
    }
    
    public int[] GetPointsId(int edgeId)
    {
        if (edgeId < 0 || edgeId >= Bounds.EdgesNum)
        {
            throw new ArgumentException();
        }
        
        int sum = Bounds.ElementsNumX + Bounds.PointsNumX;

        int[] points = new int[2];
        if (edgeId % sum < Bounds.ElementsNumX)
        {
            points[0] = edgeId - (edgeId / sum * Bounds.ElementsNumX);
            points[1] = points[0] + 1;
        }
        else
        {
            points[0] = edgeId - (edgeId / sum + 1) * Bounds.ElementsNumX;
            points[1] = points[0] + Bounds.PointsNumX;
        }
        
        Console.WriteLine($"Points: {points[0]}, {points[1]}");
        return points;
    }
}