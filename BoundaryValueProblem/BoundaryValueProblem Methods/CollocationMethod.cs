using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library;

namespace BoundaryValueProblem.BoundaryValueProblem_Methods
{
    class CollocationMethod : BoundaryValueTaskSolver
    {
        private GaussMethod method;
        private double epsilan = 0.01;

        public CollocationMethod()
        {
            method = new GaussMethod();
        }

        public KeyValuePair<double, double>[] Solve(BoundaryValueTask task, int n)
        {
            MathFunction[] u = new MathFunction[n + 1];
            for (int i = 0; i <= n; i++)
                u[i] = UK(task, i);

            double[,] matrix = new double[n, n];
            double[] vect = new double[n];
            double h = (task.b - task.a - 2 * epsilan) / (n - 1);

            for (int i = 0; i < n; i++)
            {
                double xi = task.a + epsilan + i * h;

                vect[i] = task.FX.Calculate(xi) - task.LOperator(u[0], xi);
                for (int j = 0; j < n; j++)
                    matrix[i, j] = task.LOperator(u[j + 1], xi);
            }

            Vector C = method.Solve(new SLAE(matrix, vect));
            MathFunction yn = u[0];
            for (int i = 1; i <= n; i++)
                yn += C[i - 1] * u[i];

            h = (task.b - task.a) / n;
            KeyValuePair<double, double>[] result = new KeyValuePair<double, double>[n + 1];
            for (int i = 0; i < result.Length; i++)
                result[i] = new KeyValuePair<double, double>(task.a + i * h, yn.Calculate(task.a + i * h));

            return result;               
        }

        private MathFunction UK(BoundaryValueTask task, int k)
        {
            if (k == 0)
            {
                double[,] m = new double[,]
                {
                    {task.alpha0 * task.a + task.alpha1, task.alpha0 },
                    {task.betta0 * task.b + task.betta1, task.betta0 }
                };
                double[] v = new double[] { task.A, task.B };

                Vector res = method.Solve(new SLAE(m, v));

                return new XFunction(res[0]) + res[1];
            }

            return ((new XFunction(1.0) - task.a) ^ k) * 
                ((new XFunction(1.0) - task.b) ^ 2);
        }
    }
}
