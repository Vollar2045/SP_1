using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_1
{
    public partial class Form1 : Form
    {
        private Label lblName;
        private TextBox txtName;
        private Button btnGreet;
        private Label lblResult;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Добро пожаловать!";
            this.Width = 400;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeControls();
            this.Load += Form1_Load;
        }
        private void InitializeControls()
        {
            lblName = new Label();
            lblName.Text = "Введите ваше имя:";
            lblName.Location = new System.Drawing.Point(30, 30);
            lblName.AutoSize = true;

            // 2. Создаем текстовое поле
            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(130, 27);
            txtName.Width = 200;

            // 3. Создаем кнопку "Привет"
            btnGreet = new Button();
            btnGreet.Text = "Привет";
            btnGreet.Location = new System.Drawing.Point(130, 70);
            btnGreet.Width = 100;
            btnGreet.Height = 30;
            btnGreet.Click += Btn_Click;  // Подписываемся на событие Click

            // 4. Создаем метку для вывода результата
            lblResult = new Label();
            lblResult.Location = new System.Drawing.Point(30, 125);
            lblResult.AutoSize = true;
            lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                                                      System.Drawing.FontStyle.Bold);

            // Добавляем все элементы на форму
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(btnGreet);
            this.Controls.Add(lblResult);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Hi!",
                "Добро пожаловать!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            // Получаем имя из текстового поля
            string name = txtName.Text.Trim();

            // Проверяем, не пустое ли имя
            if (string.IsNullOrEmpty(name))
            {
                lblResult.Text = "Пожалуйста, введите имя!";
                lblResult.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Выводим приветствие
            lblResult.Text = $"Привет, {name}!";
            lblResult.ForeColor = System.Drawing.Color.Green;
        }
    }
}
