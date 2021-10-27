using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MonotonePolygonsTriangulation
{
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
            foreach (Tuple<Point, string> p in verticiesPolygon)
            {
                label1.Text += p.Item1 + " " + p.Item2 + "\n";
            }

            // стек для триангуляции
            Stack<Tuple<Point, string>> st = new Stack<Tuple<Point, string>>();


        }
    }
}
