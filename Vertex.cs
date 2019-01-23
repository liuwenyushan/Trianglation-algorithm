using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
//      <position x="-0.0190301" y="0.0956709" z="0.490393" />
//      <normal x="-0.0392071" y="0.197107" z="0.979598" />
//      <texcoord u="0.03125" v="0.0625" />
//       <texcoord u="0.523049" v="0.306614" />
//       <tangent x="-0.97823" y="-0.207507" z="0.00260055" w="1" />
    class Vertex
    {//点
        public int Index { get; set; }//点的索引
        public ObjectXYZ Position { 
            get{return this.position;}
            set { this.position = value; }
        }
        public bool isSalienPoint{get;set;}//点的凹凸性，true为凸
        public ObjectXYZ Normal
        {
            get { return this.normal; }
            set{this.normal = value;} 
        }
        public ObjectXY TexCoord1 {
            get { return this.texcoord1; }
            set { this.texcoord1 = value; }
        }

        public ObjectXY TexCoord2
        {
            get { return this.texcoord2; }
            set { this.texcoord2 = value; }
        }

        public ObjectXYZW Tangent {
            get { return this.tangent; }
            set { this.tangent = value; }
        }

        ObjectXYZ position;//位置
        ObjectXYZ normal;//法线
        ObjectXY texcoord1 ;//材质坐标1
        ObjectXY texcoord2;//材质坐标2
        ObjectXYZW tangent;//切线

        public Vertex(ObjectXYZ _position,ObjectXYZ _normal,ObjectXY _texcoord1,ObjectXY _texcoord2,ObjectXYZW _tangent) {
            this.position = _position;
            this.normal = _normal;
            this.texcoord1 = _texcoord1;
            this.texcoord2 = _texcoord2;
            this.tangent = _tangent;
        }

    }
}
