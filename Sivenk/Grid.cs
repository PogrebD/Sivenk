﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sivenk.DataTypes;

namespace Sivenk
{
    public class Grid
    {
        Element[] elements;
        Point[,] points;

        public Grid(Element[] elements, Point[,] points)
        {
            this.elements = elements;
            this.points = points;
        }
    }
}
