using MathLibrary.DataTypes;
using Sivenk.DataTypes;
using Sivenk.Enumerators;
using Sivenk.Splitters.GridSplitters;

namespace Sivenk.Builders;

public class GridBuilder
{
    private IGridSplitter _gridSplitter = new WithoutSplitting();
    private readonly EdgeEnumerator _edgeEnumerator = new EdgeEnumerator();
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
        Grid splittedGrid = _gridSplitter.Split(grid);
        
        _edgeEnumerator.EnumerateEdges(splittedGrid);
        return splittedGrid;
    }

    
}