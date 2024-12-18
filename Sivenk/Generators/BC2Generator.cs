using Sivenk.DataTypes;
using Sivenk.generators;
using Sivenk.Paths;

namespace Sivenk.Generators;

public class BC2Generator : IGenerator
{
    public void Generate(Grid grid)
    {
        
        double left = -1;
        double right = 1;
        double top = 0;
        double down = 0;

        //all
        //int n = (grid.dischargeFactor.NElemZ) * 2 + (grid.dischargeFactor.NElemR) * 2;

        //left right
        int n = (grid.Bounds.EdgesVerticalNumY) * 2;
        
        //left or right
        //int n = (grid.Bounds.EdgesVerticalNumY) ;
        
        //0
        //int n = 0;
        
        //left top
        //int n = (grid.Bounds.EdgesHorizontalNumX) + (grid.Bounds.EdgesVerticalNumY);
        
        File.WriteAllText(PathsProvider.BC2Folder, n.ToString() + "\n");
        for (int i = 0; i < grid.Bounds.EdgesHorizontalNumX; i++)
        {
            //down
            //string str = string.Format("{0} {1} {2}\n", i, i + 1, down);
            //File.AppendAllText(Config.bc2Path, str);

            //top
            //string str2 = string.Format("{0} {1} {2} {3}\n", (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i, (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i + 1, top, top);
            //File.AppendAllText(PathsProvider.BC2Folder, str2);
        }

        for (int i = 0; i < grid.Bounds.EdgesVerticalNumY; i++)
        {
            //left
            var left1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1);
            var left2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1);
            string str = $"{left1Point} {left2Point} {left*CalcTheta(left1Point, grid)} {left*CalcTheta(left2Point, grid)}\n";
            File.AppendAllText(PathsProvider.BC2Folder, str);

            //right
            var right1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
            var right2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
            string str2 = $"{right1Point} {right2Point} {right*CalcTheta(right1Point, grid)} {right*CalcTheta(right2Point, grid)}\n";
            File.AppendAllText(PathsProvider.BC2Folder, str2);
        }
    }
    
    private double CalcTheta(int indexPoint, Grid grid)
    {
        Func fun = new Func();
        var res = fun.Fun2kray(grid.Points[indexPoint][0], grid.Points[indexPoint][1]);
        return res;
    }
}