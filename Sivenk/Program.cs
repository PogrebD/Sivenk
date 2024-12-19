using System.Globalization;
using Sivenk;
using Sivenk.DataTypes;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Configuration configuration = new()
{
    openPythonProject = false,
    shouldSplitGrid = true,
    inputFolderName = "txt",
    outputFolderName = "output",
};

Application app = new();

app.Run(configuration, out var grid);

int[] inputPoints = [22, 23];
int inputEdgeId = 20;
int inputElementId = 3;

var pointsId = grid.GetPointsId(inputEdgeId);
var edgeId = grid.GetEdgeId(inputPoints[0], inputPoints[1]);
var edgesIds = grid.GetEdgeIds(inputElementId);
var elementIds = grid.GetElementIds(26);

Console.WriteLine($"Input Edge: {inputEdgeId} => Points: {pointsId[0]}, {pointsId[1]}");
Console.WriteLine($"Input points: {inputPoints[0]}, {inputPoints[1]} => EdgeId: {edgeId}");
Console.WriteLine($"Input {nameof(inputElementId)}: {inputElementId} => EdgeId: {edgesIds[0]}, {edgesIds[1]}, {edgesIds[2]}, {edgesIds[3]}");
