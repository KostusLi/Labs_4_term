namespace Lab_3_1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            аToolStripMenuItem = new ToolStripMenuItem();
            поВидеокартеToolStripMenuItem = new ToolStripMenuItem();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripButton6 = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            listBox1 = new ListBox();
            computerBindingSource = new BindingSource(components);
            button1 = new Button();
            toolStripButton7 = new ToolStripButton();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)computerBindingSource).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripButton1, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5, toolStripButton6, toolStripButton7 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(919, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { аToolStripMenuItem, поВидеокартеToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(113, 24);
            toolStripDropDownButton1.Text = "Сортировать";
            // 
            // аToolStripMenuItem
            // 
            аToolStripMenuItem.Name = "аToolStripMenuItem";
            аToolStripMenuItem.Size = new Size(199, 26);
            аToolStripMenuItem.Text = "По процессору";
            аToolStripMenuItem.Click += аToolStripMenuItem_Click;
            // 
            // поВидеокартеToolStripMenuItem
            // 
            поВидеокартеToolStripMenuItem.Name = "поВидеокартеToolStripMenuItem";
            поВидеокартеToolStripMenuItem.Size = new Size(199, 26);
            поВидеокартеToolStripMenuItem.Text = "По видеокарте";
            поВидеокартеToolStripMenuItem.Click += поВидеокартеToolStripMenuItem_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(130, 24);
            toolStripButton1.Text = "Сохранить в json";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(127, 24);
            toolStripButton2.Text = "Сохранить в xml";
            toolStripButton2.Click += toolStripButton2_Click;
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(108, 24);
            toolStripButton3.Text = "О программе";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(163, 24);
            toolStripButton4.Text = "Добавить компьютер";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(56, 24);
            toolStripButton5.Text = "Поиск";
            toolStripButton5.Click += toolStripButton5_Click;
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(55, 24);
            toolStripButton6.Text = "Назад";
            toolStripButton6.Visible = false;
            toolStripButton6.Click += toolStripButton6_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
            statusStrip1.Location = new Point(0, 460);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(919, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.AutoSize = false;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(230, 20);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "Количество: 0";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.AutoSize = false;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(630, 20);
            toolStripStatusLabel2.Spring = true;
            toolStripStatusLabel2.Text = "Все хорошо";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(0, 29);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(919, 424);
            listBox1.TabIndex = 2;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // computerBindingSource
            // 
            computerBindingSource.DataSource = typeof(Computer);
            // 
            // button1
            // 
            button1.Location = new Point(822, 29);
            button1.Name = "button1";
            button1.Size = new Size(97, 29);
            button1.TabIndex = 3;
            button1.Text = "Скрыть";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(77, 24);
            toolStripButton7.Text = "Очистить";
            toolStripButton7.Click += toolStripButton7_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 486);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)computerBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem аToolStripMenuItem;
        private ToolStripMenuItem поВидеокартеToolStripMenuItem;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        public ToolStripStatusLabel toolStripStatusLabel2;
        public ListBox listBox1;
        private BindingSource computerBindingSource;
        private ToolStripButton toolStripButton5;
        public ToolStripButton toolStripButton6;
        public StatusStrip statusStrip1;
        public ToolStripStatusLabel toolStripStatusLabel1;
        private Button button1;
        private ToolStripButton toolStripButton7;
    }
}
