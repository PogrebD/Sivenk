using System.Text;
using Sivenk.DataTypes;

namespace Sivenk.Outputers;

public class ElemsOutputer
{
    public void Print(Element[,] elements, StreamWriter writer)
    {
        int elemsX = elements.GetLength(1);
        int elemsY = elements.GetLength(0);
        writer.WriteLine(elemsX * elemsY);
        for (int i = 0; i < elemsY; ++i)
        {
            for(int j = 0; j < elemsX; ++j)
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(elements[i, j].material + " ");
                stringBuilder.Append(elements[i, j].IdPoints[0] + " ");
                stringBuilder.Append(elements[i, j].IdPoints[1] + " ");
                stringBuilder.Append(elements[i, j].IdPoints[2] + " ");
                stringBuilder.Append(elements[i, j].IdPoints[3] + " ");
                writer.WriteLine(stringBuilder);
            }
        }
    }
}