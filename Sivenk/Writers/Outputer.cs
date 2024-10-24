using Sivenk.DataTypes;

namespace Sivenk.Writers;

public class Writer : IGridWriter
{
    private readonly ElemsWriter _elemsWriter = new();
    private readonly PointsWriter _pointsWriter = new();

    private readonly string[] _paths;

    public Writer(string[] paths)
    {
        _paths = paths;
    }

    public void Print(Grid grid)
    {
        var directoryPath = Path.GetDirectoryName(_paths[0]);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        
        using var pointsWriter = new StreamWriter(_paths[0]);
        using var elemWriter = new StreamWriter(_paths[1]);
        
        _pointsWriter.Print(grid.Points, pointsWriter);
        _elemsWriter.Print(grid.Elements, elemWriter);
    }
}