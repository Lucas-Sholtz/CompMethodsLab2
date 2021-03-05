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
                    indexes.Add(new Tuple<int, double>(j, absSum- Math.Abs(m[j,j])));
                }
            }
            return indexes;
        }
        public static Matrix<double> ModifyToDiagonalyDominant(Matrix<double> m, List<Tuple<int, double>> indexes)
        {
            foreach(var j in indexes)//для каждой пары индекс и разница
            {
                double value = (j.Item2 / (double)(m.RowCount - 1));//вычислить число которое нужно отнять от каждого елемента
                Console.WriteLine($"Sum = {j.Item2}");
                Console.WriteLine($"Value = {value}");
                for(int i = 0;i<m.RowCount;i++)//пройтись по колонке
                {   
                    if (i != j.Item1)//и из каждого числа не на диагонали
                    {
                        if (Math.Abs(m[i, j.Item1]) <= Math.Abs(value))
                            m[i, j.Item1] = 0.0;
                        else
                        {
                            if (m[i, j.Item1] < 0.0) //вычесть значение разницы
                                m[i, j.Item1] += value;
                            else
                                m[i, j.Item1] -= value;
                        }
                    }
                }
            }
            return m;
        }
    }
}
