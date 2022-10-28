using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab2
{
    internal class Program
    {
        [STAThread]
        static double FindQ1(int[] stat)
        {
            double Q1;
            Q1 = Math.Floor((stat.Length + 1) * 0.25f) - 1;
            Q1 = stat[(int)Q1] + (((stat[(int)Q1 + 1] - stat[(int)Q1])) * 0.25f);
            Console.WriteLine("Q1 is " + Q1);
            return Q1;
        }
        static double FindQ3(int[] stat)
        {
            double Q3;
            Q3 = Math.Floor((stat.Length + 1) * 0.75f) - 1;
            Q3 = stat[(int)Q3] + (((stat[(int)Q3 + 1] - stat[(int)Q3])) * 0.75f);
            Console.WriteLine("Q3 is " + Q3);
            return Q3;
        }
        static double FindP90(int[] stat)
        {
            double P90;
            P90 = Math.Floor((stat.Length + 1) * 0.9f) - 1;
            P90 = stat[(int)P90] + (((stat[(int)P90 + 1] - stat[(int)P90])) * 0.9f);
            Console.WriteLine("P90 is " + P90);
            return P90;
        }
        static double FindMAD(int[] stat, int[] element, int[] freq)
        {
            double MAD = 0;
            double xser = Xser(stat);
            for (int i = 0; i < element.Length; i++)
            {
                MAD += (freq[i] * Math.Abs(element[i] - xser));
            }
            MAD /= stat.Length;
            Console.WriteLine("MAD is: " + MAD);
            return MAD;
        }
        static double Xser(int[] stat)
        {
            int sum = 0;
            for (int i = 0; i < stat.Length; i++)
            {
                sum += stat[i];
            }
            double xser = sum / (double)stat.Length;
            Console.WriteLine(xser);
            return xser;
        }

        static double GetDeviation(int[] stat, int[] element, int[] freq)
        {
            double variance = 0;
            double xser = Xser(stat);
            for (int i = 0; i < element.Length; i++)
            {
                variance += freq[i] * Math.Pow(element[i] - xser, 2.0);
            }

            variance /= stat.Length;
            Console.WriteLine("Variance is: " + variance);

            double deviation = Math.Sqrt(variance);
            Console.WriteLine("Deviation is: " + deviation);
            return deviation;
        }

        static double Task3(double deviation, double xser)
        {
            Console.WriteLine("\nTask 3: ");
            Console.WriteLine("The formula is: y = ax + b");
            Console.WriteLine("[Xser * a + b = 95");
            Console.WriteLine("[100 * a + b = 100");

            double[,] A = { { 1, -1 }, { -100, xser } };
            double[,] B = { { 95 }, { 100 } };

            //double[,] A2 = { { 1, -1 }, { -100, xser } };

            double divide = (xser * A[0, 0]) - (A[0, 1] * A[1, 0]);
            A[0, 0] /= divide;
            A[0, 1] /= divide;
            A[1, 0] /= divide;
            A[1, 1] /= divide;


            //Console.WriteLine(xser);
            double[,] result = MultiplyMatrix(A, B);
            Console.WriteLine("a = " + result[0, 0] + ";   b = " + result[1, 0]);
            Console.WriteLine("\n>> y = " + result[0, 0] + $" * {xser}" + " + " + result[1, 0]);

            double Ymean = result[0, 0] * xser + result[1, 0];
            Console.WriteLine("> y mean is  = " + Math.Ceiling(Ymean));
            double YDeviation = Math.Abs(result[0, 0]) * deviation;
            Console.WriteLine("Y Deviation is: " + YDeviation);
            return YDeviation;
        }
        static public double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            if (cA != rB)
            {
                Console.WriteLine("Matrixes can't be multiplied!!");
            }
            else
            {
                double temp = 0;
                double[,] result = new double[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        result[i, j] = temp;
                    }
                }

                return result;
            }

            return null;
        }

        public static void StemAndLeafPlot(int[] stat)
        {
            int stemMax = stat.Max() / 10;
            int stemMin = stat.Min() / 10;
            Array.Sort(stat);

            for (int i = stemMin; i <= stemMax; i++)
            {
                Console.Write("{0,3} | ", i);
                foreach (var t in stat)
                {
                    if (t < 10 * i)
                        continue;
                    if (t >= 10 * (i + 1))
                        break;
                    Console.Write("{0} ", t % 10);
                }
                Console.WriteLine("");
            }
        }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int[] stat = File.ReadLines(@"C:\Users\Степан Пантера\Desktop\input_10.txt").Select(l => Convert.ToInt32(l)).ToArray();

            for (int i = 0; i < stat.Length; i++)
            {
                Console.WriteLine(stat[i] + " ");
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int a in stat)
            {
                if (counts.ContainsKey(a))
                    counts[a] = counts[a] + 1;
                else
                    counts[a] = 1;
            }

            int[] element = counts.Keys.ToArray();
            int[] freq = counts.Values.ToArray();

            double xser = Xser(stat);

            double Q1 = FindQ1(stat);
            double Q2 = FindQ3(stat);
            double P90 = FindP90(stat);

            double MAD = FindMAD(stat, element, freq);
            double deviation = GetDeviation(stat, element, freq);

            double Ydeviation = Task3(deviation, xser);

            StemAndLeafPlot(stat);

            Application.Run(new Lab2());

            Console.Read();
        }

    }

}
