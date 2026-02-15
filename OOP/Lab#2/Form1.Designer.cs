namespace Lab_2
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
            ListBox listBox1;
            computerBindingSource = new BindingSource(components);
            groupBox1 = new GroupBox();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            comboBox8 = new ComboBox();
            comboBox7 = new ComboBox();
            comboBox5 = new ComboBox();
            comboBox6 = new ComboBox();
            comboBox4 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label23 = new Label();
            label22 = new Label();
            label21 = new Label();
            label20 = new Label();
            comboBox13 = new ComboBox();
            comboBox12 = new ComboBox();
            comboBox11 = new ComboBox();
            comboBox10 = new ComboBox();
            textBox2 = new TextBox();
            label3 = new Label();
            groupBox3 = new GroupBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            groupBox4 = new GroupBox();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label6 = new Label();
            comboBox15 = new ComboBox();
            comboBox14 = new ComboBox();
            comboBox9 = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            trackBarDisk = new TrackBar();
            numericUpDown1 = new NumericUpDown();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)computerBindingSource).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDisk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.DataSource = computerBindingSource;
            listBox1.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point, 204);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 12;
            listBox1.Location = new Point(6, 44);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(242, 112);
            listBox1.TabIndex = 4;
            // 
            // computerBindingSource
            // 
            computerBindingSource.DataSource = typeof(Computer);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboBox8);
            groupBox1.Controls.Add(comboBox7);
            groupBox1.Controls.Add(comboBox5);
            groupBox1.Controls.Add(comboBox6);
            groupBox1.Controls.Add(comboBox4);
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.ForeColor = Color.Yellow;
            groupBox1.Location = new Point(5, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(429, 266);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Процессор";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = Color.White;
            label14.Location = new Point(205, 199);
            label14.Name = "label14";
            label14.Size = new Size(24, 20);
            label14.TabIndex = 21;
            label14.Text = "L3";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.White;
            label13.Location = new Point(82, 199);
            label13.Name = "label13";
            label13.Size = new Size(24, 20);
            label13.TabIndex = 20;
            label13.Text = "L2";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.White;
            label12.Location = new Point(337, 148);
            label12.Name = "label12";
            label12.Size = new Size(24, 20);
            label12.TabIndex = 19;
            label12.Text = "L1";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.White;
            label11.Location = new Point(174, 147);
            label11.Name = "label11";
            label11.Size = new Size(95, 20);
            label11.TabIndex = 18;
            label11.Text = "Архитектура";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.White;
            label10.Location = new Point(40, 147);
            label10.Name = "label10";
            label10.Size = new Size(103, 20);
            label10.TabIndex = 17;
            label10.Text = "Макс. частота";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.White;
            label9.Location = new Point(314, 95);
            label9.Name = "label9";
            label9.Size = new Size(63, 20);
            label9.TabIndex = 16;
            label9.Text = "Частота";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.White;
            label8.Location = new Point(174, 95);
            label8.Name = "label8";
            label8.Size = new Size(89, 20);
            label8.TabIndex = 15;
            label8.Text = "Число ядер";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.White;
            label7.Location = new Point(64, 95);
            label7.Name = "label7";
            label7.Size = new Size(52, 20);
            label7.TabIndex = 14;
            label7.Text = "Серия";
            // 
            // comboBox8
            // 
            comboBox8.FormattingEnabled = true;
            comboBox8.Location = new Point(167, 222);
            comboBox8.Name = "comboBox8";
            comboBox8.Size = new Size(104, 28);
            comboBox8.TabIndex = 12;
            // 
            // comboBox7
            // 
            comboBox7.FormattingEnabled = true;
            comboBox7.Location = new Point(39, 222);
            comboBox7.Name = "comboBox7";
            comboBox7.Size = new Size(104, 28);
            comboBox7.TabIndex = 11;
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(293, 170);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(104, 28);
            comboBox5.TabIndex = 10;
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(167, 170);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(104, 28);
            comboBox6.TabIndex = 9;
            comboBox6.SelectedIndexChanged += comboBox6_SelectedIndexChanged;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(39, 170);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(104, 28);
            comboBox4.TabIndex = 8;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(293, 117);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(104, 28);
            comboBox3.TabIndex = 7;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(167, 117);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(104, 28);
            comboBox2.TabIndex = 6;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(39, 117);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(104, 28);
            comboBox1.TabIndex = 5;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(269, 75);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(64, 24);
            radioButton2.TabIndex = 4;
            radioButton2.TabStop = true;
            radioButton2.Text = "AMD";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(146, 75);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(59, 24);
            radioButton1.TabIndex = 3;
            radioButton1.TabStop = true;
            radioButton1.Text = "Intel";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(7, 75);
            label2.Name = "label2";
            label2.Size = new Size(118, 20);
            label2.TabIndex = 2;
            label2.Text = "Производитель";
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(88, 34);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(277, 27);
            textBox1.TabIndex = 1;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(7, 37);
            label1.Name = "label1";
            label1.Size = new Size(63, 20);
            label1.TabIndex = 0;
            label1.Text = "Модель";
            label1.Click += label1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label23);
            groupBox2.Controls.Add(label22);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(label20);
            groupBox2.Controls.Add(comboBox13);
            groupBox2.Controls.Add(comboBox12);
            groupBox2.Controls.Add(comboBox11);
            groupBox2.Controls.Add(comboBox10);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label3);
            groupBox2.ForeColor = Color.Yellow;
            groupBox2.Location = new Point(440, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(407, 235);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Видеокарта";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.ForeColor = Color.White;
            label23.Location = new Point(286, 72);
            label23.Name = "label23";
            label23.Size = new Size(61, 20);
            label23.TabIndex = 23;
            label23.Text = "Память";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.ForeColor = Color.White;
            label22.Location = new Point(53, 72);
            label22.Name = "label22";
            label22.Size = new Size(63, 20);
            label22.TabIndex = 22;
            label22.Text = "Частота";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.ForeColor = Color.White;
            label21.Location = new Point(286, 127);
            label21.Name = "label21";
            label21.Size = new Size(52, 20);
            label21.TabIndex = 21;
            label21.Text = "Серия";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.ForeColor = Color.White;
            label20.Location = new Point(31, 127);
            label20.Name = "label20";
            label20.Size = new Size(118, 20);
            label20.TabIndex = 20;
            label20.Text = "Производитель";
            // 
            // comboBox13
            // 
            comboBox13.FormattingEnabled = true;
            comboBox13.Location = new Point(258, 95);
            comboBox13.Name = "comboBox13";
            comboBox13.Size = new Size(104, 28);
            comboBox13.TabIndex = 11;
            // 
            // comboBox12
            // 
            comboBox12.FormattingEnabled = true;
            comboBox12.Location = new Point(31, 95);
            comboBox12.Name = "comboBox12";
            comboBox12.Size = new Size(104, 28);
            comboBox12.TabIndex = 10;
            // 
            // comboBox11
            // 
            comboBox11.FormattingEnabled = true;
            comboBox11.Location = new Point(258, 149);
            comboBox11.Name = "comboBox11";
            comboBox11.Size = new Size(104, 28);
            comboBox11.TabIndex = 9;
            // 
            // comboBox10
            // 
            comboBox10.FormattingEnabled = true;
            comboBox10.Location = new Point(31, 149);
            comboBox10.Name = "comboBox10";
            comboBox10.Size = new Size(104, 28);
            comboBox10.TabIndex = 8;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(85, 34);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(277, 27);
            textBox2.TabIndex = 2;
            textBox2.KeyPress += textBox2_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.White;
            label3.Location = new Point(6, 37);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 1;
            label3.Text = "Модель";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(statusStrip1);
            groupBox3.Controls.Add(listBox1);
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(button1);
            groupBox3.ForeColor = Color.Yellow;
            groupBox3.Location = new Point(440, 253);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(410, 235);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Управление и вывод";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(3, 206);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(404, 26);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "Все норм";
            statusStrip1.ItemClicked += statusStrip1_ItemClicked;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.AutoSize = false;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(391, 20);
            toolStripStatusLabel1.Text = "Все хорошо";
            toolStripStatusLabel1.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // button4
            // 
            button4.BackColor = Color.Yellow;
            button4.ForeColor = Color.Red;
            button4.Location = new Point(268, 168);
            button4.Name = "button4";
            button4.Size = new Size(104, 32);
            button4.TabIndex = 3;
            button4.Text = "Расчитать";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Yellow;
            button3.ForeColor = Color.Red;
            button3.Location = new Point(268, 126);
            button3.Name = "button3";
            button3.Size = new Size(104, 32);
            button3.TabIndex = 2;
            button3.Text = "Загрузить";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Yellow;
            button2.ForeColor = Color.Red;
            button2.Location = new Point(268, 78);
            button2.Name = "button2";
            button2.Size = new Size(104, 32);
            button2.TabIndex = 1;
            button2.Text = "Сохранить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Yellow;
            button1.ForeColor = Color.Red;
            button1.Location = new Point(268, 27);
            button1.Name = "button1";
            button1.Size = new Size(104, 32);
            button1.TabIndex = 0;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label19);
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(comboBox15);
            groupBox4.Controls.Add(comboBox14);
            groupBox4.Controls.Add(comboBox9);
            groupBox4.Controls.Add(dateTimePicker1);
            groupBox4.Controls.Add(trackBarDisk);
            groupBox4.Controls.Add(numericUpDown1);
            groupBox4.ForeColor = Color.Yellow;
            groupBox4.Location = new Point(5, 284);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(429, 204);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Компьютер";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.ForeColor = Color.White;
            label19.Location = new Point(299, 137);
            label19.Name = "label19";
            label19.Size = new Size(67, 20);
            label19.TabIndex = 26;
            label19.Text = "Тип ОЗУ";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = Color.White;
            label18.Location = new Point(299, 76);
            label18.Name = "label18";
            label18.Size = new Size(78, 20);
            label18.TabIndex = 25;
            label18.Text = "Тип диска";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.ForeColor = Color.White;
            label17.Location = new Point(284, 13);
            label17.Name = "label17";
            label17.Size = new Size(126, 20);
            label17.TabIndex = 24;
            label17.Text = "Тип компьютера";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = Color.White;
            label16.Location = new Point(146, 29);
            label16.Name = "label16";
            label16.Size = new Size(103, 20);
            label16.TabIndex = 23;
            label16.Text = "Размер диска";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = Color.White;
            label15.Location = new Point(49, 29);
            label15.Name = "label15";
            label15.Size = new Size(37, 20);
            label15.TabIndex = 22;
            label15.Text = "ОЗУ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.White;
            label6.Location = new Point(174, 88);
            label6.Name = "label6";
            label6.Size = new Size(33, 20);
            label6.TabIndex = 14;
            label6.Text = "256";
            // 
            // comboBox15
            // 
            comboBox15.FormattingEnabled = true;
            comboBox15.Location = new Point(293, 160);
            comboBox15.Name = "comboBox15";
            comboBox15.Size = new Size(104, 28);
            comboBox15.TabIndex = 9;
            // 
            // comboBox14
            // 
            comboBox14.FormattingEnabled = true;
            comboBox14.Location = new Point(293, 99);
            comboBox14.Name = "comboBox14";
            comboBox14.Size = new Size(104, 28);
            comboBox14.TabIndex = 8;
            // 
            // comboBox9
            // 
            comboBox9.FormattingEnabled = true;
            comboBox9.Location = new Point(293, 36);
            comboBox9.Name = "comboBox9";
            comboBox9.Size = new Size(104, 28);
            comboBox9.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(25, 145);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(213, 27);
            dateTimePicker1.TabIndex = 2;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // trackBarDisk
            // 
            trackBarDisk.Location = new Point(132, 52);
            trackBarDisk.Maximum = 10000;
            trackBarDisk.Minimum = 256;
            trackBarDisk.Name = "trackBarDisk";
            trackBarDisk.Size = new Size(130, 56);
            trackBarDisk.TabIndex = 1;
            trackBarDisk.Value = 256;
            trackBarDisk.Scroll += trackBarDisk_Scroll;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(25, 52);
            numericUpDown1.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(81, 27);
            numericUpDown1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(862, 500);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)computerBindingSource).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDisk).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ComboBox comboBox4;
        private ComboBox comboBox3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private ComboBox comboBox8;
        private ComboBox comboBox7;
        private ComboBox comboBox5;
        private ComboBox comboBox6;
        private NumericUpDown numericUpDown1;
        private TrackBar trackBarDisk;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox9;
        private TextBox textBox2;
        private Label label3;
        private ComboBox comboBox13;
        private ComboBox comboBox12;
        private ComboBox comboBox11;
        private ComboBox comboBox10;
        private Button button1;
        private Button button4;
        private Button button3;
        private Button button2;
        private ComboBox comboBox15;
        private ComboBox comboBox14;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label label8;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label23;
        private Label label22;
        private Label label21;
        private Label label20;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private BindingSource computerBindingSource;
    }
}
