using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using library;

namespace BoundaryValueProblem
{
    public partial class Graph : Form
    {
        private GraphicPainter painter;
        private Random rnd = new Random();
        private const int width = 3;

        public Graph()
        {
            InitializeComponent();
            painter = new GraphicPainter(pictureBox1);
        }
        public Graph(MathFunction accurate, KeyValuePair<double, double>[] dots): this()
        {
            ConfigureBoundaries(accurate, dots[0].Key, dots[dots.Length - 1].Key);
            painter.Draw(accurate, GetRandColor(), width);
            painter.DrawPath(painter.PathForDots(dots), GetRandColor(), width, GetRandColor(), 0.5);
        }

        private void Graph_Load(object sender, EventArgs e)
        {

        }

        private void ConfigureBoundaries(MathFunction func, double minX, double maxX)
        {
            if (minX < 0)
                minX -= 2;
            else
                minX = -1;
            if (maxX > 0)
                maxX += 2;
            else
                maxX = 1;

            double minY = func.MinValue(minX, maxX, false),
                   maxY = func.MaxValue(minX, maxX, false);

            if (minY < 0)
                minY -= 2;
            else
                minY = -1;
            if (maxY > 0)
                maxY += 2;
            else
                maxY = 1;

            painter.XBounds = new Point((int)minX, (int)maxX);
            painter.YBounds = new Point((int)minY, (int)maxY);
        }
        private Color GetRandColor()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
