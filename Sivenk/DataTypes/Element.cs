namespace Sivenk.DataTypes;

public class Element
{
    public Element() { }
    public Element(Tuple<int, int>[] idPoints)
    {
        IdPoints = idPoints;
    }
    
    public Element(Tuple<int, int>[] idPoints, int material)
    {
        IdPoints = idPoints;
        this.material = material;
    }
    public Tuple<int, int>[] IdPoints { get; set; }
    public int material { get; set; }
}