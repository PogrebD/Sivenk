namespace Sivenk.DataTypes;

public struct GridBuildingData
{
    public GridBuildingData(Bounds newBounds, Point[] newPoints, Element[] newElements)
    {
        bounds = newBounds;
        points = newPoints;
        elements = newElements;
    }
    
    public Bounds bounds;
    public Point[] points;
    public  Element[] elements;
}