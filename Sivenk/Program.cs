using System.Diagnostics;
using System.Globalization;
using Sivenk;
using Sivenk.DataTypes;
using Sivenk.Paths;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new Configuration()
{
    shouldSplitGrid = false,
    inputPaths = [PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath],
    outputPaths = [PathsProvider.OutputPointsPath, PathsProvider.OutputElementsPath],
};

Application app = new Application();
Grid grid = app.Run(configuration);
grid.GetElementIds(37);
grid.GetElementIds(5);

#if false
ShowGrid("../../../../GridView/GridView.py", "../../../output/points.txt", "../../../output/elements.txt");
#endif

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
