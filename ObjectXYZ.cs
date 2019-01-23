using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
    class ObjectXYZ
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public ObjectXYZ(double _X, double _Y, double _Z) {
            this.X = _X;
            this.Y = _Y;
            this.Z = _Z;
        }
        public ObjectXYZ(ObjectXY obxy, double _Z) {
            this.X = obxy.X;
            this.Y = obxy.Y;
            this.Z = _Z;
        }
    }
}
