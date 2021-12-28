using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        double[] epsArray;
        const int k = 4;
        public Form1()
        {
            InitializeComponent();
        }

        private double f(ref double[] epsAr, int n) {
            if (n != 0) {
                double local = Math.Pow(10, -2 * k) * (Convert.ToInt64(Math.Pow(10, 3 * k)* Math.Pow(f(ref epsAr, n - 1), 2)));
                epsAr[n] = Math.Round((local - (int) local),2*k);
            }
            return epsAr[n];
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            int n = Convert.ToInt32(textBox1.Text);
            epsArray = new double[n];
            epsArray[0] = Convert.ToDouble(textBox2.Text);
            f(ref epsArray, n-1);
            for (int i = 0; i < n; i++) {
                textBox3.Text += epsArray[i].ToString() + "\r" + "\n";          
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sum1 = 0, sum2 = 0, sum3 = 0, z, ro_kol, ro_max, p;
            double n = Convert.ToDouble(textBox1.Text);
            z = 1.64;
            for (int i = 0; i < n; i++)
            {
                sum1 = sum1 + i * epsArray[i];
                sum2 = sum2 + (epsArray[i] * ((n + 1) / 2));
                sum3 = sum3 + epsArray[i];
            }
            p = (Math.Pow(n, 2) - 1) / 12;
            ro_kol = (((1 / n) * sum1) - ((1 / n) * sum2)) / Math.Sqrt((((1 / n) * sum3) - Math.Pow(((1 / n) * sum3), 2)) * p);
            ro_max = z * ((1 - Math.Pow(ro_kol, 2)) / Math.Sqrt(n));
            if (ro_kol > ro_max)
            {
                textBox4.Text = "Коэффициент корреляции: " + Convert.ToString(ro_kol) + "\nВерхняя граница: " + Convert.ToString(ro_max) + "\nСуществует корреляционная зависимость";
            }
            else
            {
                textBox4.Text = "Коэффициент корреляции: " + Convert.ToString(ro_kol) + "\nВерхняя граница: " + Convert.ToString(ro_max) + "\nКорреляционная зависимость не прослеживается";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double Xn, Xv;
            double[] r1 = new double[] { 0.006, 0.040, 0.185, 0.43, 0.75, 1.13, 1.56, 2.03, 2.53, 3.06, 3.6, 4.2, 4.8, 5.4, 6.0, 6.6, 7.3, 7.9, 8.6, 9.2 };
            double[] r2 = new double[] { 2.7, 4.6, 6.3, 7.8, 9.2, 10.6, 12.0, 13.4, 14.7, 16.0, 17.3, 18.5 };
            int N = Convert.ToInt32(textBox1.Text);
            //string[] y = (from object item in textBox3.Text select item.ToString()).ToArray<string>();
            string[] y = new string[N];
            y = textBox3.Text.Split('\n');
            double m = Convert.ToDouble(textBox6.Text);
            double[] s = new double[Convert.ToInt32(m)];
            double xj, xj1, l;
            for (int i = 0; i < m; i++)
            {
                s[i] = 0;
            }
            for (int i = 0; i < N - 1; i++)
            {
                xj = 0;
                for (int j = 0; j < m; j++)
                {
                    if ((Convert.ToDouble(y[i]) >= xj) && (Convert.ToDouble(y[i]) <= xj + 1.0 / m))
                    {
                        s[j] = s[j] + 1;
                    }
                    xj = xj + 1.0 / m;
                }
            }
            double Y = 0;
            xj = 1.0 / m;
            xj1 = 0;
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine(xj);
                l = (xj - xj1) * N;
                Y = Y + (Math.Pow(s[i] - l, 2) / l);
                xj1 = xj;
                xj = xj + 1.0 / m;
                l = 0;
            }
            Xn = r1[Convert.ToInt32(m) - 1 - 1];
            Xv = r2[Convert.ToInt32(m) - 1 - 1];
            if (Y >= Xn & Y <= Xv)
            {
                textBox5.Text = "X Пирсона =" + Convert.ToString(Y) + (char)13 + (char)10 + "Нижняя граница доверительного интервала " + Convert.ToString(Xn) + (char)13 + (char)10 + "Верхняя граница доверительного интервала" + Convert.ToString(Xv) + (char)13 + (char)10 + "Гипотеза о равномерном законе распределения СВ не отвергается";
            }
            else
            {
                textBox5.Text = "X Пирсона =" + Convert.ToString(Y) + (char)13 + (char)10 + "Нижняя граница доверительного интервала " + Convert.ToString(Xn) + (char)13 + (char)10 + "Верхняя граница доверительного интервала" + Convert.ToString(Xv) + (char)13 + (char)10 + "Гипотеза о равномерном законе распределения СВ отвергается";
            } 
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
