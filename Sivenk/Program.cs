using System.Diagnostics;
using System.Globalization;
using Sivenk;
using Sivenk.DataTypes;
using Sivenk.Paths;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new Configuration()
{
    shouldSplitGrid = true,
    inputPaths = [PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath],
    outputPaths = [PathsProvider.OutputPointsPath, PathsProvider.OutputElementsPath],
};

Application app = new Application();
Grid grid = app.Run(configuration);
//grid.GetEdgeId(13, 14);
//grid.GetEdgeId(21, 22);

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

//points_path = sys.argv[1] if len(sys.argv) > 1 else '../Sivenk/output/points.txt'
//elements_path = sys.argv[2] if len(sys.argv) > 1 else '../Sivenk/output/elements.txt'
