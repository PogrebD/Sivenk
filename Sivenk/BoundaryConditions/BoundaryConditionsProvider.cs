using Sivenk.DataTypes;

namespace Sivenk.BoundaryConditions;

public class BoundaryConditionsProvider
{
    public FirstBoundaryConditions bc1;
    public SecondBoundaryConditions bc2;

    public void Applay(GlobalMatrix globalMatrices, Grid grid)
    {
        bc2.Apply(globalMatrices, grid);
        bc1.Apply(globalMatrices, grid);
    }
}
