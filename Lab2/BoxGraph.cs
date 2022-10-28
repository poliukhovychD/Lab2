using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab2
{
    public partial class Lab2 : Form
    {
        public Lab2()
        {
            InitializeComponent();
        }
        static double FindQ1(int[] stat)
        {
            double Q1;
            Q1 = Math.Floor((stat.Length + 1) * 0.25f) - 1;
            Q1 = stat[(int)Q1] + (((stat[(int)Q1 + 1] - stat[(int)Q1])) * 0.25f);
            return Q1;
        }
        static double FindQ3(int[] stat)
        {
            double Q3;
            Q3 = Math.Floor((stat.Length + 1) * 0.75f) - 1;
            Q3 = stat[(int)Q3] + (((stat[(int)Q3 + 1] - stat[(int)Q3])) * 0.75f);
            return Q3;
        }
        static float GetMedian(int[] stat)
        {
            Array.Sort(stat);
            float Mediana;
            if (stat.Length % 2 == 0)
            {
                Mediana = (stat.Length / 2 + (stat.Length / 2 + 1)) / 2;
                Mediana = ((stat[(int)Mediana] + stat[(int)Mediana - 1]) / 2f);
                return Mediana;
            }

            else
            {
                Mediana = (stat.Length + 1) / 2;
                return Mediana;
            }

        }
        private void Lab2_Load(object sender, EventArgs e)
        {
            int[] stat = File.ReadLines(@"C:\Users\Степан Пантера\Desktop\input_10.txt").
                Select(l => Convert.ToInt32(l)).ToArray();

            double Q1 = FindQ1(stat);
            double Q3 = FindQ3(stat);

            int min = stat.Min();
            int max = stat.Max();

            float Mediana = GetMedian(stat);

            chart1.Series["Коробковий графік"].Points.AddXY(1, min, max, Q1, Q3, Mediana);
        }
    }
}
