﻿using Sivenk.Inputers;
using Sivenk.DataTypes;

namespace Sivenk
{
    public class LinesGridBuilder
    {
        private Element[] elements;
        private Point[,] points;
        
        Inputer inputer = new();

        public LinesGridBuilder()
        {
            const string BasePath = "../../../txt/";

            InputData inputData = inputer.Input(BasePath + "input.txt", BasePath + "material.txt");
            points = inputData.Points;
            elements = BuildElements(inputData.Points, inputData.Area);
        }

        private Element[] BuildElements(Point[,] points, Area[] areas)
        {
            int elemX = (points.GetLength(1) - 1);
            int elemY = (points.GetLength(0) - 1);
            int elemNum = elemX * elemY;
            Element[] result = new Element[elemNum];

            for (int i = 0; i < elemY; i++)
            {
                for (int j = 0; j < elemX; j++)
                {
                    int[] idPoint = new int[4];
                    idPoint[0] = i * (elemX+1) + j;
                    idPoint[1] = i * (elemX + 1) + j + 1;
                    idPoint[2] = (i + 1) * (elemX + 1) + j;
                    idPoint[3] = (i + 1) * (elemX + 1) + j + 1;

                    result[i * elemX + j] = new Element(idPoint);
                }
            }

            for (int k = 0; k < areas.Length; k++)
            {
                for(int i = areas[k].BoundsIndexes[2]; i < areas[k].BoundsIndexes[3]; i++)
                {
                    for (int j = areas[k].BoundsIndexes[0]; j < areas[k].BoundsIndexes[1]; j++)
                    {
                        result[elemX * i + j].material = areas[k].MatId;
                    }
                }
            }


            return result;
        }

        public Grid Build()
        {
            return new Grid(elements, points);
        }
    }
}
