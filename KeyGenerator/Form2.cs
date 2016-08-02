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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0:D2}{1:D2}", random.Next(0, 50), random.Next(50, 99));
            string text = "FI0DI1JFScOJ66556FT6Gtrwe43VuasduNmds4hiu627Fsdyqwe";
            stringBuilder.Append(text.Substring(random.Next(text.Length - 4), 4));
            stringBuilder.Append('x');
            string seq = "37248872362716767678263478627846382648763867863485002183987328737YRYUIEYUIRYWUYhdfjdhsfuhdsfiuhsdubcvbxcvqwerq";
            stringBuilder.Append(seq.Substring(random.Next(seq.Length - 3), 3));
            stringBuilder.AppendFormat("{0:D3}", random.Next(101, 400));
            textBox1.Text = stringBuilder.ToString();
        }
    }
}
