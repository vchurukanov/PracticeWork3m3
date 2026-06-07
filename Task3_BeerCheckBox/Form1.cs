using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task3_BeerCheckBox
{
    public partial class Form1 : Form
    {
        private CheckBox cbLightBeer;
        private CheckBox cbDarkBeer;

        private NumericUpDown nudLightPortions;
        private NumericUpDown nudDarkPortions;

        private TextBox txtLightPrice;
        private TextBox txtDarkPrice;

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
            this.Text = "Практична робота 3 — CheckBox";
            this.Name = "BeerCheckBoxForm";
            this.Size = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Controls.Clear();

            Label lblTitle = new Label
            {
                Text = "Завдання 3. Замовлення різних сортів",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(lblTitle);

            Label lblBeer = new Label
            {
                Text = "Сорт пива",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(40, 80),
                AutoSize = true
            };
            Controls.Add(lblBeer);

            Label lblCount = new Label
            {
                Text = "Кількість",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(230, 80),
                AutoSize = true
            };
            Controls.Add(lblCount);

            Label lblPrice = new Label
            {
                Text = "Ціна однієї порції",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(360, 80),
                AutoSize = true
            };
            Controls.Add(lblPrice);

            cbLightBeer = new CheckBox
            {
                Text = "Світле",
                Location = new Point(40, 120),
                AutoSize = true
            };
            cbLightBeer.CheckedChanged += CheckBoxChanged;
            Controls.Add(cbLightBeer);

            nudLightPortions = new NumericUpDown
            {
                Location = new Point(230, 118),
                Width = 80,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Enabled = false
            };
            Controls.Add(nudLightPortions);

            txtLightPrice = new TextBox
            {
                Location = new Point(360, 118),
                Width = 100,
                Text = LightBeerPrice.ToString("0.00"),
                ReadOnly = true
            };
            Controls.Add(txtLightPrice);

            cbDarkBeer = new CheckBox
            {
                Text = "Темне",
                Location = new Point(40, 165),
                AutoSize = true
            };
            cbDarkBeer.CheckedChanged += CheckBoxChanged;
            Controls.Add(cbDarkBeer);

            nudDarkPortions = new NumericUpDown
            {
                Location = new Point(230, 163),
                Width = 80,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Enabled = false
            };
            Controls.Add(nudDarkPortions);

            txtDarkPrice = new TextBox
            {
                Location = new Point(360, 163),
                Width = 100,
                Text = DarkBeerPrice.ToString("0.00"),
                ReadOnly = true
            };
            Controls.Add(txtDarkPrice);

            btnCalculate = new Button
            {
                Text = "Розрахувати",
                Location = new Point(40, 230),
                Width = 120
            };
            btnCalculate.Click += BtnCalculate_Click;
            Controls.Add(btnCalculate);

            btnClear = new Button
            {
                Text = "Очистити",
                Location = new Point(175, 230),
                Width = 100
            };
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            btnExit = new Button
            {
                Text = "Вихід",
                Location = new Point(290, 230),
                Width = 100
            };
            btnExit.Click += BtnExit_Click;
            Controls.Add(btnExit);

            Label lblResult = new Label
            {
                Text = "Результат:",
                Location = new Point(40, 285),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            Controls.Add(lblResult);

            txtResult = new TextBox
            {
                Location = new Point(40, 315),
                Size = new Size(550, 70),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(txtResult);
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            nudLightPortions.Enabled = cbLightBeer.Checked;
            nudDarkPortions.Enabled = cbDarkBeer.Checked;

            if (!cbLightBeer.Checked)
            {
                nudLightPortions.Value = 0;
            }

            if (!cbDarkBeer.Checked)
            {
                nudDarkPortions.Value = 0;
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (!cbLightBeer.Checked && !cbDarkBeer.Checked)
            {
                MessageBox.Show(
                    "Оберіть хоча б один сорт.",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int lightCount = cbLightBeer.Checked ? (int)nudLightPortions.Value : 0;
            int darkCount = cbDarkBeer.Checked ? (int)nudDarkPortions.Value : 0;

            int totalCount = lightCount + darkCount;

            if (totalCount == 0)
            {
                MessageBox.Show(
                    "Вкажіть кількість порцій.",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            decimal lightTotal = lightCount * LightBeerPrice;
            decimal darkTotal = darkCount * DarkBeerPrice;

            decimal total = lightTotal + darkTotal;
            decimal discount = 0;

            if (totalCount > 20)
            {
                discount = total * DiscountPercent / 100;
            }

            decimal finalTotal = total - discount;

            txtResult.Text =
                $"Світле: {lightCount} порц. × {LightBeerPrice} грн = {lightTotal:0.00} грн" + Environment.NewLine +
                $"Темне: {darkCount} порц. × {DarkBeerPrice} грн = {darkTotal:0.00} грн" + Environment.NewLine +
                $"Усього порцій: {totalCount}; Знижка: {discount:0.00} грн; До сплати: {finalTotal:0.00} грн.";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cbLightBeer.Checked = false;
            cbDarkBeer.Checked = false;

            nudLightPortions.Value = 0;
            nudDarkPortions.Value = 0;

            nudLightPortions.Enabled = false;
            nudDarkPortions.Enabled = false;

            txtResult.Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
