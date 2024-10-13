using Sivenk.DataTypes;

namespace Sivenk.Splitters;

public interface ISplitter
{
    Grid Split(Grid sourceGrid);
}