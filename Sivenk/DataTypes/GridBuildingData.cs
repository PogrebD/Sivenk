using MathLibrary.DataTypes;

namespace Sivenk.DataTypes;

public struct GridBuildingData
{
    public Bounds bounds;
    public Point[] points;
    public Element[] elements;
    
    public GridBuildingData(Bounds newBounds, Point[] newPoints, Element[] newElements)
    {
        bounds = newBounds;
        points = newPoints;
        elements = newElements;
    }
}