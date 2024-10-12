using Sivenk.DataTypes;

namespace Sivenk
{
    internal class PointInputer
    {
        public Point[,] Input(StreamReader reader)
        {
                var K = reader.ReadLine();
                var KArray = K.Split(' ').ToArray();
                int Kx = int.Parse(KArray[0]);
                int Ky = int.Parse(KArray[1]);

                Point[,] points = new Point[Ky, Kx];

                for (int i = 0; i < Ky; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    for (int j = 0; j < Kx; j++)
                    {
                        points[i, j] = new Point(double.Parse(elemArray[2 * j]),
                        double.Parse(elemArray[2 * j + 1])
                        );
                    }
                }
                return points;
        }
    }
}
