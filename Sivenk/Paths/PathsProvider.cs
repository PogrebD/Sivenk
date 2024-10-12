namespace Sivenk.Utils;

public static class PathsProvider
{
    public static readonly string InputGigaPath = Path.Combine(FoldersProvider.InputFolder, FilesProvider.GigaInput);
    public static readonly string InputMaterialPath = Path.Combine(FoldersProvider.InputFolder, FilesProvider.InputMaterial);
    
    public static readonly string OutputElementsPath = Path.Combine(FoldersProvider.OutputFolder, FilesProvider.OutputElements);
    public static readonly string OutputPointsPath = Path.Combine(FoldersProvider.OutputFolder, FilesProvider.OutputPoints);
}