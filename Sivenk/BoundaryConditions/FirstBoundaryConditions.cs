using Sivenk.DataTypes;

namespace Sivenk.BoundaryConditions;

public class FirstBoundaryConditions
{
    public FirstBoundaryConditions(List<int[]> nodeIndices, List<double> u, int nBc)
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
        Func U = new Func();
        for (int i = 0; i < nBc; i++)
        {
            for (int k = 0; k < 2; k++)
            {
                if (nodeIndices[i][k] != 0)
                {
                    for (int j = globalMatrices.ig[nodeIndices[i][k] - 1]; j < globalMatrices.ig[nodeIndices[i][k]]; j++)
                    {
                        //globalMatrices._globalVectorB[globalMatrices.jg[j]] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]) * globalMatrices._globaleATriangle[j];
                        globalMatrices._globalVectorB[globalMatrices.jg[j]] -= U.FunU(grid.Points[nodeIndices[i][k]][0], grid.Points[nodeIndices[i][k]][1]) * globalMatrices._globaleATriangle[j];

                        globalMatrices._globaleATriangle[j] = 0;
                    }
                }
                //globalMatrices._globalVectorB[nodeIndices[i][k]] = Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]);
                globalMatrices._globalVectorB[nodeIndices[i][k]] = U.FunU(grid.Points[nodeIndices[i][k]][0], grid.Points[nodeIndices[i][k]][1]);
                globalMatrices._globaleAdiag[nodeIndices[i][k]] = 1;
                for (int j = nodeIndices[i][k] + 1; j < grid.Points.Length; j++)
                {
                    for (int h = globalMatrices.ig[j - 1]; h < globalMatrices.ig[j]; h++)
                    {
                        if (globalMatrices.jg[h] == nodeIndices[i][k])
                        {
                            //globalMatrices._globalVectorB[j] -= Func.u(grid.nodes[nodeIndices[i][k]].r, grid.nodes[nodeIndices[i][k]].z, grid.time.timeSloy[t]) * globalMatrices._globaleATriangle[h];
                            globalMatrices._globalVectorB[j] -= U.FunU(grid.Points[nodeIndices[i][k]][0], grid.Points[nodeIndices[i][k]][1]) * globalMatrices._globaleATriangle[h];
                            globalMatrices._globaleATriangle[h] = 0;
                        }
                    }
                }
            }
        }
    }
}