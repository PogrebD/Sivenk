using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivenk
{
    public class Grid
    {
        Element[] elements;
        Point[,] points;
    }

    public struct Material(int Number, double Cp, double Ro, double Lambda);

    public class Element
    {
        public Element(int[] idPoints)
        {
            IdPoints = idPoints;
        }
        public int[] IdPoints { get; set; }
        public int material { get; set; }
    }

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override readonly string ToString()
        {
            return new string($"{X} {Y}");
        }
    }

    public struct Area
    {
        public int MatId { get; set; }
        public int[] BoundsIndexes { get; set; }
        public Area(int matId, int[] boundsIndexes)
        {
            MatId = matId;
            BoundsIndexes = boundsIndexes;
        }
    }
}
