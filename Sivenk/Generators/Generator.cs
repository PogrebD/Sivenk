using Sivenk.DataTypes;
using Sivenk.Generators;

namespace Sivenk.generators
{
    internal class Generator
    {
        public Generator(Grid grid)
        {
            bc1Generator = new BC1Generator();
            bc2Generator = new BC2Generator();
        }

        public IGenerator bc1Generator;
        public IGenerator bc2Generator;

        public void Generate(Grid grid)
        {
            bc1Generator.Generate(grid);
            bc2Generator.Generate(grid);
        }
    }
}
