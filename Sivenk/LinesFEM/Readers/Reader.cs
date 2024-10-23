using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Readers;

public class Reader : IInputDataReader
{
    private readonly AreaReader _areaReader = new();
    private readonly MaterialReader _matReader = new();
    private readonly LineReader _lineReader = new();
    private readonly SplitReader _splitReader = new();
    
    private readonly string[] _paths;

    public Reader(string[] paths)
    {
        _paths = paths;
    }
    
    public InputData Input()
    {
        using var gridReader = new StreamReader(_paths[0]);
        using var materialReader = new StreamReader(_paths[1]);

        var lines = _lineReader.Input(gridReader);
        var area = _areaReader.Input(gridReader);
        var splitX = _splitReader.Input(gridReader);
        var splitY = _splitReader.Input(gridReader);
        var materials = _matReader.Input(materialReader);

        return new InputData
        {
            Lines = lines,
            Area = area,
            SplitX = splitX,
            SplitY = splitY,
            Material = materials 
        };
    }
}
