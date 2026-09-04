using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SP_1
{
    public partial class Form1 : Form
    {
        private Label lblName;
        private TextBox txtName;
        private Button btnGreet;
        private Label lblResult;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem clear;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem about;
        private string filePath = "user.txt";
        public Form1()
        {
            InitializeComponent();
            this.Text = "Добро пожаловать!";
            this.Width = 400;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeControls();
            InitializeMenu();
            this.Load += Form1_Load;
        }
        private void InitializeControls()
        {
            lblName = new Label();
            lblName.Text = "Введите ваше имя:";
            lblName.Location = new System.Drawing.Point(30, 30);
            lblName.AutoSize = true;
            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(140, 27);
            txtName.Width = 200;
            btnGreet = new Button();
            btnGreet.Text = "Привет";
            btnGreet.Location = new System.Drawing.Point(130, 70);
            btnGreet.Width = 100;
            btnGreet.Height = 30;
            btnGreet.Click += Btn_Click;
            lblResult = new Label();
            lblResult.Location = new System.Drawing.Point(30, 125);
            lblResult.AutoSize = true;
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(btnGreet);
            this.Controls.Add(lblResult);
        }
        private void InitializeMenu()
        {
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem("Файл");
            clear = new ToolStripMenuItem("Очистить");
            clear.Click += Clear_Click;
            fileMenu.DropDownItems.Add(clear);
            helpMenu = new ToolStripMenuItem("Справка");
            about = new ToolStripMenuItem("О программе");
            about.Click += About_Click;
            helpMenu.DropDownItems.Add(about);
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(helpMenu);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckFirstLaunch();
            LoadNameFromFile();
            MessageBox.Show(
                "Hi!",
                "Добро пожаловать!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void CheckFirstLaunch()
        {
            try
            {
                string registryPath = @"Software\MyApp";
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
                {
                    if (key == null)
                    {
                        using (RegistryKey newKey = Registry.CurrentUser.CreateSubKey(registryPath))
                        {
                            newKey.SetValue("FirstLaunch", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            newKey.SetValue("LaunchCount", 1, RegistryValueKind.DWord);
                            MessageBox.Show(
                                "Это первый запуск программы!\n" +
                                "Создана запись в реестре.",
                                "Первый запуск",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                    else
                    {
                        object firstLaunch = key.GetValue("FirstLaunch");
                        object launchCount = key.GetValue("LaunchCount");
                        if (firstLaunch != null)
                        {
                            int count = 1;
                            if (launchCount != null)
                            {
                                count = Convert.ToInt32(launchCount) + 1;
                            }
                            key.SetValue("LaunchCount", count, RegistryValueKind.DWord);
                        }
                    }
                }
            }
            catch {}
        }
        private void LoadNameFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string name = File.ReadAllText(filePath).Trim();
                    if (!string.IsNullOrEmpty(name))
                    {
                        txtName.Text = name;
                        lblResult.Text = "Имя загружено из файла";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки имени: {ex.Message}", "Ошибка");
            }
        }

        private void SaveNameToFile()
        {
            try
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    if (File.Exists(filePath)) File.Delete(filePath);
                    return;
                }
                File.WriteAllText(filePath, name);
            }
            catch {}
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                lblResult.Text = "Введите имя!";
                return;
            }
            lblResult.Text = $"Привет, {name}!";
            SaveNameToFile();
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            lblResult.Text = "";
            txtName.Focus();
            try { if (File.Exists(filePath)) File.Delete(filePath); }
            catch {}
        }
        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Персональный приветственник\n" +
                "made by Алескин Влад\n",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveNameToFile();
            base.OnFormClosing(e);
        }
    }
}
