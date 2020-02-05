using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Animator
    {
        PictureBox pic;
        int h = 0;
        public Thread animThread;
        static Bitmap[,] sprHor = new Bitmap[2, 5];

        public static void Load()
        {
            sprHor[0, 0] = Properties.Resources.hor_box_sp01;
            sprHor[0, 1] = Properties.Resources.hor_box_sp02;
            sprHor[0, 2] = Properties.Resources.hor_box_sp03;
            sprHor[0, 3] = Properties.Resources.hor_box_sp04;
            sprHor[0, 4] = Properties.Resources.hor_box_sp05;
            sprHor[1, 0] = Properties.Resources.ver_box_sp01;
            sprHor[1, 1] = Properties.Resources.ver_box_sp02;
            sprHor[1, 2] = Properties.Resources.ver_box_sp03;
            sprHor[1, 3] = Properties.Resources.ver_box_sp04;
            sprHor[1, 4] = Properties.Resources.ver_box_sp05;
        }

        public Animator(PictureBox picture, bool isHor)
        {
            pic = picture;
            h = isHor ? 0 : 1;
        }

        public void Animate()
        {
            pic.BeginInvoke(new Action(() => { pic.Image = sprHor[h, 0]; }));
            animThread = new Thread(AsyncAnim);
            animThread.Start();
            animThread.IsBackground = true;
        }
        public void AsyncAnim()
        {
            Thread.Sleep(100);
            for (int i = 1; i < 5; i++)
            {
                Thread.Sleep(50);
                pic.Invoke(new Action(() => { pic.Image = sprHor[h, i]; }));
            }
            Thread.Sleep(50);
            pic.Invoke(new Action(() => { pic.Image = null; }));
        }
    }
}
