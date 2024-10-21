namespace Sivenk.DataTypes;

public record struct Split(int IntervalsNum, double DischargeCoefficient)
{
    public readonly int PointsNum => IntervalsNum + 1; 
}