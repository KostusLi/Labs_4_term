using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();

            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                using var temp = Image.FromFile(@"D:\Labs_4_sem\OOP\Lab#1\Pictures\errorImage2.png");
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
