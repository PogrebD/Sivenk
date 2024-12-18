using System.Globalization;
using Sivenk;
using Sivenk.DataTypes;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new()
{
    openPythonProject = true,
    shouldSplitGrid = true,
    inputFolderName = "txt",
    outputFolderName = "output",
};

Application app = new();
Grid grid;

app.Run(configuration, out grid);

//LocalMatrix localMatrix = new LocalMatrix((double x, double y) => x + y);

grid.GetEdgeId(22, 23);
grid.GetPointsId(20);
grid.GetEdgeIds(3);