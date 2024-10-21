using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Readers;

public class Reader
{
    private readonly AreaReader _areaReader = new();
    private readonly MaterialReader _matReader = new();
    private readonly LineReader _lineReader = new();
    private readonly SplitReader _splitReader = new();
    
    public InputData Input(string fileNameGl, string fileNameMat)
    {
        using var materialReader = new StreamReader(fileNameMat);
        using var gridReader = new StreamReader(fileNameGl);

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
