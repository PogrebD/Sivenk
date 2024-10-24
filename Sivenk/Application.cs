using System.Diagnostics;
using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Readers;
using Sivenk.Paths;
using Sivenk.Splitters.GridSplitters;
using Sivenk.Writers;

namespace Sivenk;

public class Application
{
    public Grid Run(Configuration config)
    {
        IInputDataReader reader = new Reader(config.inputPaths);
        InputData inputData = reader.Input();

        GridBuildingDataParser gridBuildingDataParser = new();
        GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

        IGridSplitter gridSplitter = config.shouldSplitGrid
            ? new DefaultGridSplitter(inputData.SplitX, inputData.SplitY)
            : new WithoutSplitting();

        GridBuilder builder = new();
        Grid grid = builder
            .SetBounds(gridBuildingData.bounds)
            .SetElements(gridBuildingData.elements)
            .SetPoints(gridBuildingData.points)
            .SetGridSplitter(gridSplitter)
            .Build();

        IGridWriter outputer = new Writer(config.outputPaths);
        outputer.Print(grid);

        if (config.shouldSplitGrid)
        {
            ShowGrid("../../../../GridView/GridView.py", "../../../output/points.txt", "../../../output/elements.txt");
        }

        return grid;
    }
    
    void ShowGrid(String filePath, string? pointsPath = null, string? elementsPath = null)
    {
        ProcessStartInfo prog = new()
        {
            FileName = "python",
            Arguments = $"{filePath} {pointsPath} {elementsPath}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        using var process = Process.Start(prog);
        using var reader = process?.StandardOutput;
        string result = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        Console.WriteLine(result);
        if (!string.IsNullOrEmpty(error))
        {
            Console.WriteLine($"Error: {error}");
        }
    }
}