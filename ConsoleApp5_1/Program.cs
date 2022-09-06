using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] lines = File.ReadAllLines("graph1.txt");
                List<List<int>> num = new List<List<int>>();
                List<int> t = new List<int>();
                for (int i = 0; i < lines.Length; i++)
                {
                    t = new List<int>();
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                    {
                        if (int.TryParse(temp[j], out _))
                        {
                            t.Add(j);
                        }
                    }
                    num.Add(t);
                }

                int[] marksS = new int[num.Count];
                List<int> numbers = new List<int>();
                Stack<int> numbersS = new Stack<int>();

                Console.Write("Введите номер начальной вершины: ");
                int n = int.Parse(Console.ReadLine());
                if (n < 1 || n > num.Count) throw new Exception();
                numbers.Add(n - 1);
                numbersS.Push(n - 1);
                marksS[n - 1] = 1;

                while(numbersS.Count != 0)
                {
                    int count = 0;
                    while (num[numbersS.Peek()].Count != count)
                    {
                        count = 0;
                        for (int i = 0; i < num[numbersS.Peek()].Count; i++)
                        {
                            if (marksS[num[numbersS.Peek()][i]] != 1)
                            {
                                marksS[num[numbersS.Peek()][i]] = 1;
                                numbers.Add(num[numbersS.Peek()][i]);
                                numbersS.Push(num[numbersS.Peek()][i]);
                                i = -1;
                            }
                            else count++;
                        }
                    }
                    bool flag = false;
                    while (!flag)
                    {
                        if (numbersS.Count != 0) numbersS.Pop();
                        if (numbersS.Count != 0)
                        {
                            for (int i = 0; i < num[numbersS.Peek()].Count; i++)
                            {
                                if (marksS[num[numbersS.Peek()][i]] != 1) flag = true;
                            }
                        }
                        if (numbersS.Count == 0) flag = true;
                    }
                }

                Console.Write("Порядок обхода в глубину: ");
                int b = 0;
                int start = 0;
                foreach (int i in numbers)
                {
                    b = i + 1;
                    if (start == 0) Console.Write(b);
                    else Console.Write("->" + b);
                    start = 1;
                }
                Console.WriteLine();


                int[] marksQ = new int[num.Count];
                numbers.Clear();
                Queue<int> numbersQ = new Queue<int>();

                numbers.Add(n - 1);
                numbersQ.Enqueue(n - 1);
                marksQ[n - 1] = 1;

                while (numbersQ.Count != 0)
                {
                    int count = 0;
                    while (num[numbersQ.Peek()].Count != count)
                    {
                        count = 0;
                        for (int i = 0; i < num[numbersQ.Peek()].Count; i++)
                        {
                            if (marksQ[num[numbersQ.Peek()][i]] != 1)
                            {
                                marksQ[num[numbersQ.Peek()][i]] = 1;
                                numbers.Add(num[numbersQ.Peek()][i]);
                                numbersQ.Enqueue(num[numbersQ.Peek()][i]);
                                i = -1;
                            }
                            else count++;
                        }
                    }
                    bool flag = false;
                    while (!flag)
                    {
                        if (numbersQ.Count != 0) numbersQ.Dequeue();
                        if (numbersQ.Count != 0)
                        {
                            for (int i = 0; i < num[numbersQ.Peek()].Count; i++)
                            {
                                if (marksQ[num[numbersQ.Peek()][i]] != 1) flag = true;
                            }
                        }
                        if (numbersQ.Count == 0) flag = true;
                    }
                }

                Console.Write("Порядок обхода в ширину: ");
                int b_ = 0;
                int start_ = 0;
                foreach (int i in numbers)
                {
                    b_ = i + 1;
                    if (start_ == 0) Console.Write(b_);
                    else Console.Write("->" + b_);
                    start_ = 1;
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
