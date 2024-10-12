using Sivenk.Utils;

namespace Sivenk.Outputers;

public class Outputer
{
    private ElemsOutputer _elemsOutputer = new ElemsOutputer();
    private PointsOutputer _pointsOutputer = new PointsOutputer();

    public void Print(Grid grid)
    {
        using var pointsWriter = new StreamWriter(Path.Combine(PathsProvider.OutputPath, FilesProvider.OutputPoints));
        using var elemWriter = new StreamWriter(Path.Combine(PathsProvider.OutputPath, FilesProvider.OutputElements));
        
        _pointsOutputer.Print(grid.points, pointsWriter);
        _elemsOutputer.Print(grid.elements, elemWriter);
    }
}