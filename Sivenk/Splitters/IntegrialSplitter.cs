using Sivenk.DataTypes;

namespace Sivenk.Splitters;

public class IntegrialSplitter : ISplitter
{
    private int _coefX;
    private int _coefY;
    
    public IntegrialSplitter(int coefX, int coefY)
    {
        _coefX = coefX;
        _coefY = coefY;
    }
    public Grid Split(Grid sourceGrid)
    {
        Grid result = sourceGrid;
        return result;
    }
}