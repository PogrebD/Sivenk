using MathLibrary.DataTypes;
using Sivenk.DataTypes;

namespace Sivenk.BoundaryConditions;

public class SecondBoundaryConditions
{
    private readonly IReadOnlyList<int[]> _nodeIndices;
    private readonly IReadOnlyList<double[]> _theta;

    private readonly int _countEdge;

    public SecondBoundaryConditions(List<int[]> nodeIndices, List<double[]> theta, int nBc)
    {
        _nodeIndices = nodeIndices;
        _theta = theta;
        _countEdge = nBc;
    }

    public void Apply(GlobalMatrix globalMatrices, Grid grid)
    {
        for (int i = 0; i < _countEdge; i++)
        {
            var vector = CalcVector(grid.Points[_nodeIndices[i][0]], grid.Points[_nodeIndices[i][1]], i);

            globalMatrices._globalVectorB[_nodeIndices[i][0]] += vector[0];
            globalMatrices._globalVectorB[_nodeIndices[i][1]] += vector[1];
        }
    }

    private IList<double> CalcVector(Point pointQ, Point pointP, int thetaIndex)
    {
        var L = double.Sqrt(double.Pow(pointQ.R - pointP.R, 2) + double.Pow(pointQ.Z - pointP.Z, 2));

        double[,] Matrix =
        {
           { 2, 1},
           { 1, 2}
        };

        return UtilsLibrary.Multiply(UtilsLibrary.Multiply(Matrix, _theta[thetaIndex]), L / 6);
    }
}
