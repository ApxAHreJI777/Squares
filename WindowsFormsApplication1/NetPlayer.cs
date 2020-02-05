using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class NetPlayer : Player
    {
        Socket otherSide;
        Form1 f1;

        public NetPlayer(Form1 f, Socket s)
        {
            f1 = f;
            otherSide = s;
        }
        public override Field Play()
        {
            Field fld = new Field();
            string data = null;
            byte[] bytes = new byte[1024];
            try
            {
                data = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = otherSide.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                if (!data.Contains("END"))
                {
                    string[] input = data.Split('-');
                    bool isHor = input[2] == "horBox";
                    fld.X = Convert.ToInt32(input[0]);
                    fld.Y = Convert.ToInt32(input[1]);
                    fld.H = isHor;
                    f1.onPicBoxMouseUp(data);
                }
                else
                {
                    MessageBox.Show("Connection lost");
                    fld.X = -100;
                    fld.Y = -100;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connection lost");
                fld.X = -100;
                fld.Y = -100;
            }
            return fld;
        }
    }
}
