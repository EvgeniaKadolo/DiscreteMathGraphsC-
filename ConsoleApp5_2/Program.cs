using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] lines = File.ReadAllLines("graph2.txt");
                int[,] num = new int[lines.Length, lines[0].Split(' ').Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (int.TryParse(temp[j], out _) && i != j) num[i, j] = Convert.ToInt32(temp[j]);
                        else num[i, j] = -1;
                    }
                }

                int sum = 0;
                List<int[]> r = new List<int[]>();
                int[] marks = new int[num.GetLength(0)];

                marks[0] = 1;
                int min = int.MaxValue;
                int indexJ = 0;
                int indexI = 0;
                bool flag = false;
                int flagMarks = 0;

                while (flagMarks != marks.Length)
                {
                    flag = false;
                    flagMarks = 0;
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
                                    indexI = i;
                                    flag = true;
                                }
                            }
                            
                        }
                    }
                    if (flag)
                    {
                        sum += min;
                        int[] r_ = { indexI, indexJ };
                        r.Add(r_);
                        marks[indexJ] = 1;
                    }
                    for (int i = 0; i < marks.Length; i++)
                    {
                        if (marks[i] == 1)
                        {
                            flagMarks++;
                        }
                    }
                }

                Console.WriteLine("Множество ребер кратчайшего остова:");
                int a, b;
                for (int i = 0; i < r.Count; i++)
                {
                    a = r[i][0] + 1;
                    b = r[i][1] + 1;
                    Console.WriteLine(a + " " + b);
                }
                Console.WriteLine("Сумма длин ребер кратчайшего остова: " + sum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
