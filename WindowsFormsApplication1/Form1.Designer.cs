namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.PictureBox();
            this.newGameBtn = new System.Windows.Forms.PictureBox();
            this.winnerPct = new System.Windows.Forms.PictureBox();
            this.currentWinnerPct = new System.Windows.Forms.PictureBox();
            this.currentPlayerPct = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentWinnerPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentPlayerPct)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(328, 239);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Image = global::WindowsFormsApplication1.Properties.Resources.button_exit;
            this.exitBtn.Location = new System.Drawing.Point(328, 158);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(130, 30);
            this.exitBtn.TabIndex = 7;
            this.exitBtn.TabStop = false;
            this.exitBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exitBtn_MouseDown);
            this.exitBtn.MouseEnter += new System.EventHandler(this.exitBtn_MouseEnter);
            this.exitBtn.MouseLeave += new System.EventHandler(this.exitBtn_MouseLeave);
            this.exitBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exitBtn_MouseUp);
            // 
            // newGameBtn
            // 
            this.newGameBtn.Image = global::WindowsFormsApplication1.Properties.Resources.button_new_game;
            this.newGameBtn.Location = new System.Drawing.Point(328, 109);
            this.newGameBtn.Name = "newGameBtn";
            this.newGameBtn.Size = new System.Drawing.Size(130, 30);
            this.newGameBtn.TabIndex = 6;
            this.newGameBtn.TabStop = false;
            this.newGameBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.newGameBtn_MouseDown);
            this.newGameBtn.MouseEnter += new System.EventHandler(this.newGameBtn_MouseEnter);
            this.newGameBtn.MouseLeave += new System.EventHandler(this.newGameBtn_MouseLeave);
            this.newGameBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.newGameBtn_MouseUp);
            // 
            // winnerPct
            // 
            this.winnerPct.Image = global::WindowsFormsApplication1.Properties.Resources.text_winner;
            this.winnerPct.Location = new System.Drawing.Point(308, 59);
            this.winnerPct.Name = "winnerPct";
            this.winnerPct.Size = new System.Drawing.Size(83, 29);
            this.winnerPct.TabIndex = 5;
            this.winnerPct.TabStop = false;
            this.winnerPct.Visible = false;
            // 
            // currentWinnerPct
            // 
            this.currentWinnerPct.Image = global::WindowsFormsApplication1.Properties.Resources.text_draw;
            this.currentWinnerPct.Location = new System.Drawing.Point(394, 59);
            this.currentWinnerPct.Name = "currentWinnerPct";
            this.currentWinnerPct.Size = new System.Drawing.Size(83, 29);
            this.currentWinnerPct.TabIndex = 4;
            this.currentWinnerPct.TabStop = false;
            this.currentWinnerPct.Visible = false;
            // 
            // currentPlayerPct
            // 
            this.currentPlayerPct.Image = global::WindowsFormsApplication1.Properties.Resources.text_p1;
            this.currentPlayerPct.Location = new System.Drawing.Point(308, 12);
            this.currentPlayerPct.Name = "currentPlayerPct";
            this.currentPlayerPct.Size = new System.Drawing.Size(83, 29);
            this.currentPlayerPct.TabIndex = 3;
            this.currentPlayerPct.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 290);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.newGameBtn);
            this.Controls.Add(this.winnerPct);
            this.Controls.Add(this.currentWinnerPct);
            this.Controls.Add(this.currentPlayerPct);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Squares";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winnerPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentWinnerPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentPlayerPct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox currentPlayerPct;
        private System.Windows.Forms.PictureBox currentWinnerPct;
        private System.Windows.Forms.PictureBox winnerPct;
        private System.Windows.Forms.PictureBox newGameBtn;
        private System.Windows.Forms.PictureBox exitBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;


    }
}

