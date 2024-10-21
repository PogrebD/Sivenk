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
}