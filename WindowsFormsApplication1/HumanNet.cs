using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class HumanNet : Human
    {
        Socket otherSide;

        public HumanNet(Form1 f, string pName, Socket s)
            : base(f, pName)
        {
            otherSide = s;
        }

        public override Field Play()
        {
            Field fld = base.Play();
            string str = fld.X + "-" + fld.Y;
            if (fld.H)
                str += "-horBox";
            else
                str += "-verBox";

            str += "-<EOF>";
            byte[] msg = Encoding.ASCII.GetBytes(str);
            otherSide.Send(msg);

            return fld;
        }
    }
}
