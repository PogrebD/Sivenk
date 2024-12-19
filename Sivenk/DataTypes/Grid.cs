using MathLibrary.DataTypes;

namespace Sivenk.DataTypes;

public class Grid
{
    public Bounds Bounds;
    public Element[] Elements;
    public Point[] Points;
    public Material[] Materials;

    public Grid(Bounds bounds, Element[] elements, Point[] points, Material[] materials)
    {
        Bounds = bounds;
        Elements = elements;
        Points = points;
        Materials = materials;
    }

    public int[] GetEdgeIds(int elementId)
    {
        var edges = Elements[elementId].Edges;

        Console.WriteLine($"Input {nameof(elementId)}: {elementId} => EdgeId: {edges[0]}, {edges[1]}, {edges[2]}, {edges[3]}");

        return edges;
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
            Console.WriteLine($"Input points: {firstPointId}, {secondPointId} => EdgeId: {edgeId}");
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

        int edgesSumInRow = Bounds.EdgesHorizontalNumX + Bounds.EdgesVerticalNumX;

        int[] points = new int[2];
        if (edgeId % edgesSumInRow < Bounds.EdgesHorizontalNumX)
        {
            points[0] = edgeId - (edgeId / edgesSumInRow * Bounds.ElementsNumX); // hr
            points[1] = points[0] + 1;
        }
        else
        {
            points[0] = edgeId - (edgeId / edgesSumInRow + 1) * Bounds.ElementsNumX; //vr
            points[1] = points[0] + Bounds.PointsNumX;
        }

        Console.WriteLine($"Input Edge: {edgeId} => Points: {points[0]}, {points[1]}");
        return points;
    }

    private bool IsElementIdValid(int elementId)
    {
        return elementId >= 0 && elementId < Bounds.ElementsNum;
    }
}