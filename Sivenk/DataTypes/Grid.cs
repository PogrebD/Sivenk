namespace Sivenk.DataTypes
{
    public class Grid
    {
        public Bounds bounds;
        public Element[] elements;
        public Point[] points;

        public Grid(Bounds bounds, Element[] elements, Point[] points)
        {
            this.bounds = bounds;
            this.elements = elements;
            this.points = points;
        }
    }
}
