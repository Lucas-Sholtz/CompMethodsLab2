using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace CompMethodsLab2
{
    class MatrixGenerator
    {
        public static Matrix<double> RandomMatrix(int i, int j)
        {
            Matrix<double> matrix = Matrix<double>.Build.Random(i,j);
            return matrix;
        }
        public static Matrix<double> RandomMatrix(int n)
        {
            Matrix<double> matrix = Matrix<double>.Build.Random(n,n);
            return matrix;
        }
        public static Matrix<double> HilbertMatrix(int i,int j)
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(i, j, (i, j) => 1.0 / ((double)(i + 1) + (double)(j + 1) - 1.0));
            return matrix;
        }
        public static Matrix<double> HilbertMatrix(int n)
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(n, n, (i, j) => 1.0 / ((double)(i + 1) + (double)(j + 1) - 1.0));
            return matrix;
        }
        public static Matrix<double> SwapMatrix(int n, int i, int j)
        {
            Matrix<double> matrix = Matrix<double>.Build.DenseIdentity(n);
            var tmp = matrix.Column(i);
            matrix.SetColumn(i, matrix.Row(j));
            matrix.SetColumn(j, tmp);
            return matrix;
        }
        public static Matrix<double> TranslateMatrix(Matrix<double> m, int n, int step, double max)
        {
            Matrix<double> matrix = Matrix<double>.Build.DenseIdentity(n);
            matrix[step, step] = 1.0 / max;
            for (int l = step + 1; l < n; l++)
            {
                matrix[l, step] = -m[l,step] / max;
            }
            return matrix;
        }
    }
}
