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
using System.Threading;
using System.IO;
using System.IO.Pipes;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Animator.Load();
        }

        public int x = 12, y = 12;
        public PictureBox[,] smallBox = new PictureBox[5, 5];
        public PictureBox[,] horBox = new PictureBox[5, 4];
        public PictureBox[,] verBox = new PictureBox[4, 5];
        public PictureBox[,] bigBox = new PictureBox[4, 4];
        public static int H = 4, W = 4;
        public bool locked = false;
        public bool isServer = false;
        Socket otherSide;
        public State curNetPlayer;
        public string pipeName = "GamePipeName";
        public Board gameBoard;
        public Player player1, player2;
        public Game game;
        public Thread mainLoopThread;
       
        public void closeSocket()
        {
            try
            {
                otherSide.Send(Encoding.ASCII.GetBytes("END-<EOF>"));
                otherSide.Shutdown(SocketShutdown.Both);
                otherSide.Close();
            }
            catch (Exception) { }
        }

        public void onPicBoxMouseUp(string s)
        {
            string[] buf = s.Split('-');
            bool f = buf[2] == "horBox";
            int i = Convert.ToInt32(buf[0]);
            int j = Convert.ToInt32(buf[1]);
            if (f)
            {
                horBox[i, j].Image = null;
                horBox[i, j].MouseUp -= new MouseEventHandler(box_MouseUp);
                horBox[i, j].MouseDown -= new MouseEventHandler(box_MouseDown);
                horBox[i, j].MouseEnter -= new EventHandler(box_MouseEnter);
                horBox[i, j].MouseLeave -= new EventHandler(box_MouseLeave);
                Animator an = new Animator(horBox[i, j], true);
                an.Animate();
            }
            else
            {
                verBox[i, j].Image = null;
                verBox[i, j].MouseUp -= new MouseEventHandler(box_MouseUp);
                verBox[i, j].MouseDown -= new MouseEventHandler(box_MouseDown);
                verBox[i, j].MouseEnter -= new EventHandler(box_MouseEnter);
                verBox[i, j].MouseLeave -= new EventHandler(box_MouseLeave);
                Animator an = new Animator(verBox[i, j], false);
                an.Animate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor_blue.GetHicon());
            int tabCount = 100; 
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    smallBox[i, j] = new PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(smallBox[i, j])).BeginInit();
                    smallBox[i, j].Location = new Point(x + 60 * i, y + 60 * j);
                    smallBox[i, j].Name = "smallBox" + i + "-" + j;
                    smallBox[i, j].Size = new Size(20, 20);
                    smallBox[i, j].TabIndex = tabCount;
                    smallBox[i, j].TabStop = false;
                    smallBox[i, j].Image = Properties.Resources.small_box;
                    tabCount++;
                    this.Controls.Add(smallBox[i, j]);
                    ((System.ComponentModel.ISupportInitialize)(smallBox[i, j])).EndInit();
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    horBox[j, i] = new PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(horBox[j, i])).BeginInit();
                    horBox[j, i].Location = new Point(x + 20 + 60 * i, y + 60 * j);
                    horBox[j, i].Name = j + "-" + i + "-horBox";
                    horBox[j, i].Size = new Size(40, 20);
                    horBox[j, i].TabIndex = tabCount;
                    horBox[j, i].TabStop = false;
                    horBox[j, i].Image = Properties.Resources.hor_box_up;
                    horBox[j, i].BackgroundImage = Properties.Resources.hor_box_green;
                    horBox[j, i].MouseEnter += new System.EventHandler(box_MouseEnter);
                    horBox[j, i].MouseLeave += new System.EventHandler(box_MouseLeave);
                    horBox[j, i].MouseUp += new MouseEventHandler(box_MouseUp);
                    horBox[j, i].MouseDown += new MouseEventHandler(box_MouseDown);
                    tabCount++;
                    this.Controls.Add(horBox[j, i]);
                    ((System.ComponentModel.ISupportInitialize)(horBox[j, i])).EndInit();

                    verBox[i, j] = new PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(verBox[i, j])).BeginInit();
                    verBox[i, j].Location = new Point(x + 60 * j, y + 20 + 60 * i);
                    verBox[i, j].Name = i + "-" + j + "-verBox";
                    verBox[i, j].Size = new Size(20, 40);
                    verBox[i, j].TabIndex = tabCount;
                    verBox[i, j].TabStop = false;
                    verBox[i, j].Image = Properties.Resources.ver_box_up;
                    verBox[i, j].BackgroundImage = Properties.Resources.ver_box_green;
                    verBox[i, j].MouseEnter += new System.EventHandler(box_MouseEnter);
                    verBox[i, j].MouseLeave += new System.EventHandler(box_MouseLeave);
                    verBox[i, j].MouseUp += new MouseEventHandler(box_MouseUp);
                    verBox[i, j].MouseDown += new MouseEventHandler(box_MouseDown);
                    tabCount++;
                    this.Controls.Add(verBox[i, j]);
                    ((System.ComponentModel.ISupportInitialize)(verBox[i, j])).EndInit();
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    
                    bigBox[i, j] = new PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(bigBox[i, j])).BeginInit();
                    bigBox[i, j].Location = new Point(y + 20 + 60 * j, x + 20 + 60 * i);
                    bigBox[i, j].Name = "bigBox" + i + " " + j;
                    bigBox[i, j].Size = new Size(40, 40);
                    bigBox[i, j].TabIndex = tabCount;
                    bigBox[i, j].TabStop = false;
                    bigBox[i, j].Image = Properties.Resources.big_box;
                    tabCount++;
                    this.Controls.Add(bigBox[i, j]);
                    ((System.ComponentModel.ISupportInitialize)(bigBox[i, j])).EndInit();
                }
            }
            InitGame();
        }

        public void InitGame()
        {
            player1 = new Human(this, pipeName);
            player2 = new Human(this, pipeName);
            gameBoard = new Board(H, W);
            game = new Game(player1, player2, gameBoard);
            mainLoopThread = new Thread(mainLoop);
            mainLoopThread.Start();
            mainLoopThread.IsBackground = true;
        }

        public void mainLoop()
        {
            while (!game.NextTurn())
            {
                Redraw();
            }
            Redraw();
        }

        public delegate void RedrawCursorCallback(Cursor crs);

        private void RedrawCursor(Cursor crs)
        {
            this.Cursor = crs;
        }

        private void Redraw()
        {
            lock (this)
            {
                switch (game.gameBoard.CurPlayer)
                {
                    case State.First:
                        currentPlayerPct.Image = Properties.Resources.text_p1;
                        if (!isServer)
                            Invoke(new RedrawCursorCallback(this.RedrawCursor),
                                new object[] { new Cursor(Properties.Resources.cursor_blue.GetHicon()) });
                        break;
                    case State.Second:
                        currentPlayerPct.Image = Properties.Resources.text_p2;
                        if (!isServer)
                            Invoke(new RedrawCursorCallback(this.RedrawCursor),
                                new object[] { new Cursor(Properties.Resources.cursor_red.GetHicon()) });
                        break;
                }

                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        switch (game.gameBoard.GetFieldState(i, j))
                        {
                            case State.Empty:
                                bigBox[i, j].Image = Properties.Resources.big_box;
                                break;
                            case State.First:
                                bigBox[i, j].Image = Properties.Resources.big_box_cross;
                                break;
                            case State.Second:
                                bigBox[i, j].Image = Properties.Resources.big_box_circle;
                                break;
                        }
                    }
                }

                if (game.gameBoard.Winner != State.Empty)
                {
                    switch (game.gameBoard.Winner)
                    {
                        case State.First:
                            currentWinnerPct.Image = Properties.Resources.text_p1;
                            break;
                        case State.Second:
                            currentWinnerPct.Image = Properties.Resources.text_p2;
                            break;
                        case State.Draw:
                            currentWinnerPct.Image = Properties.Resources.text_draw;
                            break;
                    }
                    currentWinnerPct.BeginInvoke(new Action(() => { currentWinnerPct.Visible = true; }));
                    winnerPct.BeginInvoke(new Action(() => { winnerPct.Visible = true; }));
                }
            }
        }


        #region Events

        private void box_MouseUp(object sender, EventArgs e)
        {
            if ((Control)sender == GetChildAtPoint(PointToClient(Cursor.Position)))
            {
                using (NamedPipeClientStream pipeClient =
                new NamedPipeClientStream(pipeName))
                {
                    try
                    {
                        pipeClient.Connect(10);
                        using (StreamWriter sw = new StreamWriter(pipeClient))
                        {
                            sw.AutoFlush = true;
                            sw.WriteLine(((PictureBox)sender).Name + "-<EOM>");
                        }
                    }
                    catch (TimeoutException)
                    {
                        Console.WriteLine("Time Out");
                    }
                }
            }
        }

        private void box_MouseEnter(object sender, EventArgs e)
        {
            string[] s = ((PictureBox)sender).Name.Split('-');
            if (s[2] == "horBox")
                ((PictureBox)sender).Image = Properties.Resources.hor_box_hl;
            else
                ((PictureBox)sender).Image = Properties.Resources.ver_box_hl;
        }
        private void box_MouseLeave(object sender, EventArgs e)
        {
            string[] s = ((PictureBox)sender).Name.Split('-');
            if (s[2] == "horBox")
                ((PictureBox)sender).Image = Properties.Resources.hor_box_up;
            else
                ((PictureBox)sender).Image = Properties.Resources.ver_box_up;
        }
        private void box_MouseDown(object sender, EventArgs e)
        {
            string[] s = ((PictureBox)sender).Name.Split('-');
            if (s[2] == "horBox")
                ((PictureBox)sender).Image = Properties.Resources.hor_box_down;
            else
                ((PictureBox)sender).Image = Properties.Resources.ver_box_down;
        }

        #endregion


        #region Buttons
        private void newGameBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Control)sender == GetChildAtPoint(PointToClient(Cursor.Position)))
            {
                newGameBtn.Image = Properties.Resources.button_new_game;

                game.changePlayer(new Human(this, pipeName), 1);
                game.changePlayer(new Human(this, pipeName), 2);
                game.Reset();
                Human.StopPipe(pipeName);
                if (!mainLoopThread.IsAlive)
                {
                    mainLoopThread = new Thread(mainLoop);
                    mainLoopThread.Start();
                    mainLoopThread.IsBackground = true;
                }
                ResetGame();
            }
        }

        private void newGameBtn_MouseEnter(object sender, EventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game_hl;
        }

        private void newGameBtn_MouseLeave(object sender, EventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game;
        }

        private void newGameBtn_MouseDown(object sender, MouseEventArgs e)
        {
            newGameBtn.Image = Properties.Resources.button_new_game_down;
        }

        private void exitBtn_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Control)sender == GetChildAtPoint(PointToClient(Cursor.Position)))
            {
                exitBtn.Image = Properties.Resources.button_exit;
                closeSocket();
                this.Close();
            }
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit_hl;
        }

        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit;
        }

        private void exitBtn_MouseDown(object sender, MouseEventArgs e)
        {
            exitBtn.Image = Properties.Resources.button_exit_down;
        }

        #endregion

        private void ResetGame()
        {
            this.Cursor = new Cursor(Properties.Resources.cursor_blue.GetHicon());
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 5; j++)
                {
                    horBox[j, i].MouseEnter -= new System.EventHandler(box_MouseEnter);
                    horBox[j, i].MouseLeave -= new System.EventHandler(box_MouseLeave);
                    horBox[j, i].MouseUp -= new MouseEventHandler(box_MouseUp);
                    horBox[j, i].MouseDown -= new MouseEventHandler(box_MouseDown);
                    verBox[i, j].MouseEnter -= new System.EventHandler(box_MouseEnter);
                    verBox[i, j].MouseLeave -= new System.EventHandler(box_MouseLeave);
                    verBox[i, j].MouseUp -= new MouseEventHandler(box_MouseUp);
                    verBox[i, j].MouseDown -= new MouseEventHandler(box_MouseDown);

                    horBox[j, i].Image = Properties.Resources.hor_box_up;
                    horBox[j, i].MouseEnter += new System.EventHandler(box_MouseEnter);
                    horBox[j, i].MouseLeave += new System.EventHandler(box_MouseLeave);
                    horBox[j, i].MouseUp += new MouseEventHandler(box_MouseUp);
                    horBox[j, i].MouseDown += new MouseEventHandler(box_MouseDown);

                    verBox[i, j].Image = Properties.Resources.ver_box_up;
                    verBox[i, j].MouseEnter += new System.EventHandler(box_MouseEnter);
                    verBox[i, j].MouseLeave += new System.EventHandler(box_MouseLeave);
                    verBox[i, j].MouseUp += new MouseEventHandler(box_MouseUp);
                    verBox[i, j].MouseDown += new MouseEventHandler(box_MouseDown);
                }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    bigBox[i, j].Image = Properties.Resources.big_box;
            
            currentWinnerPct.Visible = false;
            winnerPct.Visible = false;
            currentPlayerPct.Image = Properties.Resources.text_p1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            if (f2.ShowDialog() == DialogResult.OK)
            {
                otherSide = f2.ReturnOtherSide;
                game.changePlayer(new HumanNet(this, pipeName, otherSide), 1);
                game.changePlayer(new NetPlayer(this, otherSide), 2);
                game.Reset();
                ResetGame();
                if (mainLoopThread.IsAlive)
                {
                    Human.StopPipe(pipeName);
                    mainLoopThread.Abort();
                }
                mainLoopThread = new Thread(mainLoop);
                mainLoopThread.Start();
                mainLoopThread.IsBackground = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            if (f3.ShowDialog() == DialogResult.OK)
            {
                otherSide = f3.ReturnOtherSide;
                game.changePlayer(new NetPlayer(this, otherSide), 1);
                game.changePlayer(new HumanNet(this, pipeName, otherSide), 2);
                game.Reset();
                ResetGame();
                if (mainLoopThread.IsAlive)
                {
                    Human.StopPipe(pipeName);
                    mainLoopThread.Abort();
                }
                mainLoopThread = new Thread(mainLoop);
                mainLoopThread.Start();
                mainLoopThread.IsBackground = true;
            }
        }

    }
}
