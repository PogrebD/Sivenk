using System.Diagnostics;
using MathLibrary.DataTypes;
using Sivenk.BoundaryConditions;
using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.generators;
using Sivenk.InputBC;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Readers;
using Sivenk.Paths;
using Sivenk.Splitters.GridSplitters;
using Sivenk.Writers;

namespace Sivenk;

public class Application
{
    public void Run(Configuration config, out Grid grid)
    {
        string[] inputPaths = [
            Path.Combine(PathsProvider.InputFolder, config.inputFolderName, "input.txt"),
            Path.Combine(PathsProvider.InputFolder, config.inputFolderName, "material.txt")
        ];

        
        IInputDataReader reader = new Reader(inputPaths);
        InputData inputData = reader.Input();

        GridBuildingDataParser gridBuildingDataParser = new();
        GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

        IGridSplitter gridSplitter = config.shouldSplitGrid
            ? new DefaultGridSplitter(inputData.SplitX, inputData.SplitY)
            : new WithoutSplitting();

        GridBuilder builder = new();
        grid = builder
            .SetBounds(gridBuildingData.bounds)
            .SetElements(gridBuildingData.elements)
            .SetPoints(gridBuildingData.points)
            .SetGridSplitter(gridSplitter)
            .Build();

        
        Generator generator = new Generator();
        generator.Generate(grid);
        BoundaryConditionsProvider provider = new BoundaryConditionsProvider();
        BcInputer inputerBc = new BcInputer();
        inputerBc.Input(provider);
        
        DirectTask(grid, provider); 
        
        string[] outputPaths = [
            Path.Combine(PathsProvider.OutputFolder, config.outputFolderName, "points.txt"),
            Path.Combine(PathsProvider.OutputFolder, config.outputFolderName, "elements.txt")
        ];
        
        IGridWriter outputer = new Writer(outputPaths);
        outputer.Print(grid);

        if (config.openPythonProject)
        {
            ShowGrid("../../../../GridView/GridView.py", "../../../output/points.txt", "../../../output/elements.txt");
        }
    }

    void DirectTask(Grid grid, BoundaryConditionsProvider providerBC)
    {
        LocalMatrix localMatrices = new(grid);
        GlobalMatrix globalMatrices = new(grid);

        // краевые и слау
        providerBC.Applay(globalMatrices,grid);
        Slau slau = new(globalMatrices, grid.Points.Length);
        
        Point[] Points = [new Point(2,3)];
        PrintFileResultPointST(slau.q, grid, Points);
    }

    static void PrintFileResultPointST(double[] result, Grid grid, Point[] points)
    {
        var resulter = new ResultInPoint(grid, result);
        for (var i = 0; i < points.Length / 2; i++)
        {
            var res = resulter.Calculate(points[i]);
            var str = $"{res}\t";

            File.AppendAllText(  PathsProvider.outFolder , str);
        }
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