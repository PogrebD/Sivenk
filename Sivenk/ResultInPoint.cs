using MathLibrary.DataTypes;
using Sivenk.DataTypes;

namespace Sivenk;

public class ResultInPoint
{
    public IReadOnlyList<double> Weights { get; }

    private readonly Grid _grid;
    private const double Epsilon = 1e-10;

    public ResultInPoint(Grid grid, IReadOnlyList<double> weights)
    {
        _grid = grid;
        Weights = weights;
    }

    public double Calculate(Point point)
    {
        Element element = _grid.Elements.First(el => ElementHas(el, point));

        var x = new double[4];
        var y = new double[4];

        for (int i = 0; i < 4; i++)
        {
            var node = _grid.Points[element.IdPoints[i]];

            x[i] = node[0];
            y[i] = node[1];
        }

        var b1 = x[2] - x[0];
        var b2 = x[1] - x[0];
        var b3 = y[2] - y[0];
        var b4 = y[1] - y[0];
        var b5 = x[0] - x[1] - x[2] + x[3];
        var b6 = y[0] - y[1] - y[2] + y[3];

        var alpha1 = (x[1] - x[0]) * (y[3] - y[2]) - (y[1] - y[0]) * (x[3] - x[2]);
        var alpha2 = (x[3] - x[1]) * (y[2] - y[0]) - (y[3] - y[1]) * (x[2] - x[0]);

        var w = b6 * (point[0] - x[0]) - b5 * (point[1] - y[0]);

        double ksi, eta;

        if (alpha1 < Epsilon || alpha2 < Epsilon)
        {
            ksi = (b3 * (point[0] - x[0]) - b1 * (point[1] - y[0])) / (b2 * b3 - b1 * b4);
            eta = (b2 * (point[1] - y[0]) - b4 * (point[0] - x[0])) / (b2 * b3 - b1 * b4);
        }
        else if (alpha1 < Epsilon && alpha2 > Epsilon)
        {
            ksi = (alpha2 * (point[0] - x[0]) + b1 * w) / (alpha2 * b2 - b5 * w);
            eta = -1d * w / alpha2;
        }
        else if (alpha2 < Epsilon && alpha1 > Epsilon)
        {
            ksi = w / alpha1;
            eta = (alpha1 * (point[1] - y[0]) - b4 * w) / (alpha1 * b3 + b6 * w);
        }
        else
        {
            throw new NotImplementedException();
        }

        var pointInTemplate = new Point(new double[] { ksi, eta });
        var funcValues = new double[4];

        for (int i = 0; i < 4; i++)
        {
            funcValues[i] = UtilsLibrary.Fi(ksi, eta, i);
        }

        var result = 0d;
        for (int i = 0; i < 4; i++)
        {
            var weightId = element.IdPoints[i];
            var weight = Weights[weightId];

            result += funcValues[i] * weight;
        }

        return result;
    }

    private bool ElementHas(Element element, Point point)
    {
        var nodes = element.IdPoints
            .Select(nodeId => _grid.Points[nodeId])
            .ToArray();

        var leftBottom = nodes[0];
        var rightBottom = nodes[1];
        var leftTop = nodes[2];
        var rightTop = nodes[3];

        return IsPointInTriangle(point, leftBottom, rightBottom, leftTop) ||
               IsPointInTriangle(point, leftTop, rightBottom, rightTop);

        static bool IsPointInTriangle(Point p, Point a, Point b, Point c)
        {
            // Векторные произведения для всех трёх рёбер треугольника 
            var v1 = (b - a)[0] * (p[1] - a[1]) - (b - a)[1] * (p[0] - a[0]);
            var v2 = (c - b)[0] * (p[1] - b[1]) - (c - b)[1] * (p[0] - b[0]);
            var v3 = (a - c)[0] * (p[1] - c[1]) - (a - c)[1] * (p[0] - c[0]);

            // Проверяем, что все знаки одинаковы 
            return (v1 >= 0 && v2 >= 0 && v3 >= 0) || (v1 <= 0 && v2 <= 0 && v3 <= 0);
        }
    }
}