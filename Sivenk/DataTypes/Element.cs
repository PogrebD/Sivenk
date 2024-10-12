namespace Sivenk.DataTypes;

public class Element
{
    public Element(int[] idPoints)
    {
        IdPoints = idPoints;
    }
    public int[] IdPoints { get; set; }
    public int material { get; set; }
}