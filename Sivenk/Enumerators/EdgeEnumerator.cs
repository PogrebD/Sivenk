using Sivenk.DataTypes;

namespace Sivenk.Enumerators;

public class EdgeEnumerator
{
    public void EnumerateEdges(Grid grid)
    {
        for (int i = 0; i < grid.Bounds.ElementsNumY; ++i)
        {
            for (int j = 0; j < grid.Bounds.ElementsNumX; ++j)
            {
                Element currentElement = grid.Elements[i * grid.Bounds.ElementsNumX + j];

                int pointsSum = grid.Bounds.PointsNumX + grid.Bounds.ElementsNumX;
                int secondElement = grid.Bounds.ElementsNumX + i * pointsSum + j;
                
                int[] egdes = new[]
                {
                    i * pointsSum + j,
                    secondElement,
                    secondElement + 1,
                    (i + 1) * pointsSum + j
                };

                currentElement.Edges = egdes;
            }   
        }
    }
}