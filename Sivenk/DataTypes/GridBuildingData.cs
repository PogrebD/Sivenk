using MathLibrary.DataTypes;

namespace Sivenk.DataTypes;

public struct GridBuildingData
{
    public Bounds bounds;
    public Point[] points;
    public Element[] elements;
    public Material[] materials;
    
    public GridBuildingData(Bounds newBounds, Point[] newPoints, Element[] newElements, Material[] newMaterials)
    {
        bounds = newBounds;
        points = newPoints;
        elements = newElements;
        materials = newMaterials;
    }
}