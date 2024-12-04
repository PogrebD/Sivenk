using MathLibrary.DataTypes;
using MathLibrary.DataTypes.Internal;

namespace Sivenk;

public class Gauss2D 
{
    private readonly GaussConfig _config;

    public Gauss2D(GaussConfig config)
    {
        if (config.Segments < 1)
        {
            throw new ArgumentException("The number of segments must be at least 1.");
        }

        _config = config;
    }

    public double Calculate(Func<Point, double> f, Interval1D xInterval, Interval1D
        yInterval)
    {
        var integral = 0d;
        var xSegmentLength = (xInterval.End - xInterval.Start) /
                             _config.Segments;
        var ySegmentLength = (yInterval.End - yInterval.Start) /
                             _config.Segments;
        for (var i = 0; i < _config.Segments; i++)
        {
            for (var j = 0; j < _config.Segments; j++)
            {
                var xStart = xInterval.Start + i * xSegmentLength;
                var xEnd = xStart + xSegmentLength;
                var yStart = yInterval.Start + j * ySegmentLength;
                var yEnd = yStart + ySegmentLength;
                integral += CalculateOnSubinterval(f, xStart, xEnd, yStart,
                    yEnd);
            }
        }

        return integral;
    }

    private double CalculateOnSubinterval(Func<Point, double> f, double
            xStart, double xEnd, double yStart,
        double yEnd)
    {
        var ySum = 0d;
        var xHalfLength = (xEnd - xStart) / 2;
        var xMid = (xStart + xEnd) / 2;
        var yHalfLength = (yEnd - yStart) / 2;
        var yMid = (yStart + yEnd) / 2;
        for (var i = 0; i < _config.Nodes.Count; i++)
        {
            var y = (yHalfLength * _config.Nodes[i] + yMid);
            var xSum = 0d;
            for (var j = 0; j < _config.Nodes.Count; j++)
            {
                var x = xHalfLength * _config.Nodes[j] + xMid;
                xSum += _config.Weights[j] * f(new Point(x, y));
            }

            ySum += _config.Weights[i] * xHalfLength * xSum;
        }

        return yHalfLength * ySum;
    }
}

public class GaussConfig
{
    public IReadOnlyList<double> Nodes { get; init; }

    public IReadOnlyList<double> Weights { get; init; }
    public int Segments { get; init; }

    public static GaussConfig Gauss2(int segments) => new()
    {
        Nodes = [-1d / Math.Sqrt(3), 1d / Math.Sqrt(3)],
        Weights = [1d, 1d],
        Segments = segments
    };

    public static GaussConfig Gauss3(int segments) => new()
    {
        Nodes =
        [
            -Math.Sqrt(3d / 5),
            0,
            Math.Sqrt(3d / 5),
        ],
        Weights =
        [
            5d / 9,
            8d / 9,
            5d / 9
        ],
        Segments = segments
    };

    public static GaussConfig Gauss4(int segments) => new()
    {
        Nodes =
        [
            -Math.Sqrt((3 - 2 * Math.Sqrt(6d / 5)) / 7),
            Math.Sqrt((3 - 2 * Math.Sqrt(6d / 5)) / 7),
            -Math.Sqrt((3 + 2 * Math.Sqrt(6d / 5)) / 7),
            Math.Sqrt((3 + 2 * Math.Sqrt(6d / 5)) / 7),
        ],
        Weights =
        [
            (18d + Math.Sqrt(30)) / 36,
            (18d + Math.Sqrt(30)) / 36,
            (18d - Math.Sqrt(30)) / 36,
            (18d - Math.Sqrt(30)) / 36,
        ],
        Segments = segments
    };
}