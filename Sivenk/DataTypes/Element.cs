namespace Sivenk.DataTypes;

public class Element
{
    public Element(int[] idPoints)
    {
        IdPoints = idPoints;
    }

    public Element(int[] idPoints, int material)
    {
        IdPoints = idPoints;
        this.material = material;
    }
    public int[] IdPoints { get; set; }
    public int material { get; set; }
}