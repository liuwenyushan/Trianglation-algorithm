using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapefileTest
{
    //多边形三角化,仅限于二维平面，顶点Z值均为0
    class Trianglation
    {
         
        //凸多边形三角化,顶点为顺时针
        public static List<Face> getFacesOfSimplePolygon(List<Vertex> vertexes)
        {

            List<Vertex> _vertexes=setIsSalientPoint(vertexes);

            List<Face> faces = new List<Face>();
            if(_vertexes.Count<3)
            return null;
            else if (_vertexes.Count == 3)
            {
                faces.Add(new Face(_vertexes[0].Index, _vertexes[1].Index, _vertexes[2].Index));
                return faces;
            }
            else {//删去一个凸点，获取一个三角形面

                if (_vertexes[0].isSalienPoint&&!isListInTriangle(_vertexes,_vertexes[_vertexes.Count - 1],_vertexes[0],_vertexes[1])) {//第一个点为凸点
                    faces.Add(new Face(_vertexes[_vertexes.Count - 1].Index, _vertexes[0].Index, _vertexes[1].Index));
                    _vertexes.Remove(_vertexes[0]);
                    List<Face> subFaces = getFacesOfSimplePolygon(_vertexes);
                    foreach (Face f in subFaces) {
                        faces.Add(f);
                    }
                    return faces;
                }
                else if (_vertexes[_vertexes.Count - 1].isSalienPoint && !isListInTriangle(_vertexes, _vertexes[_vertexes.Count - 2], _vertexes[_vertexes.Count - 1], _vertexes[0]))
                {//最后一个点为凸点
                    faces.Add(new Face(_vertexes[_vertexes.Count - 2].Index, _vertexes[_vertexes.Count - 1].Index, _vertexes[0].Index));
                    _vertexes.Remove(_vertexes[_vertexes.Count - 1]);
                    List<Face> subFaces = getFacesOfSimplePolygon(_vertexes);
                    foreach (Face f in subFaces)
                    {
                        faces.Add(f);
                    }
                    return faces;
                }
                else {//中间点为凸点
                    int temp = 1;
                    for (int i = 1; i < _vertexes.Count - 1; i++) {
                        if (_vertexes[i].isSalienPoint && !isListInTriangle(_vertexes, _vertexes[i-1], _vertexes[i], _vertexes[i+1]))
                        {
                            temp = i;
                            break;
                        }
                    }
                    faces.Add(new Face(_vertexes[temp-1].Index, _vertexes[temp].Index, _vertexes[temp+1].Index));
                    _vertexes.Remove(_vertexes[temp]);
                    List<Face> subFaces = getFacesOfSimplePolygon(_vertexes);
                    foreach (Face f in subFaces)
                    {
                        faces.Add(f);
                    }
                    return faces;
                
                }
               
            
            }
        }

        //设置所有顶点的凹凸属性
        public static List<Vertex> setIsSalientPoint(List<Vertex> vertexes){

            List<Vertex> _vertexes = new List<Vertex>();
            for (int i = 0; i < vertexes.Count; i++) {
                _vertexes.Add(vertexes[i]);
            }

                if (_vertexes.Count <= 3)
                {
                    foreach (Vertex v in _vertexes)
                    {
                        v.isSalienPoint = true;
                    }
                    return _vertexes;
                }
                else
                {
                    _vertexes[0].isSalienPoint = isSalientPoint(_vertexes[_vertexes.Count - 1], _vertexes[0], _vertexes[1]);
                    _vertexes[_vertexes.Count - 1].isSalienPoint = isSalientPoint(_vertexes[_vertexes.Count - 2], _vertexes[_vertexes.Count - 1], _vertexes[0]);

                    for (int i = 1; i < _vertexes.Count - 1; i++)
                    {
                        _vertexes[i].isSalienPoint = isSalientPoint(_vertexes[i - 1], _vertexes[i], _vertexes[i + 1]);
                    }

                    return _vertexes;
                }


        }
        
        //判断点v是否为凸点,默认z坐标均为0
        public static bool isSalientPoint(Vertex pre,Vertex v,Vertex next) {

            MyVector3 v1 = new MyVector3(v.Position.X-pre.Position.X,v.Position.Y-pre.Position.Y,v.Position.Z-pre.Position.Z);
            MyVector3 v2 = new MyVector3(next.Position.X - v.Position.X, next.Position.Y - v.Position.Y, next.Position.Z - v.Position.Z);

            MyVector3 c = new MyVector3(v1.Y * v2.Z - v2.Y * v1.Z, 
                                        v1.Z * v2.X - v2.Z * v1.X, 
                                        v1.X * v2.Y - v2.X * v1.Y);
            double k = c.Z;
            if (k > 0) return false;
            else return true;
        }

        //判断点v是否在点t1,t2,t3组成的三角形中
        public static bool isInTriangle(Vertex v, Vertex t1, Vertex t2, Vertex t3) {

            //三角形中所有点均在其余两点组成的向量的同一侧，用signOfTriangle进行标记
            double signOfTriangle = (t2.Position.X - t1.Position.X) * (t3.Position.Y - t1.Position.Y) - (t2.Position.Y - t1.Position.Y) * (t3.Position.X - t1.Position.X);

            //分别标记点v在t1t2,t2t3,t3t1向量的哪一侧
            double signOft1t2 = (t2.Position.X - t1.Position.X) * (v.Position.Y - t1.Position.Y) - (t2.Position.Y - t1.Position.Y) * (v.Position.X - t1.Position.X);
            double signOft2t3 = (t3.Position.X - t2.Position.X) * (v.Position.Y - t2.Position.Y) - (t3.Position.Y - t2.Position.Y) * (v.Position.X - t2.Position.X);
            double signOft3t1 = (t1.Position.X - t3.Position.X) * (v.Position.Y - t3.Position.Y) - (t1.Position.Y - t3.Position.Y) * (v.Position.X - t3.Position.X);


            bool d1 = (signOfTriangle * signOft1t2 > 0);
            bool d2 = (signOfTriangle * signOft2t3 > 0);
            bool d3 = (signOfTriangle * signOft3t1 > 0);

            //当点v与三角形中任一点均在其余两点组成向量的同一侧表示点v在t1,t2,t3组成的三角形中
            return (d1&&d2&&d3);
        }

        //判断点集中是否有点在t1,t2,t3组成的三角形中
        public static bool isListInTriangle(List<Vertex> vertexes, Vertex t1, Vertex t2, Vertex t3) {

            foreach (Vertex v in vertexes) {
                if (isInTriangle(v, t1, t2, t3)) return true;
            }

            return false;
        
        }
    }
}
