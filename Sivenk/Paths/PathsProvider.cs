﻿namespace Sivenk.Paths;

public static class PathsProvider
{
    public static readonly string BasePath = "../../../";

    public static readonly string InputFolder = Path.Combine(BasePath);
    public static readonly string OutputFolder = Path.Combine(BasePath);
    public static readonly string BC1Folder = Path.Combine(BasePath, "BCFolder", "Bc1.txt");
    public static readonly string BC2Folder = Path.Combine(BasePath, "BCFolder", "Bc2.txt");
}