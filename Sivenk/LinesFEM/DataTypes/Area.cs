namespace Sivenk.DataTypes;

public struct Area
{
    public int MatId { get; set; }
    public int[] BoundsIndexes { get; set; }

    public Area(int matId, int[] boundsIndexes)
    {
        MatId = matId;
        BoundsIndexes = boundsIndexes;
    }
}