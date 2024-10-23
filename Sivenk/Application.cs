using Sivenk.Builders;
using Sivenk.DataTypes;
using Sivenk.LinesFEM;
using Sivenk.LinesFEM.Readers;
using Sivenk.Paths;
using Sivenk.Splitters.GridSplitters;
using Sivenk.Writers;

namespace Sivenk;

public class Application
{
    public Grid Run(Configuration config)
    {
        Reader reader = new(config.inputPaths);
        InputData inputData = reader.Input();

        GridBuildingDataParser gridBuildingDataParser = new();
        GridBuildingData gridBuildingData = gridBuildingDataParser.Parse(inputData);

        IGridSplitter gridSplitter = config.shouldSplitGrid
            ? new DefaultGridSplitter(inputData.SplitX, inputData.SplitY)
            : new WithoutSplitting();

        GridBuilder builder = new();
        Grid grid =  builder
            .SetBounds(gridBuildingData.bounds)
            .SetElements(gridBuildingData.elements)
            .SetPoints(gridBuildingData.points)
            .SetGridSplitter(gridSplitter)
            .Build();

        IGridWriter outputer = new Writer(config.outputPaths);
        outputer.Print(grid);

        return grid;
    }
}