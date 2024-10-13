namespace Sivenk.DataTypes
{
    public class Grid
    {
        public Element[] elements;
        public Point[,] points;

        public Grid(Element[] elements, Point[,] points)
        {
            this.elements = elements;
            this.points = points;
        }
    }
}
