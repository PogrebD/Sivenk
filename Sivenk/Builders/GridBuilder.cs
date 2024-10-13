using Sivenk.Constructors;
using Sivenk.DataTypes;
using Sivenk.Splitters;

namespace Sivenk.Builders;

public class GridBuilder
{
    private IConstructor _gridConstructor = new DefaultConstructor();
    private ISplitter _gridSplitter = new DefaultSplitter();

    public GridBuilder SetGridConstructor(IConstructor newConstructor)
    {
        _gridConstructor = newConstructor;
        return this;
    }
    
    public GridBuilder SetGridSplitter(ISplitter newSplitter)
    {
        _gridSplitter = newSplitter;
        return this;
    }
    
    public Grid Build(GridBuildingData gridBuildingData)
    {
        Grid constructedGrid = _gridConstructor.Construct(gridBuildingData);
        Grid splittedGrid = _gridSplitter.Split(constructedGrid);

        return splittedGrid;
    }
}