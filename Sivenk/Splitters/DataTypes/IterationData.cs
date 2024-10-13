using Sivenk.DataTypes;

namespace Sivenk.Splitters.DataTypes;

public struct IterationData
{
    public int PrevElemsX = 0;
    public int PrevElemsY = 0;
    
    public Split CurrentSplitX = new Split();
    public Split CurrentSplitY = new Split();
    
    public Element CurrentElement = null;

    public IterationData()
    {
    }
}