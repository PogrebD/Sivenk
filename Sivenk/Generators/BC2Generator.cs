using Sivenk.DataTypes;
using Sivenk.generators;
using Sivenk.Paths;

namespace Sivenk.Generators;

public class BC2Generator : IGenerator
{
    public void Generate(Grid grid)
    {
        double left = 0;
        double right = 0;
        double top = 0;
        double down = 0;

        //all
        //int n = (grid.dischargeFactor.NElemZ) * 2 + (grid.dischargeFactor.NElemR) * 2;

        //left right
        //int n = (grid.dischargeFactor.NElemZ) * 2;
            
        //left top
        int n = (grid.Bounds.EdgesHorizontalNumX) + (grid.Bounds.EdgesVerticalNumY);
        
        File.WriteAllText(PathsProvider.BC2Folder, n.ToString() + "\n");
        for (int i = 0; i < grid.Bounds.EdgesHorizontalNumX; i++)
        {
            //down
            //string str = string.Format("{0} {1} {2}\n", i, i + 1, down);
            //File.AppendAllText(Config.bc2Path, str);

            //top
            string str2 = string.Format("{0} {1} {2} {3}\n", (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i, (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i + 1, top, top);
            File.AppendAllText(PathsProvider.BC2Folder, str2);
        }

        for (int i = 0; i < grid.Bounds.EdgesVerticalNumY; i++)
        {
            //left
            string str = string.Format("{0} {1} {2} {3}\n", i * (grid.Bounds.EdgesHorizontalNumX + 1), (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1), left, left);
            File.AppendAllText(PathsProvider.BC2Folder, str);

            //right
            //string str2 = string.Format("{0} {1} {2}\n", i * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, (i + 1) * (grid.dischargeFactor.NElemR + 1) + grid.dischargeFactor.NElemR, right);
            //File.AppendAllText(Config.bc2Path, str2);
        }
    }
}