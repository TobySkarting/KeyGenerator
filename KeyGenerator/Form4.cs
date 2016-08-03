using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private int[] key = { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23,
                                24, 25, 26, 27, 28, 29, 32, 30, 31, 33};

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int ind = random.Next() % key.Length;
            int n1 = key[ind] / 10;
            int n2 = key[ind] % 10;
            int n3 = random.Next() % 2 + 1;
            int weight = n1 * 1 + n2 * 9 + n3 * 8;
            byte[] serial = new byte[7];
            random.NextBytes(serial);
            for (int i = 0, j = 7; i < serial.Length; i++, j--)
            {
                serial[i] %= 10;
                weight += serial[i] * j;
            }
            int n10 = (10 - weight % 10) % 10;
            StringBuilder sb = new StringBuilder();
            sb.Append((char)('A' + ind));
            sb.Append(n3);
            foreach (byte b in serial)
                sb.Append(b);
            sb.Append(n10);
            textBox1.Text = sb.ToString();
        }
    }
}
