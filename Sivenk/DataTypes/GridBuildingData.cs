namespace Sivenk.DataTypes;

public struct GridBuildingData
{
    public GridBuildingData(Point[,] newPoints, Element[] newElements)
    {
        points = newPoints;
        elements = newElements;
    }
    
    public Point[,] points;
    public  Element[] elements;
}