namespace Sivenk.DataTypes;

public class LocalMatrix
{
    private readonly Func<double, double, double> _rightFunction;
    private readonly Grid _grid;
    public LocalMatrix(Func<double, double, double> rightFunction, Grid grid)
    {
        _rightFunction = rightFunction;
        _grid = grid;
    }
    
    public void CalcLocalMatrices()
    {
        for (int i = 0; i < _grid.Elements.Count(); i++)
        {
            double hx = _grid.Points[_grid.Elements[i].IdPoints[1]][0] - _grid.Points[_grid.Elements[i].IdPoints[0]][0];
            double hy = _grid.Points[_grid.Elements[i].IdPoints[2]][1] - _grid.Points[_grid.Elements[i].IdPoints[0]][1];
            _grid.Elements[i] = new Element(
                CalcMassMatrix(_grid.Elements[i], hx, hy, _grid.Materials[_grid.Elements[i].Material].gamma),
                CalcStiffnesMatrix(_grid.Elements[i], hx, hy, _grid.Materials[_grid.Elements[i].Material].Lambda),
                CalcBVector(_grid.Elements[i], hx, hy), _grid.Elements[i]);
        }
    }

    private double[] CalcBVector(Element gridElement, double hx, double hy)
    {
        var VectorF = new double[4];

        for (var i = 0; i < VectorF.Count(); i++)
        {
            VectorF[i] = _rightFunction(_grid.Points[gridElement.IdPoints[i]][0],_grid.Points[gridElement.IdPoints[i]][1]);
        }

        return Multiply(CalcMassMatrixWithoutGamma(gridElement, hx, hy), VectorF);
    }

    private double[,] CalcMassMatrix(Element gridElement, double hx, double hy, double gamma)
    {
        return Multiply(CalcMassMatrixWithoutGamma(gridElement, hx, hy), gamma);
    }

    private double[,] CalcMassMatrixWithoutGamma(Element gridElement, double hx, double hy)
    {
        return Multiply(GetMassMatrix(), (hx * hy / 36));
    }

    private double[,] CalcStiffnesMatrix(Element gridElement, double hx, double hy, double lambda)
    {
        return Sum(Multiply(GetStiffnes1Matrix(), (hy * lambda / hx * 6)),
            Multiply(GetStiffnes2Matrix(), (hx * lambda / hy * 6)));
    }


    public double[,] GetStiffnes1Matrix() => new double[4, 4]
        { { 2d, -2d, 1d, -1d }, { -2d, 2d, -1d, 1d }, { 1d, -1d, 2d, -2d }, { -1d, 1d, -2d, 2d } };

    public double[,] GetStiffnes2Matrix() => new double[4, 4]
        { { 2d, 1d, -2d, -1d }, { 1d, 2d, -1d, -2d }, { -2d, -1d, 2d, 1d }, { -1d, -2d, 2d, 2d } };

    public double[,] GetMassMatrix() => new double[4, 4]
        { { 4d, 2d, 2d, 1d }, { 2d, 4d, 1d, 2d }, { 2d, 1d, 4d, 2d }, { 1d, 2d, 2d, 4d } };

    public static double[,] Multiply(double[,] matrix, double coefficient)
    {
        var result = new double[4, 4];
        for (var i = 0; i < matrix.GetUpperBound(0) + 1; i++)
        {
            for (var j = 0; j < matrix.Length / (matrix.GetUpperBound(0) + 1); j++)
            {
                result[i, j] = matrix[i, j] * coefficient;
            }
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

    public static double[,] Sum(double[,] matrix1, double[,] matrix2)
    {
        var result = new double[4, 4];
        for (var i = 0; i < matrix1.GetUpperBound(0) + 1; i++)
        {
            for (var j = 0; j < matrix1.Length / (matrix1.GetUpperBound(0) + 1); j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }

        return result;
    }
}