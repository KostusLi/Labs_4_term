using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3_1
{
    public partial class AboutAndErrors : Form
    {
        public AboutAndErrors(string path)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;

            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                using var temp = Image.FromFile(path);
                pictureBox1.Image = new Bitmap(temp);
            }
            catch
            {
                BackColor = Color.Black;
            }

            pictureBox1.Click += (_, __) => Close();
            this.Click += (_, __) => Close();
        }

    }
}
