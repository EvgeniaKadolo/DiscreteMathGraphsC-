using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp5_4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] lines = File.ReadAllLines("graph4.txt");
                double[,] D = new double[lines.Length, lines[0].Split(' ').Length];
                double[,] S = new double[lines.Length, lines[0].Split(' ').Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (int.TryParse(temp[j], out _) && i != j)
                        {
                            D[i, j] = Convert.ToInt32(temp[j]);
                            S[i, j] = j;
                        }
                        else
                        {
                            D[i, j] = double.PositiveInfinity;
                            S[i, j] = double.PositiveInfinity;
                        }
                    }
                }

                Console.Write("Введите номер начальной вершины: ");
                int n = int.Parse(Console.ReadLine());
                if (n < 1 || n > D.GetLength(0)) throw new Exception();
                Console.Write("Введите номер конечной вершины: ");
                int k = int.Parse(Console.ReadLine());
                if (k < 1 || k > D.GetLength(0)) throw new Exception();

                int count = 0;

                while (count != D.GetLength(0))
                {
                    for (int i = 0; i < D.GetLength(0); i++)
                    {
                        for (int j = 0; j < D.GetLength(1); j++)
                        {
                            if (i != j)
                            {
                                if (D[i, count] + D[count, j] < D[i, j]) S[i, j] = count;
                                D[i, j] = Math.Min(D[i, j], D[i, count] + D[count, j]);
                            }
                        }
                    }
                    count++;
                }

                Console.WriteLine("Матрица длин кратчайших путей: ");
                for (int i = 0; i < D.GetLength(0); i++)
                {
                    for (int j = 0; j < D.GetLength(1); j++)
                    {
                        if (D[i, j] == double.PositiveInfinity) Console.Write("-\t");
                        else Console.Write(D[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Матрица кратчайших путей: ");
                double a;
                for (int i = 0; i < S.GetLength(0); i++)
                {
                    for (int j = 0; j < S.GetLength(1); j++)
                    {
                        if (S[i, j] == double.PositiveInfinity) Console.Write("-\t");
                        else
                        {
                            a = S[i, j] + 1;
                            Console.Write(a + "\t");
                        }
                    }
                    Console.WriteLine();
                }

                List<double> s = new List<double>();
                s.Add(n);
                s.Add(k);
                int countFlag = 0;

                while (countFlag != s.Count - 1)
                {
                    countFlag = 0;
                    for (int i = 0; i < s.Count - 1; i++)
                    {
                        if (S[(int)s[i] - 1, (int)s[i + 1] - 1] != (int)s[i + 1] - 1)
                        {
                            s.Insert(i + 1, S[(int)s[i] - 1, (int)s[i + 1] - 1] + 1);
                            break;
                        }
                        else countFlag++;
                    }
                }

                Console.WriteLine("Кратчайшее расстояние между " + n + " и " + k + " равно: " + D[n - 1, k - 1]);
                Console.Write("Кратчайший путь между " + n + " и " + k + " равен: ");
                foreach (double i in s)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
