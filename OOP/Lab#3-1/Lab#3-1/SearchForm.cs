using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3_1
{
    public partial class SearchForm : Form
    {
        private Form1 _form;

        public SearchForm(Form1 form)
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Searching));
            _form = form;
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var temp = (Searching)comboBox1.SelectedItem;
                _form.listBox1.Items.Clear();
                if (temp == Searching.По_процессору_Модель)
                {
                    foreach (Computer computer in _form.computers)
                    {
                        if (computer.Processor.Model.Contains(textBox1.Text))
                        {
                            _form.tempik.Add(computer);
                            _form.listBox1.Items.Add(computer);
                        }
                    }
                    _form.toolStripStatusLabel2.Text = "Осуществлен поиск по процу";
                }
                else if (temp == Searching.По_видеокарте_Модель)
                {
                    foreach (Computer computer in _form.computers)
                    {
                        if (computer.VideoCard.Model.Contains(textBox1.Text))
                        {
                            _form.tempik.Add(computer);
                            _form.listBox1.Items.Add(computer);
                        }
                    }
                    _form.toolStripStatusLabel2.Text = "Осуществлен поиск по видюхе";
                }
                else if (temp == Searching.Регулярка_модель_проца)
                {
                    if (IsValidRegex(textBox1.Text))
                    {
                        Regex reg = new Regex(textBox1.Text);
                        foreach (var computer in _form.computers)
                        {
                            if (reg.IsMatch(computer.Processor.Model))
                            {
                                _form.tempik.Add(computer);
                                _form.listBox1.Items.Add(computer);
                            }
                        }
                        _form.toolStripStatusLabel2.Text = "Осуществлен поиск по процу(регулярка)";
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }
                else if (temp == Searching.Регуляка_модель_видюхи)
                {
                    if (IsValidRegex(textBox1.Text))
                    {
                        Regex reg = new Regex(textBox1.Text);
                        foreach (var computer in _form.computers)
                        {
                            if (reg.IsMatch(computer.VideoCard.Model))
                            {
                                _form.tempik.Add(computer);
                                _form.listBox1.Items.Add(computer);
                            }
                        }
                        _form.toolStripStatusLabel2.Text = "Осуществлен поиск по видюхе(регулярка)";
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }

                _form.toolStripButton6.Visible = true;
            }
            catch
            {
                ErrorImage.ShowError(_form.errorImage);
            }
            finally
            {
                _form.count();
            }
        }

        public bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return false;

            try
            {
                new Regex(pattern);
                return true;
            }
            catch(ArgumentException)
            {
                return false;
            }
        }
    }
}
