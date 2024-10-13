using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Inputers;

public class Inputer
{
    private readonly AreaInputer _areaInputer = new();
    private readonly MatInputer _matInputer = new();
    private readonly PointInputer _pointInputer = new();
    private readonly SplitInputer _splitInputer = new();

    public InputData Input(string filaeNameGl, string filaeNameMat)
    {
        using var materialReader = new StreamReader(filaeNameMat);
        using var gridReader = new StreamReader(filaeNameGl);

        var poins = _pointInputer.Input(gridReader);
        var area = _areaInputer.Input(gridReader);
        var splitX = _splitInputer.Input(gridReader);
        var splitY = _splitInputer.Input(gridReader);
        var materials = _matInputer.Input(materialReader);

        return new InputData
        {
            Points = poins,
            Area = area,
            SplitX = splitX,
            SplitY = splitY,
            Material = materials 
        };
    }
}
