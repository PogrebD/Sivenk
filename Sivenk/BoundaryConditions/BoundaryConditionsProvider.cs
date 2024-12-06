using Sivenk.DataTypes;

namespace Sivenk.BoundaryConditions;

#if false
public class BoundaryConditionsProvider
{
    public Bc1 bc1;
    public SecondBoundaryConditions bc2;
    BcInputer bcInputer;

    public BoundaryConditionsProvider(GlobalMatrix globalMatrices, Grid grid)
    {
        bcInputer = new(this);
        bc2.Apply(globalMatrices, grid);
        bc1.Apply(globalMatrices, grid);
    }
}

public class Bc1
{
    public Bc1(List<int[]> nodeIndices, List<double> u, int nBc)
    {
        this.nodeIndices = nodeIndices;
        this.u = u;
        this.nBc = nBc;
    }
    private int nBc;
    private List<int[]> nodeIndices;
    private List<double> u;

    public void Apply(GlobalMatrix globalMatrices, Grid grid)
    {
        for (int i = 0; i < nBc; i++)
        {
            for (int k = 0; k < 2; k++)
            {
                if (nodeIndices[i][k] != 0)
                {
                    for (int j = globalMatrices.ig[nodeIndices[i][k] - 1]; j < globalMatrices.ig[nodeIndices[i][k]]; j++)
                    {
                        //globalMatrices._globalVectorB[globalMatrices.jg[j]] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]) * globalMatrices._globaleATriangle[j];
                        globalMatrices._globalVectorB[globalMatrices.jg[j]] -= u[i] * globalMatrices._globaleATriangle[j];

                        globalMatrices._globaleATriangle[j] = 0;
                    }
                }
                //globalMatrices._globalVectorB[nodeIndices[i][k]] = Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]);
                globalMatrices._globalVectorB[nodeIndices[i][k]] = u[i];
                globalMatrices._globaleAdiag[nodeIndices[i][k]] = 1;
                for (int j = nodeIndices[i][k] + 1; j < grid.nodes.Count; j++)
                {
                    for (int h = globalMatrices.ig[j - 1]; h < globalMatrices.ig[j]; h++)
                    {
                        if (globalMatrices.jg[h] == nodeIndices[i][k])
                        {
                            //globalMatrices._globalVectorB[j] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]) * globalMatrices._globaleATriangle[h];
                            globalMatrices._globalVectorB[j] -= u[i] * globalMatrices._globaleATriangle[h];
                            globalMatrices._globaleATriangle[h] = 0;
                        }
                    }
                }
            }
        }
    }
}
#endif