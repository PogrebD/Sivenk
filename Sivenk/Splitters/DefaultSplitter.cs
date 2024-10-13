using Sivenk.DataTypes;

namespace Sivenk.Splitters;

public class DefaultSplitter : ISplitter
{
    public Grid Split(Grid sourceGrid)
    {
        return sourceGrid;
    }
}