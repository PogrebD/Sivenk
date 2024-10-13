// See https://aka.ms/new-console-template for more information
using Sivenk;
using Sivenk.Builders;
using Sivenk.Constructors;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Inputers;
using Sivenk.Outputers;
using Sivenk.Paths;
using Sivenk.Splitters;

/*
Console.WriteLine("Points: ");
for (int i = 0; i < result.Points.GetLength(0); i++)
{
    for (int j = 0; j < result.Points.GetLength(1); j++)
    {
        Console.Write(result.Points[i, j] + " ");
    }
    
    Console.WriteLine();
}

Console.WriteLine("Material: ");
*/

Inputer inputer = new();
InputData inputData = inputer.Input(PathsProvider.InputGigaPath, PathsProvider.InputMaterialPath);

GridBuildingDataParser gridBuildingDataParser = new GridBuildingDataParser();
GridBuildingData data = gridBuildingDataParser.Parse(inputData);

GridBuilder builder = new GridBuilder();
Grid grid = builder
    .SetGridConstructor(new DefaultConstructor())
    .SetGridSplitter(new IntegrialSplitter(2, 2))
    .Build(data);

Outputer outputer = new Outputer();
outputer.Print(grid);
