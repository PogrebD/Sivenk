namespace Sivenk.BoundaryConditions.DataTypes;

public readonly record struct FirstBoundaryCondition(int FirstGlobalIndex, int SecondGlobalIndex, double FunctionValue);