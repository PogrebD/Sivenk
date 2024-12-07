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
            (point => UtilsLibrary.Fi(point[0], point[1], n1) * UtilsLibrary.Fi(point[0], point[1], n2) * double.Sign(a0) *
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
            (point => (UtilsLibrary.DevFi(point[1], i) * (b6 * point[0] + b3) - UtilsLibrary.DevFi(point[0], i) * (b6 * point[1] + b4)) *
                      (UtilsLibrary.DevFi(point[0], j) * (b6 * point[0] + b3) - UtilsLibrary.DevFi(point[1], j) * (b6 * point[1] + b4)) *
                      (1 / (double.Sign(a0) * (a0 + a1 * point[0] + a2 * point[1])))),
            xInterval, yInterval);
        var g2 = _gauss2D.Calculate(
            (point => (UtilsLibrary.DevFi(point[1], i) * (b5 * point[0] + b2) - UtilsLibrary.DevFi(point[0], i) * (b5 * point[1] + b1)) *
                      (UtilsLibrary.DevFi(point[0], j) * (b5 * point[0] + b2) - UtilsLibrary.DevFi(point[1], j) * (b5 * point[1] + b1)) *
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
        return UtilsLibrary.Multiply(CalcMassMatrixGaussWithoutGamma(gridElement), gamma);
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
        Func func = new();
        var funVector = new double[4];
        for (int i = 0; i < 4; i++)
        {
            funVector[i] = func.FunRight(_grid.Points[gridElement.IdPoints[i]][0],
                _grid.Points[gridElement.IdPoints[i]][1]);
        }

        return UtilsLibrary.Multiply(CalcMassMatrixGaussWithoutGamma(gridElement), funVector);
    }

    public void CalcLocalMatrices()
    {
        for (int i = 0; i < _grid.Elements.Count(); i++)
        {
            _grid.Elements[i] = new Element(
                UtilsLibrary.Sum(CalcMassMatrixGauss(_grid.Elements[i], _grid.Materials[_grid.Elements[i].Material].gamma),
                    CalcStiffnessMatrixGauss(_grid.Elements[i], _grid.Materials[_grid.Elements[i].Material].Lambda)),
                CalcVectorGauss(_grid.Elements[i]), _grid.Elements[i]);
        }
    }
}