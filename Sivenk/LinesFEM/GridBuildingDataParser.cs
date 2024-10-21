using MathLibrary.DataTypes;
using Sivenk.DataTypes;

namespace Sivenk.LinesFEM;

public class GridBuildingDataParser
{
    public GridBuildingData Parse(InputData inputData)
    {
        Bounds bounds = ParseBounds(inputData.Lines);
        Point[] points = ParsePoints(inputData.Lines, bounds);
        Element[] elements = ParseElements(inputData.Area, bounds);

        return new GridBuildingData(bounds, points, elements);
    }

    private Point[] ParsePoints(Line[] inputDataLines, Bounds bounds)
    {
        Point[] points = new Point[bounds.PointsNum];
        for (int i = 0; i < bounds.PointsNumY; ++i)
        {
            for (int j = 0; j < bounds.PointsNumX; ++j)
            {
                points[i * bounds.PointsNumX + j] = new Point(inputDataLines[i].Points[j]);
            }   
        }

        return points;
    }

    private Bounds ParseBounds(Line[] inputDataLines)
    {
        return new Bounds(inputDataLines[0].Points.Length - 1, inputDataLines.Length - 1);
    }

    private Element[] ParseElements(Area[] areas, Bounds bounds)
    {
        Element[] result = new Element[bounds.ElementsNum];

        for (int i = 0; i < bounds.ElementsNumY; i++)
        {
            for (int j = 0; j < bounds.ElementsNumX; j++)
            {
                int[] idPoint = new int[4];
                idPoint[0] = i * (bounds.ElementsNumX+1) + j;
                idPoint[1] = i * (bounds.ElementsNumX + 1) + j + 1;
                idPoint[2] = (i + 1) * (bounds.ElementsNumX + 1) + j;
                idPoint[3] = (i + 1) * (bounds.ElementsNumX + 1) + j + 1;

                result[i * bounds.ElementsNumX + j] = new Element(idPoint);
            }
        }

        for (int k = 0; k < areas.Length; k++)
        {
            for(int i = areas[k].BoundsIndexes[2]; i < areas[k].BoundsIndexes[3]; i++)
            {
                for (int j = areas[k].BoundsIndexes[0]; j < areas[k].BoundsIndexes[1]; j++)
                {
                    result[i * bounds.ElementsNumX + j].Material = areas[k].MatId;
                }
            }
        }


        return result;
    }
}