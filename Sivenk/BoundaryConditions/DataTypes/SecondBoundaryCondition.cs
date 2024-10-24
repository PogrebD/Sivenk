namespace Sivenk.BoundaryConditions.DataTypes;

public readonly record struct SecondBoundaryCondition(int ElemIndex, int FirstLocalIndex, int SecondLocalIndex, double Theta1, double Theta2);