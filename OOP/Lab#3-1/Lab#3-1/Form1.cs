using Microsoft.VisualBasic.Devices;

namespace Lab_3_1
{
    public partial class Form1 : Form
    {

        public List<Computer> computers = new List<Computer>();
        public List<Computer> tempik = new List<Computer>();

        string seriesPattern = @"^i[3579]$";
        string modelPattern = @"^[1-9]{1}[0-9]{4}$";
        public string errorImage = @"D:\Labs_4_sem\OOP\Lab#3-1\Lab#3-1\Pictures\beloded.png";

        public delegate void Count();
        public Count count;

        public Form1()
        {
            InitializeComponent();
            count = () =>
            {
                toolStripStatusLabel1.Text = $"Количество: {listBox1.Items.Count}";
            };
        }



        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void AddListBox(List<Computer> list)
        {
            foreach (Computer computer in list)
            {
                listBox1.Items.Add(computer);
            }
        }

        private void аToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sortProc = computers.OrderBy(n => n.Processor.Model).ToList();
            listBox1.Items.Clear();
            AddListBox(sortProc);
            count();
            toolStripStatusLabel2.Text = "Сортировано по модели процессора";
        }

        private void поВидеокартеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sortVide = computers.OrderBy(n => n.VideoCard.Model).ToList();
            listBox1.Items.Clear();
            AddListBox(sortVide);
            count();
            toolStripStatusLabel2.Text = "Сортировано по модели видеокарты";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                AddComputer form = new AddComputer(seriesPattern, modelPattern, computers);
                form.ShowDialog();
                listBox1.Items.Clear();
                foreach (var comp in computers)
                {
                    listBox1.Items.Add(comp);
                }
                toolStripStatusLabel2.Text = "Компьютер добавлен";
            }
            catch
            {
                ErrorImage.ShowError(errorImage);
                toolStripStatusLabel2.Text = "Ошибка при добавлении компьютера!";
            }
            finally
            {
                count();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(this);
            searchForm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var comp in computers)
            {
                listBox1.Items.Add(comp);
            }
            count();
            toolStripButton6.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Serializer.SaveJson(computers, "data.json");
                toolStripStatusLabel2.Text = "Сохранено в json";
            }
            catch
            {
                ErrorImage.ShowError(errorImage);
                toolStripStatusLabel2.Text = "Ошибка!";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Serializer.SaveXml(computers, "data.xml");
                toolStripStatusLabel2.Text = "Сохранено в xml";
            }
            catch
            {
                ErrorImage.ShowError(errorImage);
                toolStripStatusLabel2.Text = "Ошибка!";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //AboutAndErrors form = new AboutAndErrors(@"D:\Labs_4_sem\OOP\Lab#3-1\Lab#3-1\Pictures\about.png");
            //form.ShowDialog();
            MessageBox.Show("Версия: 1.0 Gamma\nХаткевич Константин Владимирович");
            toolStripStatusLabel2.Text = "Получена информация о программе";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            computers.Clear();
            count();
        }
    }
}
