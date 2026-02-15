namespace Lab_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button6 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.BackColor = Color.Lime;
            button1.Font = new Font("MS Gothic", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            button1.Location = new Point(613, 365);
            button1.Name = "button1";
            button1.Size = new Size(166, 62);
            button1.TabIndex = 0;
            button1.Text = "Посчитать";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(128, 255, 255);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox1.Location = new Point(38, 31);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(195, 64);
            textBox1.TabIndex = 1;
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Left;
            button2.BackColor = Color.FromArgb(255, 192, 128);
            button2.Location = new Point(38, 215);
            button2.Name = "button2";
            button2.Size = new Size(82, 74);
            button2.TabIndex = 2;
            button2.Text = "2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Left;
            button3.BackColor = Color.FromArgb(255, 192, 128);
            button3.Location = new Point(162, 215);
            button3.Name = "button3";
            button3.Size = new Size(82, 74);
            button3.TabIndex = 3;
            button3.Text = "8";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Left;
            button4.BackColor = Color.FromArgb(255, 192, 128);
            button4.Location = new Point(290, 215);
            button4.Name = "button4";
            button4.Size = new Size(82, 74);
            button4.TabIndex = 4;
            button4.Text = "10";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Left;
            button5.BackColor = Color.FromArgb(255, 192, 128);
            button5.Location = new Point(417, 215);
            button5.Name = "button5";
            button5.Size = new Size(82, 74);
            button5.TabIndex = 5;
            button5.Text = "16";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top;
            textBox2.BackColor = Color.FromArgb(128, 255, 255);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox2.Location = new Point(304, 31);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(195, 64);
            textBox2.TabIndex = 6;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox3.BackColor = Color.FromArgb(128, 255, 255);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Cursor = Cursors.IBeam;
            textBox3.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox3.Location = new Point(584, 31);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(195, 64);
            textBox3.TabIndex = 7;
            textBox3.TextAlign = HorizontalAlignment.Center;
            textBox3.KeyPress += textBox3_KeyPress;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Bottom;
            textBox4.BackColor = Color.Yellow;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBox4.Location = new Point(141, 353);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(286, 64);
            textBox4.TabIndex = 8;
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button6.BackColor = Color.Red;
            button6.Font = new Font("MS Gothic", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            button6.Location = new Point(613, 271);
            button6.Name = "button6";
            button6.Size = new Size(166, 62);
            button6.TabIndex = 9;
            button6.Text = "Очистить";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.MinimumSize = new Size(800, 456);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 456);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 456);
            Controls.Add(button6);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button6;
        private PictureBox pictureBox1;
    }
}
