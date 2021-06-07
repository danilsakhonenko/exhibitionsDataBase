using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;


namespace BD
{
    public partial class AddForm : Form
    {
        public string Table{get;set;}
        DataGridViewRow Row { get; set; }
        private Tables currenttable { get; set; }
        TextBox textBox2 { get; set; }
        //private int key;
        public AddForm(string table)
        {
            this.Table = table;
            InitializeComponent();
            this.Text = "Добавление";
            Heading_label.Text = "Добавление записи";
            Add_button.Text = "Добавить";
        }
        public AddForm(string table, DataGridViewRow Row)
        {
            this.Row = Row;
            this.Table = table;
            InitializeComponent();
            this.Text = "Изменение";
            Heading_label.Text = "Изменение записи";
            Add_button.Text = "Сохранить";

        }

        private void SetControls()
        {
            if (Table == "Участие в выставках")
            {
                Controls.Cast<Control>().ToList().ForEach(p => { if (p.GetType() != typeof(Button) && p != Heading_label) p.Visible = false; });
                label2.Visible = true;
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                label2.Text = "Экспонат:";
                label2.Location = new Point(label2.Location.X - 25, label2.Location.Y);
                label6.Visible = true;
                label6.Text = "Выставка:";
                label6.Location = new Point(label6.Location.X + 40, label6.Location.Y);
                currenttable = new Participation(comboBox1, comboBox2);
                currenttable.FillComboBox();
            }
            else if (Table == "Экспонаты")
            {
                label2.Text = "Тип экспоната:";
                label3.Text = "Автор:";
                label4.Text = "Век создания:";
                label5.Text = "Год передачи музею:";
                label6.Text = "Страховая цена:";
                label7.Text = "Музей:";
                label9.Text = "Год последней реставрации:";
                label10.Text = "Описание экспоната:";
                label2.Location = new Point(label2.Location.X - 75, label2.Location.Y);
                label3.Location = new Point(label3.Location.X + 55, label3.Location.Y);
                label4.Location = new Point(label4.Location.X + 25, label4.Location.Y);
                label6.Location = new Point(label6.Location.X - 20, label6.Location.Y);
                label7.Location = new Point(label7.Location.X + 55, label7.Location.Y);
                label9.Location = new Point(label9.Location.X - 50, label9.Location.Y);
                textBox2 = new TextBox();
                textBox2.Font = textBox1.Font;
                textBox2.Multiline = true;
                textBox2.Size = new Size(180, 100);
                textBox2.Location = num3.Location;
                this.Controls.Add(textBox2);
                label8.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                num5.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                comboBox4.Visible = false;
                comboBox2.Location = dateTimePicker1.Location;
                num3.Location = dateTimePicker2.Location;
                num4.Location = new Point(num1.Location.X, label6.Location.Y);
                num4.Maximum = int.MaxValue;
                ArrayList list = new ArrayList() { textBox1, comboBox1, comboBox2, num3, num1, num4, textBox2, num2, comboBox3 };
                currenttable = new Exhibits(list);
                currenttable.FillComboBox();
            }
            else if (Table == "Музеи")
            {
                new List<Label>() { label4, label7, label8, label9, label10, label11, label12 }.ForEach((l) => l.Visible = false);
                new List<Control>() { dateTimePicker1, dateTimePicker2, comboBox3, comboBox4, num2, num3, num4, num5 }.ForEach((l) => l.Visible = false);
                label5.Text = "Год создания:";
                label3.Text = "Телефон:";
                label6.Text = "Частный музей :";
                label3.Location = new Point(label3.Location.X + 35, label3.Location.Y);
                label6.Location = new Point(label6.Location.X - 20, label6.Location.Y);
                label5.Location = new Point(label5.Location.X + 55, label5.Location.Y);
                textBox2 = new TextBox();
                textBox2.Font = textBox1.Font;
                textBox2.Size = textBox1.Size;
                textBox2.Location = dateTimePicker1.Location;
                this.Controls.Add(textBox2);
                currenttable = new Museums(textBox1, comboBox1, num1, textBox2, comboBox2);
                currenttable.FillComboBox();
            }
            else
            {
                ArrayList list = new ArrayList() { textBox1, comboBox1, dateTimePicker1,
                    dateTimePicker2, num1, comboBox2, comboBox3,
                    comboBox4, num2, num3, num4, num5 };
                currenttable = new Exhibitions(list);
                currenttable.FillComboBox();
            }

        }

        private void FillControls()
        {
            if (Table == "Участие в выставках")
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact(Row.Cells[1].Value.ToString());
                comboBox2.SelectedIndex = comboBox2.FindStringExact(Row.Cells[2].Value.ToString());
            }
            else if (Table == "Экспонаты")
            {
                textBox1.Text = Row.Cells[1].Value.ToString();
                textBox2.Text= Row.Cells[7].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(Row.Cells[2].Value.ToString());
                comboBox2.SelectedIndex = comboBox2.FindStringExact(Row.Cells[3].Value.ToString());
                comboBox3.SelectedIndex = comboBox3.FindStringExact(Row.Cells[9].Value.ToString());
                num1.Value= (int)Row.Cells[5].Value;
                num2.Value = (int)Row.Cells[8].Value;
                num3.Value= (int)Row.Cells[4].Value;
                num4.Value = (int)Row.Cells[6].Value;
            }
            else if (Table == "Музеи")
            {
                textBox1.Text = Row.Cells[1].Value.ToString();
                textBox2.Text= Row.Cells[4].Value.ToString();
                comboBox1.SelectedIndex= comboBox1.FindStringExact(Row.Cells[2].Value.ToString());
                comboBox2.SelectedIndex = Convert.ToInt32(Row.Cells[5].Value);
                num1.Value = (int)Row.Cells[3].Value;
            }
            else
            {
                textBox1.Text = Row.Cells[1].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(Row.Cells[2].Value.ToString());
                comboBox2.SelectedIndex = comboBox2.FindStringExact(Row.Cells[6].Value.ToString());
                comboBox3.SelectedIndex = comboBox3.FindStringExact(Row.Cells[7].Value.ToString());
                comboBox4.SelectedIndex = comboBox4.FindStringExact(Row.Cells[8].Value.ToString());
                num1.Value= (int)Row.Cells[5].Value;
                num2.Value= (int)Row.Cells[9].Value;
                num3.Value = (int)Row.Cells[10].Value;
                num4.Value = (int)Row.Cells[11].Value;
                num5.Value = (int)Row.Cells[12].Value;
                dateTimePicker1.Value= (DateTime)Row.Cells[3].Value;
                dateTimePicker2.Value = (DateTime)Row.Cells[4].Value;
            }

        }

        private void Return_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetControls();
            if(Row!=null)
                FillControls();
        }

        protected void Add_button_Click(object sender, EventArgs e)
        {
            if (Add_button.Text == "Добавить")
            {
                if (currenttable.Add())
                {
                    Controls.Cast<Control>().OfType<TextBox>().ToList().ForEach(p => p.Clear());
                    Controls.Cast<Control>().OfType<NumericUpDown>().ToList().ForEach(p => p.Value = 0);
                }
            }
            else
            {
                currenttable.Edit((int)Row.Cells[0].Value);
            }
        }
    }
}
