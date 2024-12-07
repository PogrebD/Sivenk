namespace Sivenk;

public static class UtilsLibrary
{
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

    public static double W(double t, int n) => n == 1 ? 1 - t : t;

    public static double Fi(double psi, double nu, int n) => n switch
    {
        0 => W(psi, 1) * W(nu, 1),
        1 => W(psi, 2) * W(nu, 1),
        2 => W(psi, 1) * W(nu, 2),
        3 => W(psi, 2) * W(nu, 2),
        _ => throw new NotImplementedException(),
    };

    public static double DevFi(double t, int n) => n switch
    {
        0 => t - 1,
        1 => 1 - t,
        2 => -t,
        3 => t,
        _ => throw new NotImplementedException(),
    };
}
