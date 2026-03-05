namespace Lab_3_1
{
    partial class SearchForm
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
            textBox1 = new TextBox();
            label1 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(100, 95);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(188, 27);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(163, 56);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 1;
            label1.Text = "Поиск";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(50, 142);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(286, 28);
            comboBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(145, 193);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "Найти";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SearchForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 252);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "SearchForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SearchForm";
            Load += SearchForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
    }
}