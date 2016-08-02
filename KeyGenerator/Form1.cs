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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = Keygen1.GenerateSerial();
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            GenerateResponse();
        }
        
        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            textBox2.Focus();
            textBox2.SelectAll();
        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            textBox3.Focus();
            textBox3.SelectAll();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GenerateResponse();
        }

        private void GenerateResponse()
        {
            string request = textBox2.Text;
            string edition = comboBox1.Text;
            int userCount = (int)numericUpDown1.Value;
            if (request != null && edition != null && userCount != 0)
            {
                try
                {
                    textBox3.Text = Keygen1.GenerateResponse(request, edition, userCount);
                }
                catch
                {
                    textBox3.Text = "";
                }
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Keygen1.GenerateSerial();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateResponse();
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            GenerateResponse();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
