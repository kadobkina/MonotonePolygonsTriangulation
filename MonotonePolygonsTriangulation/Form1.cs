using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonotonePolygonsTriangulation
{
    public partial class Form1 : Form
    {
        List<Point> polygonPoints = new List<Point>();
        Graphics g;
        bool isSomethingOnScreen = false;
        bool isDrawingMode = false;

        public Form1()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
        }

        private void buttonPolygon_Click(object sender, EventArgs e)
        {
            canvas.Image = new Bitmap(1300, 900);
            polygonPoints.Clear();

            // изменение кнопки
            if (!isSomethingOnScreen)
            {
                isDrawingMode = true;
                buttonPolygon.Text = "Очистить";
            }
            else
            {
                isDrawingMode = false;
                buttonPolygon.Text = "Нарисовать полигон";
            }
            isSomethingOnScreen = !isSomethingOnScreen;
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawingMode)
                drawPolygon(e);
        }

        private void buttonTriangulation_Click(object sender, EventArgs e)
        {
            TriangulationPolygon();
        }
    }
}
