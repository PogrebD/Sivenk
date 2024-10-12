using Sivenk.Utils;

namespace Sivenk.Outputers;

public class Outputer
{
    private ElemsOutputer _elemsOutputer = new ElemsOutputer();
    private PointsOutputer _pointsOutputer = new PointsOutputer();

    public void Print(Grid grid)
    {
        _elemsOutputer.Print(grid.elements, Path.Combine(PathsProvider.OutputPath, FilesProvider.OutputElements));
        _pointsOutputer.Print(grid.points, Path.Combine(PathsProvider.OutputPath, FilesProvider.OutputPoints));
    }
}