using MathLibrary.DataTypes;
using MathLibrary.DataTypes.Internal;

namespace Sivenk.DataTypes;

public class LocalMatrix
{
    private readonly Grid _grid;
    private readonly Gauss2D _gauss2D;

    public LocalMatrix(Grid grid)
    {
        _grid = grid;
    }

    double W(double t, int n) => n == 1 ? 1 - t : t;

    double Fi(double psi, double nu, int n)
    {
        switch (n)
        {
            case 1:
                return W(psi, 1) * W(nu, 1);
            case 2:
                return W(psi, 2) * W(nu, 1);
            case 3:
                return W(psi, 1) * W(nu, 2);
            case 4:
                return W(psi, 2) * W(nu, 2);
        }

        throw new InvalidOperationException();
    }

    double DevFi(double t, int n)
    {
        switch (n)
        {
            case 1:
                return t - 1;
            case 2:
                return 1 - t;
            case 3:
                return -t;
            case 4:
                return t;
        }

        throw new InvalidOperationException();
    }

    double a(int n1, int n2, int n3, int n4, Element gridElement)
    {
        return ((_grid.Points[gridElement.IdPoints[n1]][0] - _grid.Points[gridElement.IdPoints[n2]][0]) *
                (_grid.Points[gridElement.IdPoints[n3]][1] - _grid.Points[gridElement.IdPoints[n4]][1])) -
               ((_grid.Points[gridElement.IdPoints[n1]][1] - _grid.Points[gridElement.IdPoints[n2]][1]) *
                (_grid.Points[gridElement.IdPoints[n3]][0] - _grid.Points[gridElement.IdPoints[n4]][0]));
    }

    double b14(int n1, int n2, int n, Element gridElement)
    {
        return _grid.Points[gridElement.IdPoints[n1]][n] - _grid.Points[gridElement.IdPoints[n2]][n];
    }

    double b56(int n, Element gridElement)
    {
        return _grid.Points[gridElement.IdPoints[0]][n] - _grid.Points[gridElement.IdPoints[0]][n] -
            _grid.Points[gridElement.IdPoints[0]][n] + _grid.Points[gridElement.IdPoints[0]][n];
    }

    double IntegralMass(Element gridElement, int n1, int n2)
    {
        var a0 = a(1, 0, 2, 0, gridElement);
        var a1 = a(1, 0, 3, 2, gridElement);
        var a2 = a(3, 1, 2, 0, gridElement);

        Interval1D xInterval = new Interval1D(new Point(0, 0), new Point(1, 0));
        Interval1D yInterval = new Interval1D(new Point(0, 0), new Point(0, 1));

        return _gauss2D.Calculate(
            (point => Fi(point[0], point[1], n1) * Fi(point[0], point[1], n2) * double.Sign(a0) *
                      (a0 + a1 * point[0] + a2 * point[1])),
            xInterval, yInterval);
    }


    double IntegralStiffnes(Element gridElement, int i, int j)
    {
        var a0 = a(1, 0, 2, 0, gridElement);
        var a1 = a(1, 0, 3, 2, gridElement);
        var a2 = a(3, 1, 2, 0, gridElement);

        var b1 = b14(2, 0, 0, gridElement);
        var b2 = b14(1, 0, 0, gridElement);
        var b3 = b14(2, 0, 1, gridElement);
        var b4 = b14(1, 0, 1, gridElement);
        var b5 = b56(0, gridElement);
        var b6 = b56(1, gridElement);

        Interval1D xInterval = new Interval1D(new Point(0, 0), new Point(1, 0));
        Interval1D yInterval = new Interval1D(new Point(0, 0), new Point(0, 1));

        var g1 = _gauss2D.Calculate(
            (point => (DevFi(point[1], i) * (b6 * point[0] + b3) - DevFi(point[0], i) * (b6 * point[1] + b4)) *
                      (DevFi(point[0], j) * (b6 * point[0] + b3) - DevFi(point[1], j) * (b6 * point[1] + b4)) *
                      (1 / (double.Sign(a0) * (a0 + a1 * point[0] + a2 * point[1])))),
            xInterval, yInterval);
        var g2 = _gauss2D.Calculate(
            (point => (DevFi(point[1], i) * (b5 * point[0] + b2) - DevFi(point[0], i) * (b5 * point[1] + b1)) *
                      (DevFi(point[0], j) * (b5 * point[0] + b2) - DevFi(point[1], j) * (b5 * point[1] + b1)) *
                      (1 / (double.Sign(a0) * (a0 + a1 * point[0] + a2 * point[1])))),
            xInterval, yInterval);
        return g1 + g2;
    }

    private double[,] CalcMassMatrixGaussWithoutGamma(Element gridElement)
    {
        var result = new double[4, 4];
        for (var i = 1; i <= 4; i++)
        {
            for (var j = 1; j <= 4; j++)
            {
                result[i, j] = IntegralMass(gridElement, i, j);
            }
        }

        return result;
    }

    private double[,] CalcMassMatrixGauss(Element gridElement, double gamma)
    {
        var result = new double[4, 4];
        result = Multiply(CalcMassMatrixGaussWithoutGamma(gridElement), gamma);

        return result;
    }

    private double[,] CalcStiffnessMatrixGauss(Element gridElement, double lambda)
    {
        var result = new double[4, 4];
        for (var i = 1; i <= 4; i++)
        {
            for (var j = 1; j <= 4; j++)
            {
                result[i, j] = IntegralStiffnes(gridElement, i, j) * lambda;
            }
        }

        return result;
    }

    private double[] CalcVectorGauss(Element gridElement)
    {
        Func func = new Func();
        var funVector = new double[4];
        for (int i = 0; i < 4; i++)
        {
            funVector[i] = func.funRight(_grid.Points[gridElement.IdPoints[i]][0],
                _grid.Points[gridElement.IdPoints[i]][1]);
        }

        return Multiply(CalcMassMatrixGaussWithoutGamma(gridElement), funVector);
    }

    public void CalcLocalMatrices()
    {
        for (int i = 0; i < _grid.Elements.Count(); i++)
        {
            _grid.Elements[i] = new Element(
                Sum(CalcMassMatrixGauss(_grid.Elements[i], _grid.Materials[_grid.Elements[i].Material].gamma),
                    CalcStiffnessMatrixGauss(_grid.Elements[i], _grid.Materials[_grid.Elements[i].Material].Lambda)),
                CalcVectorGauss(_grid.Elements[i]), _grid.Elements[i]);
        }
    }


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