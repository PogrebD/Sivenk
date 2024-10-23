using Sivenk.DataTypes;

namespace Sivenk.Splitters.GridSplitters;

public interface IGridSplitter
{
    Grid Split(Grid grid);
}