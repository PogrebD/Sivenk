using Sivenk.DataTypes;

namespace Sivenk.Constructors;

public class DefaultConstructor : IConstructor
{
    public Grid Construct(GridBuildingData gridBuildingData)
    {
        return new Grid(gridBuildingData.elements, gridBuildingData.points);
    }
}