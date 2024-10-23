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

    public int GetEdgeId(int firstPointId, int secondPointId)
    {
        return 1;
    }
    
    public int[] GetPointsId(int edgeId)
    {
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