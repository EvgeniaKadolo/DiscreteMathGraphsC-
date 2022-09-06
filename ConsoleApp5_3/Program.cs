using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp5_3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] lines = File.ReadAllLines("graph3.txt");
                int[,] num = new int[lines.Length, lines[0].Split(' ').Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (int.TryParse(temp[j], out _) && i != j) num[i, j] = Convert.ToInt32(temp[j]);
                        else num[i, j] = int.MaxValue - 1000000;
                    }
                }

                int[] marks = new int[num.GetLength(0)];

                Console.Write("Введите номер начальной вершины: ");
                int n = int.Parse(Console.ReadLine());
                if (n < 1 || n > num.GetLength(0)) throw new Exception();
                Console.Write("Введите номер конечной вершины: ");
                int k = int.Parse(Console.ReadLine());
                if (k < 1 || k > num.GetLength(0)) throw new Exception();

                marks[n - 1] = 1;
                int min;
                int indexJ = 0;
                bool flag;
                int[] D = new int[num.GetLength(0)];
                int[] P = new int[num.GetLength(0)];
                int flagMarks = 0;
                for (int i = 0; i < D.Length; i++)
                {
                    if (i != n - 1)
                    {
                        D[i] = num[n - 1, i];
                        P[i] = n - 1;
                    }
                }

                while (flagMarks != marks.Length)
                {
                    flagMarks = 0;
                    flag = false;
                    min = int.MaxValue;
                    for (int i = 0; i < marks.Length; i++)
                    {
                        if (marks[i] == 1)
                        {
                            for (int j = 0; j < num.GetLength(1); j++)
                            {
                                if (num[i, j] > 0 && num[i, j] < min && marks[j] != 1)
                                {
                                    min = num[i, j];
                                    indexJ = j;
                                    flag = true;
                                }
                            }

                        }
                    }
                    if (flag)
                    {
                        marks[indexJ] = 1;
                    }
                    for (int i = 0; i < marks.Length; i++)
                    {
                        if (marks[i] != 1)
                        {
                            if (D[indexJ] + num[indexJ, i] < D[i]) P[i] = indexJ;
                            D[i] = Math.Min(D[i], D[indexJ] + num[indexJ, i]);
                        }
                        if (marks[i] == 1)
                        {
                            flagMarks++;
                        }
                    }
                }

                List<int> r = new List<int>();
                r.Add(k - 1);

                for (int i = 0; i < r.Count; i++)
                {
                    r.Add(P[r[i]]);
                    if (P[r[i]] == n - 1) break;
                }

                Console.WriteLine("Длина кратчайшего пути равна: " + D[k - 1]);
                Console.Write("Кратчайший путь: ");
                int a;

                for (int i = r.Count - 1; i > -1; i--)
                {
                    a = r[i] + 1;
                    if (i == r.Count - 1) Console.Write(a);
                    else Console.Write("->" + a);
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
