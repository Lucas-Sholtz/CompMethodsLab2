using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace CompMethodsLab2
{
    class MatrixOperations
    {
        public static Tuple<int, int> FindMaxNumber(Matrix<double> m)
        {
            double max = 0;
            int row = 0, column = 0;
            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < m.ColumnCount; j++)
                {
                    if (Math.Abs(m[i, j]) >= max)
                    {
                        max = Math.Abs(m[i, j]);
                        row = i;
                        column = j;
                    }
                }
            }
            return new Tuple<int, int>(row, column);
        }
        public static int FindMaxNumber(Vector<double> vs, int step)
        {
            int row = 0;
            double max = 0;
            for (int i = step; i < vs.Count; i++)
            {
                if (Math.Abs(vs[i]) >= max)
                {
                    max = Math.Abs(vs[i]);
                    row = i;
                }
            }
            return row;
        }
        public static List<Tuple<int, double>> CheckDiagonalyDominant(Matrix<double> m)
        {
            List<Tuple<int,double>> indexes = new List<Tuple<int,double>>();
            for(int j = 0; j< m.ColumnCount;j++)
            {
                double absSum = 0;
                for(int i = 0; i < m.RowCount; i++)
                {
                    if (j != i)
                    {
                        absSum += Math.Abs(m[i, j]);
                    }
                }
                if (Math.Abs(m[j, j]) < absSum)
                {
                    indexes.Add(new Tuple<int, double>(j, absSum));
                }
            }
            return indexes;
        }
        public static Matrix<double> ModifyToDiagonalyDominant(Matrix<double> m, List<Tuple<int, double>> indexes)
        {
            foreach(var j in indexes)//для каждой пары индекс и разница добавить 
            {
                m[j.Item1, j.Item1] += j.Item2;
            }
            return m;
        }
        public static List<Matrix<double>> DLUDecomposition(Matrix<double> m)
        {
            List<Matrix<double>> matrices = new List<Matrix<double>>();
            Matrix<double> diagonal = Matrix<double>.Build.DenseIdentity(m.ColumnCount);
            for(int i= 0; i < m.ColumnCount; i++)
            {
                diagonal[i, i] = m[i, i];
            }
            Matrix<double> topDown = m - diagonal;
            Console.WriteLine(diagonal.ToMatrixString());
            Console.WriteLine(topDown.ToMatrixString());
            matrices.Add(diagonal);
            matrices.Add(topDown);
            return matrices;
        }
        public static List<Matrix<double>> LUDecomposition(Matrix<double> m)
        {
            List<Matrix<double>> matrices = new List<Matrix<double>>();
            var l = m.LowerTriangle();
            var u = m.StrictlyUpperTriangle();
            matrices.Add(l);
            matrices.Add(u);
            return matrices;
        }
        public static double CalculateNorm(Matrix<double> m1, Matrix<double> m2)
        {
            double delta = 0;
            for(int i = 0; i < m1.RowCount; i++)
            {
                delta += Math.Pow(Math.Abs(m1[i, 0] - m2[i, 0]), 2);
            }
            delta = Math.Sqrt(delta);
            return delta;
        }
    }
}
