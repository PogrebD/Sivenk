using Sivenk.DataTypes;
using Sivenk.generators;
using Sivenk.Paths;

namespace Sivenk.Generators;

public class BC2Generator : IGenerator
{
    public void Generate(Grid grid)
    {
        bool isLeft = false;
        bool isRight = false;
        bool isTop = true;
        bool isDown = true;
        
        
        double left = -1;
        double right = 1;
        double top = 1;
        double down = -1;
        int n = 0;
        
        //all
        //n = (grid.dischargeFactor.NElemZ) * 2 + (grid.dischargeFactor.NElemR) * 2;

        //left right
        //n = (grid.Bounds.EdgesVerticalNumY) * 2;
        
        //top bot
        //n = (grid.Bounds.EdgesVerticalNumX) * 2;
        
        //left or right
        //n = (grid.Bounds.EdgesVerticalNumY) ;
        
        //0
        //n = 0;
        
        //left top
        //n = (grid.Bounds.EdgesHorizontalNumX) + (grid.Bounds.EdgesVerticalNumY);
        
        if (isLeft)
            n += grid.Bounds.EdgesVerticalNumY;
        if (isRight)
            n += grid.Bounds.EdgesVerticalNumY;
        if (isTop)
            n += grid.Bounds.EdgesHorizontalNumX;
        if (isDown)
            n += grid.Bounds.EdgesHorizontalNumX;
        
        File.WriteAllText(PathsProvider.BC2Folder, n.ToString() + "\n");

        if (isLeft)
        {
            for (int i = 0; i < grid.Bounds.EdgesVerticalNumY; i++)
            {
                //left
                var left1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1);
                var left2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1);
                string str = $"{left1Point} {left2Point} {left*CalcThetaX(left1Point, grid)} {left*CalcThetaX(left2Point, grid)}\n";
                File.AppendAllText(PathsProvider.BC2Folder, str);
            }
        }

        if (isRight)
        {
            for (int i = 0; i < grid.Bounds.EdgesVerticalNumY; i++)
            {
                //right
                var right1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
                var right2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
                string str2 = $"{right1Point} {right2Point} {right*CalcThetaX(right1Point, grid)} {right*CalcThetaX(right2Point, grid)}\n";
                File.AppendAllText(PathsProvider.BC2Folder, str2);
            }
        }

        if (isTop)
        {
            for (int i = 0; i < grid.Bounds.EdgesHorizontalNumX; i++)
            {
                //top
                var top1Point = (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i;
                var top2Point = (grid.Bounds.EdgesHorizontalNumX + 1) * grid.Bounds.EdgesVerticalNumY + i + 1;
                string str = $"{top1Point} {top2Point} {top*CalcThetaY(top1Point, grid)} {top*CalcThetaY(top2Point, grid)}\n";
                File.AppendAllText(PathsProvider.BC2Folder, str);
            }
        }

        if (isDown)
        {
            for (int i = 0; i < grid.Bounds.EdgesHorizontalNumX; i++)
            {
                //down
                var down1Point = i;
                var down2Point = i + 1;
                string str = $"{down1Point} {down2Point} {down*CalcThetaY(down1Point, grid)} {down*CalcThetaY(down2Point, grid)}\n";
                File.AppendAllText(PathsProvider.BC2Folder, str);
            }
        }
        
        
        
        
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
            //var left1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1);
           // var left2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1);
           // string str = $"{left1Point} {left2Point} {left*CalcThetaX(left1Point, grid)} {left*CalcThetaX(left2Point, grid)}\n";
           // File.AppendAllText(PathsProvider.BC2Folder, str);

            //right
          //  var right1Point = i * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
          //  var right2Point = (i + 1) * (grid.Bounds.EdgesHorizontalNumX + 1) + grid.Bounds.EdgesHorizontalNumX;
          //  string str2 = $"{right1Point} {right2Point} {right*CalcThetaX(right1Point, grid)} {right*CalcThetaX(right2Point, grid)}\n";
           // File.AppendAllText(PathsProvider.BC2Folder, str2);
        }
    }
    
    private double CalcThetaX(int indexPoint, Grid grid)
    {
        Func fun = new Func();
        var res = fun.Fun2krayX(grid.Points[indexPoint][0], grid.Points[indexPoint][1]);
        return res;
    }
    
    private double CalcThetaY(int indexPoint, Grid grid)
    {
        Func fun = new Func();
        var res = fun.Fun2krayY(grid.Points[indexPoint][0], grid.Points[indexPoint][1]);
        return res;
    }
}