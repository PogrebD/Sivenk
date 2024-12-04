namespace Sivenk.DataTypes;

public class Element
{
    public int[] IdPoints { get; set; }
    public int[] Edges { get; set; }
    public int Material { get; set; }
    public double[,] Mass { get; set; }
    public double[,] Stiffness { get; set; }
    public double[] VectorB { get; set; }

    public Element(int[] idPoints)
    {
        IdPoints = idPoints;
    }

    public Element(int[] idPoints, int material)
    {
        IdPoints = idPoints;
        Material = material;
    }
    
    public Element(double[,] mass, double[,] stiffness, double[] vectorB, Element parent)
    {
        Mass = mass;
        Stiffness = stiffness;
        VectorB = vectorB;
        IdPoints = parent.IdPoints;
        Material = parent.Material;
        Edges = parent.Edges;
    }
}