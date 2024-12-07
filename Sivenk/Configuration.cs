namespace Sivenk;

public class Configuration
{
    public string inputFolderName = "Default";
    private string _outputFolderName;
    
    
    public string outputFolderName
    {
        get => _outputFolderName ?? inputFolderName;
        set => _outputFolderName = value;
    }
    public bool shouldSplitGrid { get; init; }
    public bool openPythonProject { get; init; }
}