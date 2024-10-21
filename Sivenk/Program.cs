using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Readers;
using Sivenk.Paths;
using Sivenk.Splitters;
using Sivenk.Writers;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Reader reader = new();
InputData inputData = reader.Input(PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath);

GridBuildingDataParser gridBuildingDataParser = new();
GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

GridBuilder builder = new();
Grid grid =  builder
    .SetBounds(gridBuildingData.bounds)
    .SetElements(gridBuildingData.elements)
    .SetPoints(gridBuildingData.points)
    .SetGridSplitter(new DischargeSplitter(inputData.SplitX, inputData.SplitY))
    .Build();

Writer outputer = new();
outputer.Print(grid);
