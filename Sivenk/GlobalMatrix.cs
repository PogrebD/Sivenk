using Sivenk.DataTypes;

namespace Sivenk;

public class GlobalMatrix
{
    public double[] _globaleATriangle;
    public double[] _globaleAdiag;
    public double[] _globalVectorB;
    public List<int> ig;
    private List<int> ig2;
    public List<int> jg;
    private List<List<int>> versh;
    private Grid _grid;

    public GlobalMatrix(Grid grid)
    {
        ig = new List<int>();
        jg = new List<int>();
        versh = Init(grid.Points.Length);
        _grid = grid;
        Portrait();
        _globaleAdiag = new double[ig.Count];
        _globaleATriangle = new double[jg.Count];
        _globalVectorB = new double[ig.Count];
        LocalMatricesInsertion();
    }

    public void Portrait()
    {
        for (int i = 0; i < _grid.Elements.Count(); i++)
        {
            for (int ji = 0; ji < 4; ji++)
            {
                for (int j = 0; j < ji; j++)
                {
                    versh[_grid.Elements[i].IdPoints[ji]].Add(_grid.Elements[i].IdPoints[j]);
                }
            }
        }

        for (int i = 0; i < versh.Count; i++)
        {
            versh[i] = versh[i].Distinct().ToList();
            versh[i].Sort();
        }

        ig.Add(0);
        for (int i = 1; i < _grid.Points.Length; i++)
        {
            ig.Add(ig[i - 1] + versh[i].Count);
        }

        foreach (var vershI in versh)
        {
            foreach (var vershJ in vershI)
            {
                jg.Add(vershJ);
            }
        }

        ig2 = new List<int>();
        ig2.Add(0);
        ig2.AddRange(ig);
    }

    public void LocalMatricesInsertion()
    {
        for (int k = 0; k < _grid.Elements.Length; k++)
        {
            for (int j = 0; j < 4; j++)
            {
                _globaleAdiag[_grid.Elements[k].IdPoints[j]] +=
                    _grid.Elements[k].AMatrix[j, j];
            }

            for (int i = 0; i < 4; i++)
            {
                var ibeg = ig2[_grid.Elements[k].IdPoints[i]];
                for (int j = 0; j < i; j++)
                {
                    var iend = ig2[_grid.Elements[k].IdPoints[i] + 1] - 1;
                    while (jg[ibeg] != _grid.Elements[k].IdPoints[j])
                    {
                        ibeg++;
                    }

                    _globaleATriangle[ibeg] += _grid.Elements[k].AMatrix[i, j];
                    ibeg++;
                }
            }

            for (var j = 0; j < 4; j++)
            {
                _globalVectorB[_grid.Elements[k].IdPoints[j]] += _grid.Elements[k].VectorB[j];
            }
        }
    }

    public static List<List<int>> Init(int n)
    {
        var list = new List<List<int>>();
        for (var i = 0; i < n; i++)
        {
            list.Add(new(0));
        }

        return list;
    }
}