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

        }
    }
}
