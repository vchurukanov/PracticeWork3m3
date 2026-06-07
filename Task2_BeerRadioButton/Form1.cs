using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task2_BeerRadioButton
{
    public partial class Form1 : Form
    {
        private RadioButton rbLightBeer;
        private RadioButton rbDarkBeer;

        private NumericUpDown nudPortions;

        private TextBox txtPrice;
        private TextBox txtResult;

        private Button btnCalculate;
        private Button btnClear;
        private Button btnExit;

        private const decimal LightBeerPrice = 45;
        private const decimal DarkBeerPrice = 55;
        private const decimal DiscountPercent = 15;

        public Form1()
        {
            InitializeComponent();
            BuildInterface();
        }

        private void BuildInterface()
        {
            this.Text = "Практична робота 3 — RadioButton";
            this.Name = "BeerRadioButtonForm";
            this.Size = new Size(560, 490);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Controls.Clear();

            Label lblTitle = new Label
            {
                Text = "Завдання 2. Розрахунок вартості замовлення",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(lblTitle);

            GroupBox groupBeer = new GroupBox
            {
                Text = "Сорт пива",
                Location = new Point(30, 75),
                Size = new Size(220, 100)
            };
            Controls.Add(groupBeer);

            rbLightBeer = new RadioButton
            {
                Text = "Світле",
                Location = new Point(20, 30),
                AutoSize = true,
                Checked = true
            };
            rbLightBeer.CheckedChanged += BeerTypeChanged;
            groupBeer.Controls.Add(rbLightBeer);

            rbDarkBeer = new RadioButton
            {
                Text = "Темне",
                Location = new Point(20, 60),
                AutoSize = true
            };
            rbDarkBeer.CheckedChanged += BeerTypeChanged;
            groupBeer.Controls.Add(rbDarkBeer);

            Label lblPortions = new Label
            {
                Text = "Кількість порцій:",
                Location = new Point(30, 200),
                AutoSize = true
            };
            Controls.Add(lblPortions);

            nudPortions = new NumericUpDown
            {
                Location = new Point(170, 198),
                Width = 80,
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            Controls.Add(nudPortions);

            Label lblPrice = new Label
            {
                Text = "Ціна однієї порції:",
                Location = new Point(30, 240),
                AutoSize = true
            };
            Controls.Add(lblPrice);

            txtPrice = new TextBox
            {
                Location = new Point(170, 237),
                Width = 100,
                ReadOnly = true
            };
            Controls.Add(txtPrice);

            Label lblCurrency = new Label
            {
                Text = "грн",
                Location = new Point(280, 240),
                AutoSize = true
            };
            Controls.Add(lblCurrency);

            btnCalculate = new Button
            {
                Text = "Розрахувати",
                Location = new Point(30, 285),
                Width = 120
            };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            btnClear = new Button
            {
                Text = "Очистити",
                Location = new Point(165, 285),
                Width = 100
            };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            btnExit = new Button
            {
                Text = "Вихід",
                Location = new Point(280, 285),
                Width = 100
            };
            btnExit.Click += BtnExit_Click;
            Controls.Add(btnExit);

            txtResult = new TextBox
            {
                Location = new Point(30, 330),
                Size = new Size(480, 90),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(txtResult);

            UpdatePriceTextBox();
        }

        private void BeerTypeChanged(object sender, EventArgs e)
        {
            UpdatePriceTextBox();
        }

        private void UpdatePriceTextBox()
        {
            decimal price = rbLightBeer.Checked ? LightBeerPrice : DarkBeerPrice;
            txtPrice.Text = price.ToString("0.00");
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            string beerType = rbLightBeer.Checked ? "Світле" : "Темне";
            decimal price = rbLightBeer.Checked ? LightBeerPrice : DarkBeerPrice;
            int portions = (int)nudPortions.Value;

            decimal total = price * portions;
            decimal discount = 0;

            if (portions > 20)
            {
                discount = total * DiscountPercent / 100;
            }

            decimal finalTotal = total - discount;

            txtResult.Text =
                $"Сорт: {beerType}" + Environment.NewLine +
                $"Кількість порцій: {portions}" + Environment.NewLine +
                $"Сума без знижки: {total:0.00} грн" + Environment.NewLine +
                $"Знижка: {discount:0.00} грн" + Environment.NewLine +
                $"До сплати: {finalTotal:0.00} грн";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            rbLightBeer.Checked = true;
            nudPortions.Value = 1;
            txtResult.Clear();
            UpdatePriceTextBox();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
