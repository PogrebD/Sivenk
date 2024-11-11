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

                int edgesSumInRow = grid.Bounds.EdgesHorizontalNumX + grid.Bounds.EdgesVerticalNumX;
                int leftEdge = grid.Bounds.EdgesHorizontalNumX + i * edgesSumInRow + j;
                
                int[] egdes = new[]
                {
                    i * edgesSumInRow + j,
                    leftEdge,
                    leftEdge + 1,
                    (i + 1) * edgesSumInRow + j
                };

                currentElement.Edges = egdes;
            }   
        }
    }
}