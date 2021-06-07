using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Start();

        }

        private void Start()
        {
            Connection.user = textBox1.Text;
            Connection.pass = textBox2.Text;
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    con.Close();
                    MainForm form = new MainForm();
                    form.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Неверное имя пользователя или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                textBox1.Select();
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Start();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                textBox2.Select();
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Start();
            }
            if (e.KeyCode == Keys.Right)
            {
                textBox1.Text = "postgres";
                textBox2.Text = "1956";
                Start();
            }

        }
    }
}
