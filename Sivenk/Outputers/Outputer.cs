using Sivenk.DataTypes;
using Sivenk.Paths;

namespace Sivenk.Outputers;

public class Outputer
{
    private ElemsOutputer _elemsOutputer = new ElemsOutputer();
    private PointsOutputer _pointsOutputer = new PointsOutputer();

    public void Print(Grid grid)
    {
        using var pointsWriter = new StreamWriter(PathsProvider.OutputPointsPath);
        using var elemWriter = new StreamWriter(PathsProvider.OutputElementsPath);
        
        _pointsOutputer.Print(grid.points, pointsWriter);
        _elemsOutputer.Print(grid.elements, elemWriter);
    }
}