using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;

namespace WindowsFormsApplication1
{
    class Human : Player
    {
        protected string pipeName;
        Form1 f1;

        public Human(Form1 f, string pipeName)
        {
            this.pipeName = pipeName;
            f1 = f;
        }

        public override Field Play()
        {
            bool OK = false;
            Field fld = new Field();

            while (!OK)
            {
                using (NamedPipeServerStream pipe =
                new NamedPipeServerStream(pipeName))
                {
                    pipe.WaitForConnection();
                    try
                    {
                        using (StreamReader sr = new StreamReader(pipe))
                        {
                            string line = sr.ReadLine();
                            if (line.Contains("<END>")) break;
                            if (!line.Contains("<EOM>")) continue;
                            OK = true;
                            string[] input = line.Split('-');
                            fld.X = Convert.ToInt32(input[0]);
                            fld.Y = Convert.ToInt32(input[1]);
                            fld.H = input[2] == "horBox";
                            f1.onPicBoxMouseUp(line);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("ERROR: {0}", e.Message);
                    }
                }
            }
            return fld;
        }

        public static void StopPipe(string pName)
        {
            using (NamedPipeClientStream pipeClient =
                new NamedPipeClientStream(pName))
            {
                try
                {
                    pipeClient.Connect(10);
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("<END>");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Time Out");
                }
            }
        }
    }
}
