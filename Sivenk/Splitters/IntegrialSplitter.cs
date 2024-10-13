using Sivenk.DataTypes;

namespace Sivenk.Splitters;

public class IntegrialSplitter : ISplitter
{
    private Split[] _splitX;
    private Split[] _splitY;

    private int pointsNumX;
    private int pointsNumY;
    private int elementsNumX;
    private int elementsNumY;

    public IntegrialSplitter(Split[] splitX, Split[] splitY)
    {
        _splitX = splitX;
        _splitY = splitY;

        elementsNumX = SumInterval(_splitX);
        elementsNumY = SumInterval(_splitY);

        pointsNumX = elementsNumX + 1;
        pointsNumY = elementsNumY + 1;
    }

    public Grid Split(Grid sourceGrid)
    {
        Point[,] newPoints = new Point[pointsNumY, pointsNumX];
        Element[,] newElements = new Element[pointsNumY - 1, pointsNumX - 1];

        int prevY = 0;
        int prevX = 0;
        
        for(int i = 0; i < sourceGrid.elements.GetLength(0); ++i)
        {
            for (int j = 0; j < sourceGrid.elements.GetLength(1); ++j)
            {
                Element currentElement = sourceGrid.elements[i, j];

                int localElementsNumX = _splitX[j].NInterval;
                int localElementsNumY = _splitY[i].NInterval;
                
                Element[,] localElements = SplitElements(currentElement, prevX, prevY, localElementsNumX, localElementsNumY);
                Point[,] localPoints = SplitPoints(currentElement, sourceGrid.points, localElementsNumX, localElementsNumY);
                
                //InsertLocalElements(currentElement, newElements, localElements);
                InsertLocalPoints(currentElement, newPoints, localPoints);
                
                prevX += _splitX[j].NInterval;
            }

            prevX = 0;
            prevY += _splitY[i].NInterval;
        }

        Grid result = new(newElements, newPoints);
        return result;
    }

    private void InsertLocalElements(Element currentElement, Element[,] globalElements, Element[,] localElements)
    {
        for (int i = 0; i < localElements.GetLength(0); ++i)
        {
            for (int j = 0; j < localElements.GetLength(1); ++j)
            {
                int newI = currentElement.IdPoints[0].Item1;
                int newJ = currentElement.IdPoints[0].Item2;
                globalElements[newI, newJ] = new Element(localElements[i, j].IdPoints, localElements[i, j].material);
            }   
        }
    }
    
    private void InsertLocalPoints(Element currentElement, Point[,] globalPoints, Point[,] localPoints)
    {
        for (int i = 0; i < localPoints.GetLength(0); ++i)
        {
            for (int j = 0; j < localPoints.GetLength(1); ++j)
            {
                int newI = currentElement.IdPoints[0].Item1;
                int newJ = currentElement.IdPoints[0].Item2;
                
                globalPoints[newI, newJ] = new Point(localPoints[i,j]);
            }   
        }
    }

    private Element[,] SplitElements(Element currentElement, int prevX, int prevY, int localElementsNumX, int localElementsNumY)
    {
        Element[,] localElements = new Element[localElementsNumY, localElementsNumX];
        for(int n = 0; n < localElementsNumY; ++n)
        {
            for(int m = 0; m < localElementsNumX; ++m)
            {
                Element localElement = new();
                localElement.material = currentElement.material;
                Tuple<int, int>[] indexes =
                {
                    new Tuple<int, int>(prevY + n, prevX + m),
                    new Tuple<int, int>(prevY + n, prevX + 1 + m),
                    new Tuple<int, int>(prevY + 1 + n, prevX + m),
                    new Tuple<int, int>(prevY + 1 + n, prevX + 1 + m),
                };
                        
                localElement.IdPoints = indexes;
                localElements[n, m] = localElement;
            }
        }

        return localElements;
    }
    
    private Point[,] SplitPoints(Element currentElement, Point[,] points, int localElementsNumX, int localElementsNumY)
    {
        Point[,] result = new Point[localElementsNumY + 1, localElementsNumX + 1];

        Point bottomLeftPoint = points[currentElement.IdPoints[0].Item1, currentElement.IdPoints[0].Item2];
        Point topRightPoint = points[currentElement.IdPoints[3].Item1, currentElement.IdPoints[3].Item2];
        
        double stepX = (topRightPoint.X - bottomLeftPoint.X) / localElementsNumX;
        double stepY = (topRightPoint.Y - bottomLeftPoint.Y) / localElementsNumY;
        for (int i = 0; i < localElementsNumY + 1; ++i)
        {
            for (int j = 0; j < localElementsNumX + 1; ++j)
            {
                result[i, j] = new Point(bottomLeftPoint.X + stepX * j, bottomLeftPoint.Y + stepY * i);
            }
        }
        
        return result;
    }

    private int SumInterval(Split[] intervals)
    {
        int sum = 0;
        foreach(Split split in intervals)
        {
            sum += split.NInterval;
        }

        return sum;
    }
}