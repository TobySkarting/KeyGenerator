using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            int i = allNetworkInterfaces.Length - 1;
            string id = allNetworkInterfaces[i].Id;
            string hostName = Dns.GetHostName();
            textBox2.Text = id;
            textBox3.Text = hostName;
        }

        private string clientCode1 = "Registered";
        private string clientCode2 = "Winder";
        private string clientCode3 = "Version1.0.0";
        private string serverCode1 = "Winder";
        private string serverCode2 = "MailProduction";

        private void RenewMachineId()
        {
            string id = textBox2.Text;
            string hostName = textBox3.Text;
            byte[] bytes = Encoding.Default.GetBytes(clientCode1 + id + clientCode2 + hostName + clientCode3);
            MD5 md = MD5.Create();
            byte[] inArray = md.ComputeHash(bytes);
            textBox4.Text = Convert.ToBase64String(inArray);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            RenewMachineId();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            RenewMachineId();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            MD5 md = MD5.Create();
            byte[] inArray = Convert.FromBase64String(textBox4.Text);
            byte[] bytes = Encoding.Default.GetBytes(serverCode1 + Convert.ToBase64String(inArray) + serverCode2);
            inArray = md.ComputeHash(bytes);
            string license = Convert.ToBase64String(inArray);
            textBox1.Text = license;
        }
    }
}
