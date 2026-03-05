namespace Lab_3_1
{
    partial class AddComputer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            label5 = new Label();
            numericUpDown2 = new NumericUpDown();
            label6 = new Label();
            textBox3 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            comboBox2 = new ComboBox();
            numericUpDown3 = new NumericUpDown();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            comboBox3 = new ComboBox();
            numericUpDown4 = new NumericUpDown();
            label13 = new Label();
            numericUpDown5 = new NumericUpDown();
            label14 = new Label();
            label15 = new Label();
            dateTimePicker1 = new DateTimePicker();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(172, 40);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 43);
            label1.Name = "label1";
            label1.Size = new Size(118, 20);
            label1.TabIndex = 1;
            label1.Text = "Производитель";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 87);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 2;
            label2.Text = "Серия";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(172, 84);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(151, 27);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 132);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 4;
            label3.Text = "Модель";
            label3.Click += label3_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(172, 129);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(151, 27);
            textBox2.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 177);
            label4.Name = "label4";
            label4.Size = new Size(95, 20);
            label4.TabIndex = 6;
            label4.Text = "Кол-во ядер";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(172, 175);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 7;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 224);
            label5.Name = "label5";
            label5.Size = new Size(63, 20);
            label5.TabIndex = 8;
            label5.Text = "Частота";
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown2.Increment = new decimal(new int[] { 20, 0, 0, 131072 });
            numericUpDown2.Location = new Point(172, 222);
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(150, 27);
            numericUpDown2.TabIndex = 9;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(123, 9);
            label6.Name = "label6";
            label6.Size = new Size(87, 20);
            label6.TabIndex = 10;
            label6.Text = "Процессор";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(579, 84);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(151, 27);
            textBox3.TabIndex = 14;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(428, 87);
            label7.Name = "label7";
            label7.Size = new Size(63, 20);
            label7.TabIndex = 13;
            label7.Text = "Модель";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(428, 43);
            label8.Name = "label8";
            label8.Size = new Size(118, 20);
            label8.TabIndex = 12;
            label8.Text = "Производитель";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(579, 40);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 11;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(580, 135);
            numericUpDown3.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(150, 27);
            numericUpDown3.TabIndex = 16;
            numericUpDown3.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(429, 137);
            label9.Name = "label9";
            label9.Size = new Size(61, 20);
            label9.TabIndex = 15;
            label9.Text = "Память";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(550, 9);
            label10.Name = "label10";
            label10.Size = new Size(90, 20);
            label10.TabIndex = 17;
            label10.Text = "Видеокарта";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(550, 195);
            label11.Name = "label11";
            label11.Size = new Size(90, 20);
            label11.TabIndex = 18;
            label11.Text = "Компьютер";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(428, 227);
            label12.Name = "label12";
            label12.Size = new Size(35, 20);
            label12.TabIndex = 20;
            label12.Text = "Тип";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(579, 224);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(151, 28);
            comboBox3.TabIndex = 19;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(580, 267);
            numericUpDown4.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(150, 27);
            numericUpDown4.TabIndex = 22;
            numericUpDown4.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(429, 269);
            label13.Name = "label13";
            label13.Size = new Size(37, 20);
            label13.TabIndex = 21;
            label13.Text = "ОЗУ";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(579, 310);
            numericUpDown5.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown5.Minimum = new decimal(new int[] { 128, 0, 0, 0 });
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(150, 27);
            numericUpDown5.TabIndex = 24;
            numericUpDown5.Value = new decimal(new int[] { 128, 0, 0, 0 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(428, 312);
            label14.Name = "label14";
            label14.Size = new Size(103, 20);
            label14.TabIndex = 23;
            label14.Text = "Размер диска";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(428, 364);
            label15.Name = "label15";
            label15.Size = new Size(90, 20);
            label15.TabIndex = 25;
            label15.Text = "Дата заказа";
            label15.Click += label15_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(542, 359);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 26;
            // 
            // button1
            // 
            button1.Location = new Point(599, 431);
            button1.Name = "button1";
            button1.Size = new Size(131, 68);
            button1.TabIndex = 27;
            button1.Text = "Добавить компьютер";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(437, 431);
            button2.Name = "button2";
            button2.Size = new Size(94, 68);
            button2.TabIndex = 28;
            button2.Text = "Очистить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // AddComputer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 521);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dateTimePicker1);
            Controls.Add(label15);
            Controls.Add(numericUpDown5);
            Controls.Add(label14);
            Controls.Add(numericUpDown4);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(comboBox3);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(numericUpDown3);
            Controls.Add(label9);
            Controls.Add(textBox3);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(comboBox2);
            Controls.Add(label6);
            Controls.Add(numericUpDown2);
            Controls.Add(label5);
            Controls.Add(numericUpDown1);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Name = "AddComputer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Добавление компьютера";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private NumericUpDown numericUpDown2;
        private Label label6;
        private TextBox textBox3;
        private Label label7;
        private Label label8;
        private ComboBox comboBox2;
        private NumericUpDown numericUpDown3;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private ComboBox comboBox3;
        private NumericUpDown numericUpDown4;
        private Label label13;
        private NumericUpDown numericUpDown5;
        private Label label14;
        private Label label15;
        private DateTimePicker dateTimePicker1;
        private Button button1;
        private Button button2;
    }
}