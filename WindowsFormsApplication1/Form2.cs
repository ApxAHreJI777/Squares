using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        IPHostEntry ipHostInfo;
        public Socket ReturnOtherSide;
        Thread thh;

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void StartListening()
        {
            //foreach (IPAddress ipA in ipHostInfo.AddressList)
            //{
            //    listBox1.BeginInvoke(new Action(() => { listBox1.Items.Add(ipA.ToString()); }));
            //}
            int listIndex = 0;
            listBox1.Invoke(new Action(() => { listIndex = listBox1.SelectedIndex; }));
            IPAddress ipAddress = ipHostInfo.AddressList[listIndex];
            //listBox1.BeginInvoke(new Action(() => { listBox1.Items.Add(ipAddress.ToString()); }));
            //return;
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(textBox1.Text);
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            int c = 0;
            while (ipAddress.IsIPv6LinkLocal && c < ipHostInfo.AddressList.Length)
            {
                ipAddress = ipHostInfo.AddressList[c];
                c++;
            }
            
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            label2.BeginInvoke(new Action(() => { label2.Text = ipAddress.ToString(); }));
            label2.BeginInvoke(new Action(() => { label2.Text += " " + ipAddress.IsIPv6LinkLocal; })); 
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                ReturnOtherSide = listener.Accept();
                MessageBox.Show("Connected " + ReturnOtherSide.RemoteEndPoint);

            }
            catch (Exception e)
            {
                MessageBox.Show("Connection lost " + e);
            }
            this.DialogResult = DialogResult.OK;
            this.BeginInvoke(new Action(() => { this.Close(); }));
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++ )
            {
                if (!ipHostInfo.AddressList[i].IsIPv6LinkLocal)
                {
                    listBox1.Items.Add(ipHostInfo.AddressList[i].ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thh = new Thread(StartListening);
            thh.Start();
            thh.IsBackground = true;
        }
    }
}
