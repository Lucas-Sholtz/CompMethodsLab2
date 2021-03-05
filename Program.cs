using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace CompMethodsLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //int good=0, total=0;
            int i = 4, j = 4;
            {
                var m = MatrixGenerator.HilbertMatrix(i, j);
                Console.WriteLine(m.ToMatrixString());
                var lst = MatrixOperations.CheckDiagonalyDominant(m);
                while (lst.Count != 0)
                {
                    MatrixOperations.ModifyToDiagonalyDominant(m, lst);
                    lst = MatrixOperations.CheckDiagonalyDominant(m);
                    Console.WriteLine(m.ToMatrixString());
                }
            }
            /*while (true)
            {
                //total++;
                i++;
                j++;
                int step = 0;
                var m = MatrixGenerator.HilbertMatrix(i, j);
                var m = Matrix<double>.Build.Dense(i, j);
                for(int k = 0; k < i; k++)
                {
                    for(int l = 0; l < j; l++)
                    {
                        m[k, l] = Convert.ToInt32(Console.ReadLine());
                    }
                }
                //var result = m;
                //Console.WriteLine(m.ToMatrixString());
                //find max elem m0
                while (step != i)
                {
                    var maxRow = MatrixOperations.FindMaxNumber(m.Column(step), step);
                    var maxCoords = new Tuple<int, int>(maxRow, step);
                    //var maxCoord = MatrixOperations.FindMaxNumber(m.SubMatrix(step, i - step, step, j - step));
                    //var maxCoords = new Tuple<int, int>(maxCoord.Item1 + step, maxCoord.Item2 + step);
                    //Console.WriteLine($"Max value={m[maxCoords.Item1,maxCoords.Item2]} at {maxCoords}");
                    var max = m[maxCoords.Item1, maxCoords.Item2];
                    var swapMatrix = MatrixGenerator.SwapMatrix(i, maxCoords.Item1, maxCoords.Item2);
                    m = swapMatrix * m;
                    //Console.WriteLine("Swap matrix");
                    //Console.WriteLine(swapMatrix.ToMatrixString());
                    //Console.WriteLine("M matrix after S mult");
                    //Console.WriteLine(m.ToMatrixString());
                    var translateMatrix = MatrixGenerator.TranslateMatrix(m.SubMatrix(0, i, 0, j), i, step, max);
                    m = translateMatrix * m;
                    //Console.WriteLine("Translate matrix");
                    //Console.WriteLine(translateMatrix.ToMatrixString());
                    //Console.WriteLine("M matrix after T mult");
                    //Console.WriteLine(m.ToMatrixString());
                    step++;
                }
                Console.WriteLine(m.ToString());
                bool result = true;
                int gh = 0;
                double delta = 0.001;
                while (gh < i&&result)
                {
                    if (m[gh, gh] >= 1.0-delta&& m[gh, gh] <= 1.0 + delta)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    gh++;
                }

                if (result)
                {
                    good++;
                }
                Console.WriteLine(good + " " + total);
                Console.ReadKey();
                Console.Clear();
            }*/
        }
    }
}
