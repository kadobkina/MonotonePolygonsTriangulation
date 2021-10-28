using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MonotonePolygonsTriangulation
{
    public class Triangle //треугольник
    {
        private Point a, b, c;
        public Triangle(Point a, Point b, Point c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Point getA()
        {
            return a;
        }

        public Point getB()
        {
            return b;
        }

        public Point getC()
        {
            return c;
        }
    }

    public partial class Form1
    {
        // нарисовать полигон
        void drawPolygon(MouseEventArgs e)
        {
            if (polygonPoints.Count > 0)
            {
                if (polygonPoints.Count == 1)
                {
                    g.Clear(Color.White);
                }
                if (Math.Abs(polygonPoints[0].X - e.X) * Math.Abs(polygonPoints[0].Y - e.Y) < 25)
                {
                    g.DrawLine(new Pen(Color.Black, 2), polygonPoints.Last(), polygonPoints[0]);
                    isDrawingMode = false;
                    buttonTriangulation.Enabled = true;
                    return;
                }
                g.DrawLine(new Pen(Color.Black, 2), polygonPoints.Last(), e.Location);
                polygonPoints.Add(e.Location);
            }
            else
            {
                polygonPoints.Add(e.Location);
                g.FillEllipse(Brushes.Black, e.X - 2, e.Y - 2, 5, 5);
            }

        }

        void FirstLastPrev(ref Stack<Tuple<Point, string>> st, ref Tuple<Point, string> first, ref Tuple<Point, string> last, ref Tuple<Point, string> prev)
        {
            if (st.Count() == 2)
            {
                last = st.Pop(); // последняя вершина в стеке
                //stTempLast = last;
                first = st.Pop(); // первая вершина в стеке
                st.Push(first);
                st.Push(last);
            }
            last = st.Pop(); // последняя вершина в стеке
            //stTempLast = last;
            prev = st.Pop(); // предпоследняя вершина в стеке
            st.Push(prev);
            st.Push(last);
        }

        // Положение точки относительно направленного ребра
        // yb·xa - xb·ya > 0 => b слева от Oa    
        // yb·xa - xb·ya < 0 => b справа от Oa
        private bool PointIsLeft(Point edgeStart, Point edgeEnd, Point p)
        {
            int res = (p.X - edgeStart.X) * (edgeEnd.Y - edgeStart.Y) - (p.Y - edgeStart.Y) * (edgeEnd.X - edgeStart.X);
            if (res > 0)
                return true; // слева
            return false; // справа
        }

        void TriangulationPolygon()
        {
            // по часовой
            bool clockwise = true; 

            double signedArea = 0;
            for(int i = 1; i < polygonPoints.Count(); i++)
            {
                signedArea += polygonPoints[i - 1].X * polygonPoints[i].Y - polygonPoints[i].X * polygonPoints[i-1].Y;
            }
            signedArea += polygonPoints[polygonPoints.Count() - 1].X * polygonPoints[0].Y - polygonPoints[0].X * polygonPoints[polygonPoints.Count() - 1].Y;
            signedArea /= 2;

            if (signedArea < 0)
                clockwise = false; // против часовой


            var minX = polygonPoints.Min(p => p.X);
            var maxX = polygonPoints.Max(p => p.X); 
            Point minPointX = polygonPoints.Find(p => p.X == minX); // самая левая точка
            Point maxPointX = polygonPoints.Find(p => p.X == maxX); // самая правая точка
            int startPoint = polygonPoints.IndexOf(minPointX);

            List<Tuple<Point,string>> verticiesPolygon = new List<Tuple<Point, string>>();
            string chain = "top";

            if (clockwise) // если по часовой
            {
                if (polygonPoints.IndexOf(minPointX) == 0)
                for (int i = 0; i < polygonPoints.Count(); i++)
                {
                    if (polygonPoints[i] == maxPointX)
                        chain = "bot";
                    verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                }
                else
                {
                    for (int i = polygonPoints.IndexOf(minPointX); i < polygonPoints.Count(); i++)
                    {
                        if (polygonPoints[i] == maxPointX)
                            chain = "bot";
                        verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                    }
                    for (int i = 0; i < polygonPoints.IndexOf(minPointX); i++)
                    {
                        if (polygonPoints[i] == maxPointX)
                            chain = "bot";
                        verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                    }
                }
            }
            else // если против часовой
            {
                if (polygonPoints.IndexOf(minPointX) == 0)
                {
                    verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[0], chain));
                    chain = "bot";
                    for (int i = 1; i < polygonPoints.Count(); i++)
                    {
                        verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                        if (polygonPoints[i] == maxPointX)
                            chain = "top";                      
                   }
                }
                else
                {
                    verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[polygonPoints.IndexOf(minPointX)], chain));
                    chain = "bot";
                    for (int i = polygonPoints.IndexOf(minPointX)+1; i < polygonPoints.Count(); i++)
                    {
                        verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                        if (polygonPoints[i] == maxPointX)
                            chain = "top";
                    }
                    for (int i = 0; i < polygonPoints.IndexOf(minPointX); i++)
                    {
                        verticiesPolygon.Add(new Tuple<Point, string>(polygonPoints[i], chain));
                        if (polygonPoints[i] == maxPointX)
                            chain = "top";
                    }
                }
            }

            // отсортированные по X вершины полигона
            verticiesPolygon.Sort((a, b) => a.Item1.X.CompareTo(b.Item1.X));

            // проверка вершин
            label1.Text = "";
            int ind = 1;
            foreach (Tuple<Point, string> p in verticiesPolygon)
            {
                label1.Text += ind + " " + p.Item1 + " " + p.Item2 + "\n";
                ind++;
            }

            // стек для триангуляции
            Stack<Tuple<Point, string>> stck = new Stack<Tuple<Point, string>>();
            // треугольники
            Triangle[] triangles = new Triangle[polygonPoints.Count() - 2];
            int trainPos = 0;

            stck.Push(verticiesPolygon[0]); // в стек заносим левую точку
            Tuple<Point, string> stFirst = verticiesPolygon[0];
            Tuple<Point, string> stLast = null, stPrevLast = null, stTempLast = null;
            for (int i = 1; i < verticiesPolygon.Count(); i++)
             {
                if (stck.Count() == 1)
                    stck.Push(verticiesPolygon[i]); // добавляем в стек следующую точку из отсортированных вершин

                else  // проверяем, не образуют ли они треугольник или веерообразный полигон
                {
                    FirstLastPrev(ref stck, ref stFirst, ref stLast, ref stPrevLast);

                    // 1 - если Vi соседняя с St и не соседняя с S1
                    if ((Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) == 1 || Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) == polygonPoints.Count() - 1)
                        && Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) != 1 && Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) != polygonPoints.Count() - 1)
                    {
                        //  В алгоритме используется тот факт, что угол vistst-1 < 180 только в том случае, когда
                        //  либо st-1 лежит слева от вектора vist, если vi принадлежит полигону pi-1 из верхней цепочки,
                        //  либо st-1 лежит справа от вектора vist, если vi принадлежит нижней цепочке.
                        if (verticiesPolygon[i].Item2 == "top" && PointIsLeft(verticiesPolygon[i].Item1, stLast.Item1, stPrevLast.Item1) ||
                            verticiesPolygon[i].Item2 == "bot" && !PointIsLeft(verticiesPolygon[i].Item1, stLast.Item1, stPrevLast.Item1))
                        {
                            Point ab = new Point(stLast.Item1.X - stPrevLast.Item1.X, stLast.Item1.Y - stPrevLast.Item1.Y);
                            Point bc = new Point(verticiesPolygon[i].Item1.X - stLast.Item1.X, verticiesPolygon[i].Item1.Y - stLast.Item1.Y);
                            double rot = (ab.X * bc.Y - ab.Y * bc.X) * Math.PI / 180;
                            // если внутренний угол St - 1 St Vi меньше 180 градусов
                            if (stck.Count() > 1 && rot < 180)
                            {
                                // в список треугольников заносим этот треугольник
                                triangles[trainPos] = new Triangle(stPrevLast.Item1, stLast.Item1, verticiesPolygon[i].Item1);
                                stck.Pop();  // из стека удаляем St
                            }
                        }
                        // после этого в стек заносится vi
                        stck.Push(verticiesPolygon[i]);
                        FirstLastPrev(ref stck, ref stFirst, ref stLast, ref stPrevLast);
                    }

                    // 2 - если Vi – соседняя с с S1, но не соседняя с St
                    else if (Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) != 1 && Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) != polygonPoints.Count() - 1
                        && (Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) == 1 || Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) == polygonPoints.Count() - 1))
                    {
                        stTempLast = stLast;
                        // получаем веерообразный полигон
                        while (stck.Count() > 1)
                        {
                            // в список треугольников заносим этот треугольник
                            triangles[trainPos] = new Triangle(stPrevLast.Item1, stLast.Item1, verticiesPolygon[i].Item1);
                            if (stck.Count() > 2)
                            {
                                stck.Pop();
                                stLast = stck.Pop();
                                stPrevLast = stck.Pop();
                                stck.Push(stPrevLast);
                                stck.Push(stLast);
                            }
                            else
                                stck.Pop();
                            trainPos++;
                        }
                        while (stck.Count() > 0)
                            stck.Pop();
                        stck.Push(stTempLast);
                        stck.Push(verticiesPolygon[i]);
                    }

                    // 3 - если Vi – соседняя c S1 и с St
                    else if ((Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) == 1 || Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stLast.Item1)) == polygonPoints.Count() - 1) 
                        && (Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) == 1 || Math.Abs(polygonPoints.IndexOf(verticiesPolygon[i].Item1) - polygonPoints.IndexOf(stFirst.Item1)) == polygonPoints.Count() - 1))
                    {
                        stLast = stPrevLast;
                        stPrevLast = stck.Pop();
                        triangles[trainPos] = new Triangle(stPrevLast.Item1, stLast.Item1, verticiesPolygon[i].Item1);
                        // получаем веерообразный полигон
                        while (stck.Count() > 1)
                        {
                            // в список треугольников заносим этот треугольник
                            triangles[trainPos] = new Triangle(stPrevLast.Item1, stLast.Item1, verticiesPolygon[i].Item1);
                            stLast = stPrevLast;
                            if (stck.Count() > 1)
                                stPrevLast = stck.Pop();
                        }
                    }
                    // 4 - eсли ни одна из предыдущих ситуаций не подходит, то просто добавляем Vi в стек.
                    else
                        stck.Push(verticiesPolygon[i]);
                }

            }

            // рисуем треугольники
            foreach(var tr in triangles)
            {
                if (tr != null)
                {
                    g.DrawLine(new Pen(Color.Black), tr.getA(), tr.getB());
                    g.DrawLine(new Pen(Color.Black), tr.getB(), tr.getC());
                    g.DrawLine(new Pen(Color.Black), tr.getA(), tr.getC());
                }
            }
        }
    }
}
