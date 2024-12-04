using MathLibrary.DataTypes;
using Sivenk.DataTypes;

namespace Sivenk;

public class Result
{
    public double ResultInPoint(Grid grid, double[] result, double x, double y)
    {
        int IndexNode = grid.Points.ToList().FindIndex((Point node) => node[0] > x && node[1] > y);
        int index = grid.Elements.ToList().FindIndex((Element elem) => elem.IdPoints[3] == IndexNode);

        double x1 = grid.Points[grid.Elements[index].IdPoints[0]][0];
        double x2 = grid.Points[grid.Elements[index].IdPoints[3]][0];
        double y1 = grid.Points[grid.Elements[index].IdPoints[0]][1];
        double y2 = grid.Points[grid.Elements[index].IdPoints[3]][1];


        double psi0 = ((x2 - x) / (x2 - x1)) * ((y2 - y) / (y2 - y1));
        double psi1 = ((x - x1) / (x2 - x1)) * ((y2 - y) / (y2 - y1));
        double psi2 = ((x2 - x) / (x2 - x1)) * ((y - y1) / (y2 - y1));
        double psi3 = ((x - x1) / (x2 - x1)) * ((y - y1) / (y2 - y1));

        double res = psi0 * result[grid.Elements[index].IdPoints[0]] + psi1 * result[grid.Elements[index].IdPoints[1]] + psi2 * result[grid.Elements[index].IdPoints[2]] + psi3 * result[grid.Elements[index].IdPoints[3]];

        return res;
    }
}