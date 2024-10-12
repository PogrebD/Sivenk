using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivenk.Inputers
{
    internal class MatInputer
    {
        public Material[] Input(StreamReader reader)
        {
            List<Material> input = new();
                int nMat = int.Parse(reader.ReadLine());
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
            return input.ToArray();
        }
    }
}
