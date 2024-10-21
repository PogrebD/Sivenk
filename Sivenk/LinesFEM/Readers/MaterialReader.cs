using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Readers;

public class MaterialReader
{
    public Material[] Input(StreamReader reader)
    {
        IList<Material> input = [];

        _ = int.Parse(reader.ReadLine());

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var elemArray = line.Split(' ').ToArray();

            input.Add(
                new Material(int.Parse(elemArray[0]),
                    double.Parse(elemArray[1]) * double.Parse(elemArray[2]),
                    double.Parse(elemArray[3]),
                    double.Parse(elemArray[4])
                    ));
        }
        return [.. input];
    }
}
