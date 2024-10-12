// See https://aka.ms/new-console-template for more information
using Sivenk;
using Sivenk.Inputers;

const string BasePath = "../../../txt/";

Console.WriteLine("Hello, World!");

var inputer = new Inputer();
var result = inputer.Input(BasePath + "input.txt", BasePath + "material.txt");


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

LinesGridBuilder linesGridBuilder = new();
linesGridBuilder.Build();

int x = 224;
