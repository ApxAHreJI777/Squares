using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Socket ReturnOtherSide;

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            TryConnect(ip);
        }

        public void TryConnect(string IP)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(IP);
                
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                ReturnOtherSide = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    ReturnOtherSide.Connect(remoteEP);

                    MessageBox.Show("Socket connected to " +
                        ReturnOtherSide.RemoteEndPoint.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Connection lost");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Incorrect IP");
            }
            catch (Exception)
            {
                MessageBox.Show("Connection lost");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

    }
}
