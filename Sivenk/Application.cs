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
            .SetMaterials(gridBuildingData.materials)
            .SetGridSplitter(gridSplitter)
            .Build();

        Generator generator = new Generator();
        generator.Generate(grid);
        BoundaryConditionsProvider provider = new BoundaryConditionsProvider();
        BcInputer inputerBc = new BcInputer();
        inputerBc.Input(provider);
        
        string[] outputPaths = [
            Path.Combine(PathsProvider.OutputFolder, config.outputFolderName, "points.txt"),
            Path.Combine(PathsProvider.OutputFolder, config.outputFolderName, "elements.txt")
        ];
        
        IGridWriter outputer = new Writer(outputPaths);
        outputer.Print(grid);
        DirectTask(grid, provider); 
        if (config.openPythonProject)
        {
            ShowPythonProject("../../../../SivenkMaterial/main.py", "../../../output/points.txt", "../../../output/elements.txt");
            ShowPythonProject("../../../../SivenkGradient/main.py", "../../../output/points.txt", "../../../output/elements.txt");
        }
    }

    void DirectTask(Grid grid, BoundaryConditionsProvider providerBC)
    {
        LocalMatrix localMatrices = new(grid);
        GlobalMatrix globalMatrices = new(grid);

        // краевые и слау
        providerBC.Applay(globalMatrices,grid);
        Slau slau = new(globalMatrices, grid.Points.Length);
        
        Point[] Points = [new Point(3.3 , 2.3)];
        PrintFileResultPointST(slau.q, grid, Points);
        PrintFileResult(slau.q, grid);
        PrintFileTrueResult(grid);
    }

    static void PrintFileResultPointST(double[] result, Grid grid, Point[] points)
    {
        File.WriteAllText(PathsProvider.outFolder,"");
        var resulter = new ResultInPoint(grid, result);
        Func fun = new Func();
        for (var i = 0; i < points.Length; i++)
        {
            var res = resulter.Calculate(points[i]);
            var resTrue = fun.FunU(points[i].R, points[i].Z);
            var str = $"{res}\n{resTrue}";

            File.AppendAllText(  PathsProvider.outFolder , str);
        }
    }
    
    static void PrintFileResult(double[] result, Grid grid)
    {
        File.WriteAllText(PathsProvider.outAllFolder,"");
        /*for (int i = grid.Bounds.PointsNumY- 1; i >=0 ; i--)
        {
            for (int j = 0; j < grid.Bounds.PointsNumX; j++)
            {
                var str = $"{result[j+i*(grid.Bounds.PointsNumX)]}\t";
                File.AppendAllText(  PathsProvider.outAllFolder , str);
            }
        }*/

        for (int i = 0; i < result.Length; i++)
        {
            var str = $"{grid.Points[i].R} {grid.Points[i].Z} {result[i]}\n";
            File.AppendAllText(PathsProvider.outAllFolder, str);
        }
    }
    
    static void PrintFileTrueResult(Grid grid)
    {
        File.WriteAllText(PathsProvider.trueFolder,"");
        Func fun = new Func();
        for (int i = 0; i < grid.Points.Length; i++)
        {
            var str2 = $"{fun.FunU(grid.Points[i][0], grid.Points[i][1])}\n";
            File.AppendAllText(PathsProvider.trueFolder, str2);
        }
    }
    
    void ShowPythonProject(String filePath, string? pointsPath = null, string? elementsPath = null)
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