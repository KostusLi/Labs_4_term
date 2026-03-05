using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3_1
{
    public partial class AddComputer : Form
    {

        Regex model, series;
        List<Computer> computers;

        public AddComputer(string seriesPattern, string modelPattern, List<Computer> computers)
        {
            series = new Regex(seriesPattern);
            model = new Regex(modelPattern);
            InitializeComponent();
            this.computers = computers;
            comboBox1.DataSource = Enum.GetValues(typeof(Manufacturer));
            comboBox2.DataSource = Enum.GetValues(typeof(Manufacturer));
            comboBox3.DataSource = Enum.GetValues(typeof(ComputerType));
        }

        private void Validation()
        {
            if (!series.IsMatch(textBox1.Text) || !model.IsMatch(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)) throw new InvalidDataException();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();
                Manufacturer manufacturer = (Manufacturer)comboBox1.SelectedItem;
                Manufacturer manufacturer1 = (Manufacturer)comboBox2.SelectedItem;
                ComputerType computerType = (ComputerType)comboBox3.SelectedItem;
                Processor processor = new Processor(manufacturer, textBox1.Text, textBox2.Text, (int)numericUpDown1.Value, (double)numericUpDown2.Value);
                VideoCard videoCard = new VideoCard(manufacturer1, textBox3.Text, (int)numericUpDown3.Value);
                Computer computer = new Computer(computerType, processor, videoCard, (int)numericUpDown4.Value, (int)numericUpDown5.Value, dateTimePicker1.Value);
                var context = new ValidationContext(computer);
                var results = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(computer, context, results, true);
                if (!isValid) throw new InvalidDataException();
                computers.Add(computer);
                this.Close();
            }
            catch
            {
                ErrorImage.ShowError(@"D:\Labs_4_sem\OOP\Lab#3-1\Lab#3-1\Pictures\beloded.png");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
    }
}
