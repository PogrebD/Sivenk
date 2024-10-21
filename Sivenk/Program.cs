// See https://aka.ms/new-console-template for more information
using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Inputers;
using Sivenk.Outputers;
using Sivenk.Paths;
using Sivenk.Splitters;

Inputer inputer = new();
InputData inputData = inputer.Input(PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath);

GridBuildingDataParser gridBuildingDataParser = new GridBuildingDataParser();
GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

GridBuilder builder = new();
Grid grid =  builder
    .SetBounds(gridBuildingData.bounds)
    .SetElements(gridBuildingData.elements)
    .SetPoints(gridBuildingData.points)
    .SetGridSplitter(new DischargeSplitter(inputData.SplitX, inputData.SplitY))
    .Build();

Outputer outputer = new();
outputer.Print(grid);
