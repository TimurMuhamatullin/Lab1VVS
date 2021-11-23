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
    }
}
