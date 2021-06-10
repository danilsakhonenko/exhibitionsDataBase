using System;
using System.Drawing;
using System.Windows.Forms;


namespace BD
{
    public partial class DirAddForm : Form
    {
        private string Table { get; set; }

        private Tables currenttable { get; set; }

        public DirAddForm(string table)
        {
            this.Table = table;
            InitializeComponent();
        }

        public void SetControls(string table)
        {
            switch (table)
            {
                case "Страны":
                    currenttable = new Countries(textBox1);
                    label2.Text = "Страна:";
                    label2.Location = new Point(textBox1.Location.X - 90, label2.Location.Y);
                    break;
                case "Города":
                    comboBox1.Visible = label1.Visible = true;
                    currenttable = new Cities(textBox1,comboBox1);
                    label2.Text = "Город:";
                    ((Cities)currenttable).FillComboBox(comboBox1);
                    break;
                case "Типы экспонатов":
                    label2.Text = "Тип экпоната:";
                    label2.Location = new Point(textBox1.Location.X - 160, label2.Location.Y);
                    currenttable = new Exhibit_types(textBox1);
                    break;
                case "Авторы":
                    label2.Text = "Автор:";
                    currenttable = new Authors(textBox1);
                    break;
                case "Организаторы выставок":
                    label2.Text = "Организатор:";
                    label2.Location = new Point(textBox1.Location.X - 145, label2.Location.Y);
                    currenttable = new Organizers(textBox1);
                    break;
                case "Тематики выставок":
                    label2.Text = "Тематика:";
                    label2.Location = new Point(textBox1.Location.X - 115, label2.Location.Y);
                    currenttable = new Exhibition_aims(textBox1);
                    break;
                case "Типы выставок":
                    label2.Text = "Тип выставки:";
                    label2.Location = new Point(textBox1.Location.X - 160, label2.Location.Y);
                    currenttable = new Exhibition_types(textBox1);
                    break;
            }
        }

        private void Return_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SetControls(Table);
        }
        protected void Add_button_Click(object sender, EventArgs e)
        {
            currenttable.Add();
            textBox1.Clear();
        }
    }
}
