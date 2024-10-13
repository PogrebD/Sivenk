namespace Sivenk.DataTypes;

public record struct Split(int IntervalsNum, double DischargeCoefficient)
{
    public int PointsNum => IntervalsNum + 1; 
}