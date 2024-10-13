using Sivenk.DataTypes;

namespace Sivenk.LinesFEM.Inputers
{
    internal class AreaInputer
    {
        public Area[] Input(StreamReader reader)
        {
            var nArea = int.Parse(reader.ReadLine());

            Area[] areas = new Area[nArea];

            for (int i = 0; i < nArea; i++)
            {
                var line = reader.ReadLine();
                var elemArray = line.Split(' ').ToArray();

                areas[i] = new Area(int.Parse(elemArray[0]), [int.Parse(elemArray[1]),
                        int.Parse(elemArray[2]),
                        int.Parse(elemArray[3]),
                        int.Parse(elemArray[4])]
                        );
            }
            return areas;
        }
    }
}
