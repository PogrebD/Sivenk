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

        return Multiply(Multiply(Matrix, _theta[thetaIndex]), L / 6);
    }

    public static double[] Multiply(double[] vector, double coefficient)
    {
        var result = new double[4];

        for (var i = 0; i < vector.Length / (vector.GetUpperBound(0) + 1); i++)
        {
            result[i] = vector[i] * coefficient;
        }

        return result;
    }

    public static double[] Multiply(double[,] matrix, double[] vector)
    {
        var result = new double[vector.Length];
        for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
        {
            for (var j = 0; j < matrix.Length / (matrix.GetUpperBound(0) + 1); j++)
            {
                result[i] += matrix[i, j] * vector[j];
            }
        }

        return result;
    }
}
