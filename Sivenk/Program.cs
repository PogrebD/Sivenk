using System.Diagnostics;
using System.Globalization;
using Sivenk;
using Sivenk.DataTypes;
using Sivenk.Paths;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new Configuration()
{
    openPythonProject = false,
    shouldSplitGrid = true,
    inputFolderName = "txt",
    outputFolderName = "output",
};

Application app = new Application();
Grid grid = app.Run(configuration);
