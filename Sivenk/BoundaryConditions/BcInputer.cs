using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sivenk.BoundaryConditions;
using Sivenk.Paths;

namespace Sivenk.InputBC
{
    internal class BcInputer
    {
        public void Input(BoundaryConditionsProvider provider)
        {
            using (StreamReader reader = new(PathsProvider.BC1Folder))
            {
                int nBc1 = int.Parse(reader.ReadLine());
                List<int[]> ints = new();
                List<double> doubles = new();
                for (int i = 0; i < nBc1; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    ints.Add(new int[]
                            { int.Parse(elemArray[0]),
                            int.Parse(elemArray[1]),
                            });

                    doubles.Add(double.Parse(elemArray[2]));
                }
                provider.bc1 = new FirstBoundaryConditions(ints, doubles, nBc1);
            }

            using (StreamReader reader = new(PathsProvider.BC2Folder))
            {
                int nBc2 = int.Parse(reader.ReadLine()); 
                List<int[]> ints = new();
                List<double[]> doubles = new();
                for (int i = 0; i < nBc2; i++)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();
                    ints.Add(new int[] { int.Parse(elemArray[0]),
                        int.Parse(elemArray[1]) });

                    doubles.Add(new double[] { double.Parse(elemArray[2]),
                        double.Parse(elemArray[3]) });
                }
                provider.bc2 = new SecondBoundaryConditions(ints, doubles, nBc2);
            }
        }
    }
}
