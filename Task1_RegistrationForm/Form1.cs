using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Task1_RegistrationForm
{
    public partial class Form1 : Form
    {
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtEmail;

        private NumericUpDown nudYear;
        private NumericUpDown nudMonth;
        private NumericUpDown nudDay;

        private RadioButton rbMale;
        private RadioButton rbFemale;

        private TextBox txtResult;

        private Button btnOk;
        private Button btnClear;
        private Button btnExit;

        public Form1()
        {
            InitializeComponent();
            BuildInterface();
        }

        private void BuildInterface()
        {
            this.Text = "Практична робота 3 — Форма реєстрації";
            this.Name = "RegistrationForm";
            this.Size = new Size(820, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Controls.Clear();

            Label lblTitle = new Label
            {
                Text = "Завдання 1. Модифікована форма реєстрації",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(lblTitle);

            GroupBox groupUserData = new GroupBox
            {
                Text = "Дані користувача",
                Location = new Point(20, 70),
                Size = new Size(380, 300)
            };
            Controls.Add(groupUserData);

            Label lblFirstName = new Label
            {
                Text = "Ім’я:",
                Location = new Point(20, 35),
                AutoSize = true
            };
            groupUserData.Controls.Add(lblFirstName);

            txtFirstName = new TextBox
            {
                Location = new Point(145, 32),
                Width = 200
            };
            groupUserData.Controls.Add(txtFirstName);

            Label lblLastName = new Label
            {
                Text = "Прізвище:",
                Location = new Point(20, 75),
                AutoSize = true
            };
            groupUserData.Controls.Add(lblLastName);

            txtLastName = new TextBox
            {
                Location = new Point(145, 72),
                Width = 200
            };
            groupUserData.Controls.Add(txtLastName);

            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(20, 115),
                AutoSize = true
            };
            groupUserData.Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Location = new Point(145, 112),
                Width = 200
            };
            groupUserData.Controls.Add(txtEmail);

            GroupBox groupBirthDate = new GroupBox
            {
                Text = "Дата народження",
                Location = new Point(20, 150),
                Size = new Size(335, 80)
            };
            groupUserData.Controls.Add(groupBirthDate);

            Label lblYear = new Label
            {
                Text = "Рік",
                Location = new Point(20, 25),
                AutoSize = true
            };
            groupBirthDate.Controls.Add(lblYear);

            nudYear = new NumericUpDown
            {
                Location = new Point(20, 45),
                Width = 90,
                Minimum = 1900,
                Maximum = DateTime.Now.Year,
                Value = 2000
            };
            groupBirthDate.Controls.Add(nudYear);

            Label lblMonth = new Label
            {
                Text = "Місяць",
                Location = new Point(130, 25),
                AutoSize = true
            };
            groupBirthDate.Controls.Add(lblMonth);

            nudMonth = new NumericUpDown
            {
                Location = new Point(130, 45),
                Width = 70,
                Minimum = 1,
                Maximum = 12,
                Value = 1
            };
            groupBirthDate.Controls.Add(nudMonth);

            Label lblDay = new Label
            {
                Text = "День",
                Location = new Point(225, 25),
                AutoSize = true
            };
            groupBirthDate.Controls.Add(lblDay);

            nudDay = new NumericUpDown
            {
                Location = new Point(225, 45),
                Width = 70,
                Minimum = 1,
                Maximum = 31,
                Value = 1
            };
            groupBirthDate.Controls.Add(nudDay);

            GroupBox groupGender = new GroupBox
            {
                Text = "Стать",
                Location = new Point(20, 235),
                Size = new Size(335, 50)
            };
            groupUserData.Controls.Add(groupGender);

            rbMale = new RadioButton
            {
                Text = "Чоловік",
                Location = new Point(20, 22),
                AutoSize = true,
                Checked = true
            };
            groupGender.Controls.Add(rbMale);

            rbFemale = new RadioButton
            {
                Text = "Жінка",
                Location = new Point(150, 22),
                AutoSize = true
            };
            groupGender.Controls.Add(rbFemale);

            GroupBox groupResult = new GroupBox
            {
                Text = "Введені дані",
                Location = new Point(420, 70),
                Size = new Size(360, 300)
            };
            Controls.Add(groupResult);

            txtResult = new TextBox
            {
                Location = new Point(15, 30),
                Size = new Size(330, 250),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            groupResult.Controls.Add(txtResult);

            btnOk = new Button
            {
                Text = "OK",
                Location = new Point(20, 395),
                Width = 100
            };
            btnOk.Click += BtnOk_Click;
            Controls.Add(btnOk);

            btnClear = new Button
            {
                Text = "Очистити",
                Location = new Point(140, 395),
                Width = 100
            };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            btnExit = new Button
            {
                Text = "Вихід",
                Location = new Point(260, 395),
                Width = 100
            };
            btnExit.Click += BtnExit_Click;
            Controls.Add(btnExit);

            this.AcceptButton = btnOk;
            this.CancelButton = btnExit;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();

            int year = (int)nudYear.Value;
            int month = (int)nudMonth.Value;
            int day = (int)nudDay.Value;

            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Введіть ім’я.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Введіть прізвище.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Введіть коректний email.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidDate(year, month, day))
            {
                MessageBox.Show("Введено некоректну дату народження.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime birthDate = new DateTime(year, month, day);
            int age = DateTime.Now.Year - birthDate.Year;

            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            string gender = rbMale.Checked ? "Чоловік" : "Жінка";

            txtResult.Text =
                "Дані користувача:" + Environment.NewLine +
                $"Ім’я: {firstName}" + Environment.NewLine +
                $"Прізвище: {lastName}" + Environment.NewLine +
                $"Email: {email}" + Environment.NewLine +
                $"Дата народження: {birthDate:dd.MM.yyyy}" + Environment.NewLine +
                $"Вік: {age}" + Environment.NewLine +
                $"Стать: {gender}";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();

            nudYear.Value = 2000;
            nudMonth.Value = 1;
            nudDay.Value = 1;

            rbMale.Checked = true;

            txtResult.Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsValidDate(int year, int month, int day)
        {
            try
            {
                DateTime date = new DateTime(year, month, day);
                return date <= DateTime.Now;
            }
            catch
            {
                return false;
            }
        }
    }
}
