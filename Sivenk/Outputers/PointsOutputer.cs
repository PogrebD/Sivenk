using System.Text;
using Sivenk.DataTypes;

namespace Sivenk.Outputers;

public class PointsOutputer
{
    public void Print(Point[,] points, StreamWriter writer)
    {
        int xPointsNum = points.GetLength(1);
        int yPointsNum = points.GetLength(0);
        
        writer.WriteLine(xPointsNum * yPointsNum);
        
        for (int i = 0; i < yPointsNum; ++i)
        {
            for (int j = 0; j < xPointsNum; ++j)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(i * xPointsNum + j);
                stringBuilder.Append(" ");
                stringBuilder.Append(points[i, j].ToString());
                writer.WriteLine(stringBuilder);
            }
        }
    }
}