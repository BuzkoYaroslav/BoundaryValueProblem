using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    class Method
    {
        public static string methodIsNotApplyableString = "Method is not applyable!";
        public static string incorrectMatrixString = "Matrix has incorrect lengths!";
        public static string matricesCannotBeMultipliedString = "Matricies cannot be multiplied!";
        public static string iterationOverflowString = "Iteration overflow! (Iteration count = {0})";

        public static double epsilan = Math.Pow(10, -4);
        public static double epsilanIter = Math.Pow(10, -3);
        public static int maxIterationCount = 10000000;

        protected static Random rnd = new Random();

        protected delegate bool Condition(double func);

        protected static double RandomNumber(double a, double b)
        {
            return rnd.Next(Convert.ToInt32(a / epsilan), Convert.ToInt32(b / epsilan)) * epsilan;
        }
        protected static double SuitableRandomArgumnent(Condition cond, double a, double b)
        {
            double arg;

            do
            {
                arg = RandomNumber(a, b);
            } while (!cond(arg));

            return arg;
        }
    }
}
