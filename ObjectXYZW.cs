using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
    class ObjectXYZW
    {
        public double X{get;set;}
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }
        public ObjectXYZW(double _X,double _Y,double _Z,double _W) {
            this.X = _X;
            this.Y = _Y;
            this.Z = _Z;
            this.W = _W;
        }
        public ObjectXYZW(ObjectXYZ obxyz, double _W) {
            this.X = obxyz.X;
            this.Y = obxyz.Y;
            this.Z = obxyz.Z;
            this.W = _W;
        }
    }
}
