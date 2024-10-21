using System.Text;
using Sivenk.DataTypes;

namespace Sivenk.Writers;

public class PointsWriter
{
    public void Print(Point[] points, StreamWriter writer)
    {
        int pointsNum = points.GetLength(0);
        
        writer.WriteLine(pointsNum);
        
        for (int i = 0; i < pointsNum; ++i)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(i);
            stringBuilder.Append(" ");
            stringBuilder.Append(points[i].ToString());
            writer.WriteLine(stringBuilder);
        }
    }
}