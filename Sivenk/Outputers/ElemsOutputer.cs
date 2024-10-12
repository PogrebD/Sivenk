using System.Text;
using Sivenk.DataTypes;

namespace Sivenk.Outputers;

public class ElemsOutputer
{
    public void Print(Element[] elements, StreamWriter writer)
    {
        int elemsNum = elements.GetLength(0);

        writer.WriteLine(elemsNum);
        for (int i = 0; i < elemsNum; ++i)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(elements[i].material + " ");
            stringBuilder.Append(elements[i].IdPoints[0] + " ");
            stringBuilder.Append(elements[i].IdPoints[1] + " ");
            stringBuilder.Append(elements[i].IdPoints[2] + " ");
            stringBuilder.Append(elements[i].IdPoints[3] + " ");
            writer.WriteLine(stringBuilder);
        }
    }
}