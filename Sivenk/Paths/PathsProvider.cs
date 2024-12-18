namespace Sivenk.Paths;

public static class PathsProvider
{
    public static readonly string BasePath = "../../../";

    public static readonly string InputFolder = Path.Combine(BasePath);
    public static readonly string OutputFolder = Path.Combine(BasePath);
    public static readonly string BC1Folder = Path.Combine(BasePath, "BCFolder", "Bc1.txt");
    public static readonly string BC2Folder = Path.Combine(BasePath, "BCFolder", "Bc2.txt");
    public static readonly string outFolder = Path.Combine(BasePath, "output", "ResultInPoints.txt");
    public static readonly string outAllFolder = Path.Combine(BasePath, "output", "Result.txt");
    public static readonly string trueFolder = Path.Combine(BasePath, "output", "true.txt");
}