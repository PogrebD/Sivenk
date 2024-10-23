using System.Globalization;
using Sivenk;
using Sivenk.Paths;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new Configuration()
{
    shouldSplitGrid = true,
    inputPaths = [PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath],
    outputPaths = [PathsProvider.OutputPointsPath, PathsProvider.OutputElementsPath],
};

Application app = new Application();
app.Run(configuration);
