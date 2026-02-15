using static Lab_1.Calculator;

namespace Lab_1
{
    public partial class Form1 : Form
    {

        private delegate void Check(object sender, KeyPressEventArgs e);
        private delegate void ValidationResult(int system);

        private ValidationResult validation;
        private int _currentNumericResult = 0;
        private bool _hasResult = false;

        private event BoxMessage _boxMessage;

        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            _boxMessage += ShowResult;

            validation = (int system) =>
            {
                try
                {
                    _boxMessage.Invoke(Convert.ToString(_currentNumericResult, system));
                }
                catch (InvalidDataException ex)
                {
                    ShowError();
                }
            };

        }

        Check checkPressKey = (a, e) =>
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
                e.Handled = true;
        };


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string num1 = textBox1.Text;
            string num2 = textBox3.Text;
            string mark = textBox2.Text;

            try
            {
                bool checkNotOperation = false;
                if (mark == "~") checkNotOperation = true;

                if (string.IsNullOrEmpty(num1) && !checkNotOperation)
                    throw new InvalidDataException();

                if (string.IsNullOrEmpty(num2) || string.IsNullOrEmpty(mark))
                    throw new InvalidDataException();

                int a = 0;
                int b = 0;
                if (checkNotOperation)
                {
                    if (!int.TryParse(num2, out b)) throw new InvalidDataException();
                }
                else
                {
                    if (!int.TryParse(num1, out a) || !int.TryParse(num2, out b)) throw new InvalidDataException();
                }

                int? result;

                switch (mark)
                {
                    case "&":
                        result = And(a, b);
                        break;

                    case "|":
                        result = Or(a, b);
                        break;

                    case "^":
                        result = Xor(a, b);
                        break;

                    case "~":
                        result = Not(b);
                        break;

                    default:
                        throw new InvalidDataException();
                }

                if (result.HasValue)
                {
                    _currentNumericResult = result.Value;
                    _hasResult = true;
                    _boxMessage.Invoke(_currentNumericResult.ToString());
                }
            }
            catch (InvalidDataException ex)
            {
                ShowError();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkPressKey(sender, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkPressKey(sender, e);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            validation(16);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            _boxMessage.Invoke(string.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            validation(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            validation(8);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            validation(10);
        }

        private void ShowError()
        {

            using var errorForm = new ErrorForm();
            errorForm.ShowDialog();
        }

        private void ShowResult(string message)
        {
            textBox4.Text = $"Результат {message}";
        }

    }
}
