
using Sivenk.DataTypes;
using Sivenk.generators;
using Sivenk.Paths;

namespace Sivenk.Generators
{
    internal class BC1Generator : IGenerator
    {
        public void Generate(Grid grid)
        {
            double left = 0;
            double right = 0;
            double top = 0;
            double down = 0;

            //all
            //int n = (grid.Bounds.EdgesHorizontalNumX) * 2 + (grid.Bounds.EdgesVerticalNumY) * 2;

            //left right
            //int n = (grid.dischargeFactor.NElemZ) * 2;
            
            //right down
            //int n = (grid.Bounds.EdgesHorizontalNumX) + (grid.Bounds.EdgesVerticalNumY);
            
            //Top down
            int n = (grid.Bounds.EdgesHorizontalNumX)*2;
            
            //Top down (right or left)
            //int n = (grid.Bounds.EdgesHorizontalNumX)*2+ (grid.Bounds.EdgesVerticalNumY);
            
            File.WriteAllText(PathsProvider.BC1Folder, n.ToString() + "\n");
            for (int i = 0; i < grid.Bounds.EdgesHorizontalNumX; i++)
            {
                //down
                string str = string.Format("{0} {1} {2}\n", i, i + 1, down);
                File.AppendAllText(PathsProvider.BC1Folder, str);

                //top
                string str2 = string.Format("{0} {1} {2}\n", (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i, (grid.Bounds.EdgesHorizontalNumX+ 1) * grid.Bounds.EdgesVerticalNumY + i + 1, top);
                File.AppendAllText(PathsProvider.BC1Folder, str2);
            }

            for (int i = 0; i < grid.Bounds.EdgesVerticalNumY; i++)
            {
                //left
                //string str = string.Format("{0} {1} {2}\n", i * (grid.Bounds.EdgesHorizontalNumX + 1), (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1), left);
                //File.AppendAllText(PathsProvider.BC1Folder, str);

                //right
                //string str2 = string.Format("{0} {1} {2}\n", i * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX, (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX, right);
                //File.AppendAllText(PathsProvider.BC1Folder, str2);
            }
        }
    }
}
