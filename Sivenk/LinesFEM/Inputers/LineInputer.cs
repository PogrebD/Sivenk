using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Inputers
{
    internal class LineInputer
    {
        public Line[] Input(StreamReader reader)
        {
            var K = reader.ReadLine();
            var KArray = K.Split(' ').ToArray();
            int Kx = int.Parse(KArray[0]);
            int Ky = int.Parse(KArray[1]);

            Line[] lines = new Line[Ky];

            for (int i = 0; i < Ky; i++)
            {
                var line = reader.ReadLine();
                var elemArray = line.Split(' ').ToArray();
                Point[] points = new Point[Kx];
                for (int j = 0; j < Kx; j++)
                {
                    points[j] = new Point(double.Parse(elemArray[2 * j]),
                        double.Parse(elemArray[2 * j + 1])
                    );
                    
                }
                
                lines[i] = new Line(points);
            }

            return lines;
        }
    }
}