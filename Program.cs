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
            
            {
                /*var m = MatrixGenerator.HilbertMatrix(i, j);
                var lst = MatrixOperations.CheckDiagonalyDominant(m);
                while (lst.Count != 0)
                {
                    MatrixOperations.ModifyToDiagonalyDominant(m, lst);
                    lst = MatrixOperations.CheckDiagonalyDominant(m);
                }
                MatrixOperations.DLUDecomposition(m);*/
            }
            while (true)
            {
                Console.Write("Enter matrix size: ");
                int n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Random matrix or Hilbert matrix. Type 1 or 2");
                int p = Convert.ToInt32(Console.ReadLine());
                bool type;
                if (p == 1)
                    type = true;
                else
                    type = false;
                Console.WriteLine("Enter method number. 1 for Gauss, 2 for Jacobi, 3 for Seidel, 0 exit.");
                int choice = Convert.ToInt32(Console.ReadLine());
                Matrix<double> m;
                if (type)
                {
                    m = MatrixGenerator.RandomMatrix(n, n + 1);
                }
                else
                {
                    m = MatrixGenerator.HilbertMatrix(n, n + 1);
                }
                //var m = Matrix<double>.Build.Dense(n, n+1);
                //for (int k = 0; k < n; k++)
                //{
                //    for (int l = 0; l < n+1; l++)
                //    {
                //        m[k, l] = Convert.ToInt32(Console.ReadLine());
                //    }
                //}

                var bRow = m.SubMatrix(0, n, n, 1);
                m = m.SubMatrix(0, n, 0, n);
                Console.WriteLine(m);
                Console.WriteLine(bRow);
                Console.WriteLine("*********");
                //Console.ReadKey();
                var newRow = Matrix<double>.Build.Dense(n, 1);
                
                switch (choice)
                {
                    case 1:
                        {
                            int step = 0;
                            while (step != n)
                            {
                                var maxRow = MatrixOperations.FindMaxNumber(m.Column(step), step);
                                var maxCoords = new Tuple<int, int>(maxRow, step);
                                //var maxCoord = MatrixOperations.FindMaxNumber(m.SubMatrix(step, i - step, step, j - step));
                                //var maxCoords = new Tuple<int, int>(maxCoord.Item1 + step, maxCoord.Item2 + step);
                                //Console.WriteLine($"Max value={m[maxCoords.Item1,maxCoords.Item2]} at {maxCoords}");
                                var max = m[maxCoords.Item1, maxCoords.Item2];
                                var swapMatrix = MatrixGenerator.SwapMatrix(n, maxCoords.Item1, maxCoords.Item2);
                                m = swapMatrix * m;
                                bRow = swapMatrix * bRow;
                                //Console.WriteLine("Swap matrix");
                                //Console.WriteLine(swapMatrix.ToMatrixString());
                                //Console.WriteLine("M matrix after S mult");
                                //Console.WriteLine(m.ToMatrixString());
                                var translateMatrix = MatrixGenerator.TranslateMatrix(m.SubMatrix(0, n, 0, n), n, step, max);
                                m = translateMatrix * m;
                                bRow = translateMatrix * bRow;
                                //Console.WriteLine("Translate matrix");
                                //Console.WriteLine(translateMatrix.ToMatrixString());
                                //Console.WriteLine("M matrix after T mult");
                                //Console.WriteLine(m.ToMatrixString());
                                step++;
                            }
                            Console.WriteLine(m.ToString());
                            //bRow[n - 1, 0] = bRow[n - 1, 0] / m[n - 1, n - 1];
                            for(int i = n - 1; i >= 0; i--)
                            {
                                for(int j = n - 1; j > i; j--)
                                {
                                    bRow[i, 0] -= bRow[j, 0] * m[i, j];
                                }
                                bRow[i, 0] = bRow[i, 0] / m[i, i];
                            }
                            Console.WriteLine(bRow);
                        }
                        break;
                    case 2:
                        {
                            var lst = MatrixOperations.CheckDiagonalyDominant(m);
                            while (lst.Count != 0)
                            {
                                MatrixOperations.ModifyToDiagonalyDominant(m, lst);
                                lst = MatrixOperations.CheckDiagonalyDominant(m);
                            }


                            for (int i = 0; i < n; i++)
                            {
                                newRow[i, 0] = 0.0;
                                for (int j = 0; j < n; j++)
                                {
                                    newRow[i, 0] += m[i, j];
                                }
                            }
                            bRow = newRow;


                            var matrices = MatrixOperations.DLUDecomposition(m);
                            var d = matrices[0];
                            var invD = d.Inverse();
                            var lu = matrices[1];
                            var b = -invD*lu;
                            var g = invD * bRow;

                            Console.Write("Enter epsilon: ");
                            double eps = Convert.ToDouble(Console.ReadLine());
                            Matrix<double> oneRow = Matrix<double>.Build.Dense(n, 1);//5 1 16
                            for(int i = 0; i < n; i++)                               //3 4 13
                            {
                                oneRow[i, 0] = 1.0;
                            }
                            var xn = b * bRow + g;
                            var xnp1 = b * xn + g;
                            while (MatrixOperations.CalculateNorm(xn,xnp1) > eps)
                            {
                                xn = xnp1;
                                xnp1 = b * xn + g;
                            }
                            Console.WriteLine(xnp1);
                        }
                        break;
                    case 3:
                        {
                            var lst = MatrixOperations.CheckDiagonalyDominant(m);
                            while (lst.Count != 0)
                            {
                                MatrixOperations.ModifyToDiagonalyDominant(m, lst);
                                lst = MatrixOperations.CheckDiagonalyDominant(m);
                            }
                            var matrices = MatrixOperations.LUDecomposition(m);
                            var l = matrices[0];
                            var u = matrices[1];
                            Matrix<double> oneRow = Matrix<double>.Build.Dense(n, 1);//5 1 16
                            for (int i = 0; i < n; i++)                               //3 4 13
                            {
                                oneRow[i, 0] = 1.0;
                            }
                            var t = -l.Inverse() * u;
                            var c = l.Inverse() * bRow;
                            var xn = t * oneRow + c;
                            var xnp1 = t * xn + c;

                            Console.Write("Enter epsilon: ");
                            double eps = Convert.ToDouble(Console.ReadLine());

                            while (MatrixOperations.CalculateNorm(xn, xnp1) > eps)
                            {
                                xn = xnp1;
                                xnp1 = t * xn + c;
                            }
                            Console.WriteLine(xnp1);
                        }
                        break;
                    default:
                        return;
                }
            }
            /*while (true)
            {
                //total++;
                i++;
                j++;
                int step = 0;
                var m = MatrixGenerator.HilbertMatrix(i, j);
                
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
