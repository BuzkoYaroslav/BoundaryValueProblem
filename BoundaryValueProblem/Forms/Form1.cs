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
using System.Threading;
using BoundaryValueProblem.BoundaryValueProblem_Methods;

namespace BoundaryValueProblem
{
    using Condition = Tuple<double, double, double, double>;

    public partial class Form1 : Form
    {
        private const int n = 100;
        //private BoundaryValueTask task = new BoundaryValueTask(new XFunction(-2.0),
        //    -2, new XFunction(-4.0), 0, 1, new Condition(1, -1, 0, 0), new Condition(2, -1, 1, 1));

        //private MathFunction accurate = new XFunction(1.0) +
        //    new StepFunction(1.0, Math.E, new PowerFunction(1.0, new XFunction(1.0), 2));
        private BoundaryValueTask task = new BoundaryValueTask(0,
            1, new XFunction(-1.0), 0, 1, new Condition(1, 0, 0, 0), new Condition(1, 0, 1, 0));

        private MathFunction accurate = new SinFunction(1.0 / Math.Sin(1), new XFunction(1.0)) - new XFunction(1.0);
        private KeyValuePair<double, double>[] gridMethod;
        private KeyValuePair<double, double>[] collocationMethod;

        private Mutex gridMethodMutex = new Mutex();
        private Mutex collocationMethodMutex = new Mutex();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = task.ToString();
            new Thread(() =>
            {
                gridMethodMutex.WaitOne();
                gridMethod = new GridMethod().Solve(task, n);
                gridMethodMutex.ReleaseMutex();
            }).Start();
            new Thread(() =>
            {
                collocationMethodMutex.WaitOne();
                collocationMethod = new CollocationMethod().Solve(task, n);
                collocationMethodMutex.ReleaseMutex();
            }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridMethodMutex.WaitOne();

            new Table(accurate, gridMethod).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            collocationMethodMutex.WaitOne();

            new Table(accurate, collocationMethod).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gridMethodMutex.WaitOne();

            new Graph(accurate, gridMethod).Show();

            gridMethodMutex.ReleaseMutex();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            collocationMethodMutex.WaitOne();

            new Graph(accurate, collocationMethod).Show();

            collocationMethodMutex.ReleaseMutex();
        }
    }
}
