namespace Sivenk.Paths;

public static class PathsProvider
{
    public static readonly string BasePath = "../../../";

    public static readonly string InputFolder = Path.Combine(BasePath, "txt");
    public static readonly string OutputFolder = Path.Combine(BasePath, "output");
    
    public static readonly string InputGigaPath = Path.Combine(InputFolder, "input.txt");
    public static readonly string InputMaterialPath = Path.Combine(InputFolder, "material.txt");
    
    public static readonly string OutputElementsPath = Path.Combine(OutputFolder, "elements.txt");
    public static readonly string OutputPointsPath = Path.Combine(OutputFolder, "points.txt");
}