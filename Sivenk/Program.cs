using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Readers;
using Sivenk.Paths;
using Sivenk.Splitters;
using Sivenk.Writers;
using System.Diagnostics;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Reader reader = new();
InputData inputData = reader.Input(PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath);

GridBuildingDataParser gridBuildingDataParser = new();
GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

GridBuilder builder = new();
Grid grid = builder
    .SetBounds(gridBuildingData.bounds)
    .SetElements(gridBuildingData.elements)
    .SetPoints(gridBuildingData.points)
    .SetGridSplitter(
                    // new DefaultSplitter()
                    // new IntegralSplitter(inputData.SplitX, inputData.SplitY)
                    new DischargeSplitter(inputData.SplitX, inputData.SplitY)
                    )
    .Build();

Writer outputer = new();
outputer.Print(grid);

ShowGrid("../../../../GridView/GridView.py", "../../../output/points.txt", "../../../output/elements.txt");

static void ShowGrid(String filePath, string? pointsPath = null, string? elementsPath = null)
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