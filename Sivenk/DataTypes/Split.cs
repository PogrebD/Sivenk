using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivenk.DataTypes
{
    public struct Split
    {
        public int NInterval { get; set; }
        public double CoefDischarge { get; set; }

        public Split(int nInterval, double coefDischarge)
        {
            NInterval = nInterval;
            CoefDischarge = coefDischarge;
        }

        public readonly override string ToString()
        {
            return new string($"{NInterval} {CoefDischarge}");
        }
    }
}
