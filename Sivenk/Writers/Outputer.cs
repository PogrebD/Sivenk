using Sivenk.DataTypes;
using Sivenk.Paths;

namespace Sivenk.Writers;

public class Writer
{
    private readonly ElemsWriter _elemsWriter = new();
    private readonly PointsWriter _pointsWriter = new();

    public void Print(Grid grid)
    {
        using var pointsWriter = new StreamWriter(PathsProvider.OutputPointsPath);
        using var elemWriter = new StreamWriter(PathsProvider.OutputElementsPath);
        
        _pointsWriter.Print(grid.Points, pointsWriter);
        _elemsWriter.Print(grid.Elements, elemWriter);
    }
}