namespace Sivenk.Utils;

public static class PathsProvider
{
    public static readonly string BasePath = "../../../";
    public static readonly string InputPath = Path.Combine(BasePath + FoldersProvider.InputFolder);
    public static readonly string OutputPath = Path.Combine(BasePath + FoldersProvider.OutputFolder);
}