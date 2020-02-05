using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Field
    {
        public int X;
        public int Y;
        public bool H;
        public Field()
        {
            X = -1;
            Y = -1;
            H = false;
        }
        public Field(int x, int y, bool h)
        {
            X = x;
            Y = y;
            H = h;
        }
    }
}
