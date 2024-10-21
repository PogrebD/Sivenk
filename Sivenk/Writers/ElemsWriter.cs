using System.Text;
using Sivenk.DataTypes;

namespace Sivenk.Writers;

public class ElemsWriter
{
    public void Print(Element[] elements, StreamWriter writer)
    {
        int elemsNum = elements.GetLength(0);

        writer.WriteLine(elemsNum);
        for (int i = 0; i < elemsNum; ++i)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(elements[i].Material + " ");
            stringBuilder.Append(elements[i].IdPoints[0] + " ");
            stringBuilder.Append(elements[i].IdPoints[1] + " ");
            stringBuilder.Append(elements[i].IdPoints[2] + " ");
            stringBuilder.Append(elements[i].IdPoints[3] + " ");
            writer.WriteLine(stringBuilder);
        }
    }
}