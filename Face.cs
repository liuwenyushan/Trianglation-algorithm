using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
    //<face v1="0" v2="1" v3="2" />
    class Face
    {//面
        public int v1 { get; set; }//点序号
        public int v2 { get; set; }
        public int v3 { get; set; }
        public Face(int _v1,int _v2,int _v3) {
            this.v1 = _v1;
            this.v2 = _v2;
            this.v3 = _v3;
        }
    }
}
