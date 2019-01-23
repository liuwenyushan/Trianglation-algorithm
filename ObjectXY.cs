using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
    class ObjectXY
    {
        public double X { get; set; }
        public double Y { get; set; }
     
        public ObjectXY(double _X, double _Y) {
            this.X = _X;
            this.Y = _Y;           
        }
    }
}
