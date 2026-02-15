using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Lab_2
{
    public partial class Form1 : Form
    {

        private delegate void Check(object sender, KeyPressEventArgs e);

        public Form1()
        {
            InitializeComponent();
            InitializeControls();

        }

        private void InitializeControls()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(CpuSeries));
            comboBox2.DataSource = Enum.GetValues(typeof(CountOfCores));
            comboBox3.DataSource = Enum.GetValues(typeof(CpuFrequency));
            comboBox4.DataSource = Enum.GetValues(typeof(MaxFrequency));
            comboBox6.DataSource = Enum.GetValues(typeof(ArchitectureBitness));
            comboBox5.DataSource = Enum.GetValues(typeof(CacheL1));
            comboBox7.DataSource = Enum.GetValues(typeof(CacheL2));
            comboBox8.DataSource = Enum.GetValues(typeof(CacheL3));
            comboBox9.DataSource = Enum.GetValues(typeof(ComputerType));
            comboBox15.DataSource = Enum.GetValues(typeof(RamType));
            comboBox14.DataSource = Enum.GetValues(typeof(DiskType));
            comboBox10.DataSource = Enum.GetValues(typeof(GpuManufacturer));
            comboBox11.DataSource = Enum.GetValues(typeof(GpuSeries));
            comboBox12.DataSource = Enum.GetValues(typeof(GpuFrequency));
            comboBox13.DataSource = Enum.GetValues(typeof(VramAmount));
        }

        Check checkPressKey = (a, e) =>
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
                e.Handled = true;
        };


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                computerBindingSource.Add(CreateComputer());
                toolStripStatusLabel1.Text = "Успешно добавлено!";
            }
            catch
            {
                toolStripStatusLabel1.Text = "Пупсик, проверь ОЗУ, имена моделей и чекбоксы";
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void trackBarDisk_Scroll(object sender, EventArgs e)
        {
            label6.Text = trackBarDisk.Value.ToString();
        }


        private void Validation()
        {

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || numericUpDown1.Value == 0 || (!radioButton1.Checked && !radioButton2.Checked)) throw new InvalidDataException();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel1.Text = Computer.CalculateTotalPrice(CreateComputer()).ToString();
            }
            catch (InvalidDataException ex)
            {
                toolStripStatusLabel1.Text = "Пупсик, проверь ОЗУ, имена моделей и чекбоксы";
            }
        }

        private Computer CreateComputer()
        {
            Validation();
            CpuSeries series = (CpuSeries)comboBox1.SelectedItem;
            CountOfCores count = (CountOfCores)comboBox2.SelectedItem;
            CpuFrequency frequency = (CpuFrequency)comboBox3.SelectedItem;
            MaxFrequency maxFrequency = (MaxFrequency)comboBox4.SelectedItem;
            ArchitectureBitness architectureBitness = (ArchitectureBitness)comboBox6.SelectedItem;
            CacheL1 cacheL1 = (CacheL1)comboBox5.SelectedItem;
            CacheL2 cacheL2 = (CacheL2)comboBox7.SelectedItem;
            CacheL3 cacheL3 = (CacheL3)comboBox8.SelectedItem;
            Processor proc;
            if (radioButton1.Checked)
            {
                proc = new Processor(Manufacturer.Intel, series, textBox1.Text, count, frequency, maxFrequency, architectureBitness, cacheL1, cacheL2, cacheL3);
            }
            else
            {
                proc = new Processor(Manufacturer.AMD, series, textBox1.Text, count, frequency, maxFrequency, architectureBitness, cacheL1, cacheL2, cacheL3);
            }


            GpuManufacturer man = (GpuManufacturer)comboBox10.SelectedItem;
            GpuSeries series1 = (GpuSeries)comboBox11.SelectedItem;
            GpuFrequency gpuFrequency = (GpuFrequency)comboBox12.SelectedItem;
            VramAmount vramAmount = (VramAmount)comboBox13.SelectedItem;
            VideoCard videoCard = new VideoCard(man, series1, textBox2.Text, gpuFrequency, vramAmount);

            ComputerType computerType = (ComputerType)comboBox9.SelectedItem;
            DiskType diskType = (DiskType)comboBox14.SelectedItem;
            RamType ramType = (RamType)comboBox15.SelectedItem;
            DateTime dateTime = dateTimePicker1.Value;
            return new Computer(computerType, proc, videoCard, (int)numericUpDown1.Value, ramType, trackBarDisk.Value, diskType, dateTime);
        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();

                Computer newComputer = CreateComputer();

                List<Computer> currentList = new List<Computer>();
                string filePath = "computers.json";

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        currentList = JsonSerializer.Deserialize<List<Computer>>(existingJson) ?? new List<Computer>();
                    }
                }

                currentList.Add(newComputer);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string newJson = JsonSerializer.Serialize(currentList, options);
                File.WriteAllText(filePath, newJson);
                toolStripStatusLabel1.Text = "Успешно записано в файл computers.json";
            }
            catch
            {
                toolStripStatusLabel1.Text = "Пупсик, проверь ОЗУ, имена моделей и чекбоксы";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = "computers.json";

                if (!File.Exists(filePath))
                {
                    toolStripStatusLabel1.Text = "Файл еще не создан!";
                    return;
                }

                string json = File.ReadAllText(filePath);

                var computers = JsonSerializer.Deserialize<List<Computer>>(json);

                foreach (var comp in computers)
                {
                    computerBindingSource.Add(comp);
                }

                toolStripStatusLabel1.Text = $"Загружено компьютеров: {computers.Count}";
            }
            catch
            {
                toolStripStatusLabel1.Text = "Ошибка открытия файла или файл пуст!";
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkPressKey(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkPressKey(sender, e);
        }
    }
}
