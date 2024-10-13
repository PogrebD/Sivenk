using Sivenk.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivenk.LinesFEM.Inputers
{
    internal class SplitInputer
    {
        public Split[] Input(StreamReader reader)
        {
            var nSplit = int.Parse(reader.ReadLine());

            Split[] splits = new Split[nSplit];

            var line = reader.ReadLine();
            var elemArray = line.Split(' ').ToArray();
            for (int i = 0; i < nSplit; i++)
            {
                splits[i] = new Split(int.Parse(elemArray[2 * i]),
                double.Parse(elemArray[2 * i + 1])
                );
            }
            return splits;
        }
    }
}
