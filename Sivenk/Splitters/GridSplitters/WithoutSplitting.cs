using Sivenk.DataTypes;

namespace Sivenk.Splitters.GridSplitters;

public class WithoutSplitting : IGridSplitter
{
    public Grid Split(Grid sourceGrid)
    {
        return sourceGrid;
    }
}