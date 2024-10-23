using MathLibrary.DataTypes;
using Sivenk.DataTypes;
using Sivenk.Splitters;
using Sivenk.Splitters.GridSplitters;

namespace Sivenk.Builders;

public class GridBuilder
{
    private IGridSplitter _gridSplitter = new WithoutSplitting();
    private Bounds _bounds;
    private Element[] _elements = [];
    private Point[] _points = [];
    
    public GridBuilder SetGridSplitter(IGridSplitter newSplitter)
    {
        _gridSplitter = newSplitter;
        return this;
    }

    public GridBuilder SetBounds(Bounds bounds)
    {
        _bounds = bounds;
        return this;
    }

    public GridBuilder SetElements(Element[] elements)
    {
        _elements = elements;
        return this;
    }

    public GridBuilder SetPoints(Point[] points)
    {
        _points = points;
        return this;
    }
    
    public Grid Build()
    {
        Grid grid = new(_bounds, _elements, _points);
        return _gridSplitter.Split(grid);
    }
}