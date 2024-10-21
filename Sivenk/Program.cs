﻿// See https://aka.ms/new-console-template for more information
using Sivenk;
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
GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

Grid grid = new Grid(gridBuildingData.bounds, gridBuildingData.elements, gridBuildingData.points);

ISplitter splitter = new IntegrialSplitter(inputData.SplitX, inputData.SplitY);
grid = splitter.Split(grid);

Outputer outputer = new Outputer();
outputer.Print(grid);
