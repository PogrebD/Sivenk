namespace Sivenk.BoundaryConditions.DataTypes;

public readonly record struct ThirdBoundaryCondition(int ElemIndex, int FirstLocalIndex, int SecondLocalIndex, double Betta, double U1, double U2);