namespace Sivenk.DataTypes;

public class Element
{
    public int[] IdPoints { get; set; }
    public int Material { get; set; }

    public Element(int[] idPoints)
    {
        IdPoints = idPoints;
    }

    public Element(int[] idPoints, int material)
    {
        IdPoints = idPoints;
        Material = material;
    }
}