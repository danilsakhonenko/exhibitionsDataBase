using System;
using System.Drawing;
using System.Windows.Forms;


namespace BD
{
    public partial class QueryForm : Form
    {
        private int QueryType { get; set; }
        public QueryForm(int type)
        {
            QueryType = type;
            InitializeComponent();
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            if (QueryType == 0)
            {
                textBox2.Visible = false;
                label2.Visible = false;
                label1.Location = new Point(label1.Location.X - 25, label1.Location.Y);
                label1.Text = "Тип экспоната:";
                Header.Text = "Вывести все экспонаты заданного типа";
            }
            else if (QueryType == 1)
            {
                textBox2.Visible = false;
                label2.Visible = false;
                label1.Text = "Название:";
                Header.Text = "Вывести все участия экспоната в выставках по его названию";
            }
            else if (QueryType == 2)
            {
                textBox2.Visible = false; 
                label2.Visible = false;
                label1.Text = "Год создания:";
                label1.Location=new Point(label1.Location.X - 25, label1.Location.Y);
                Header.Text = "Вывести информацию о музеях по вводимому году создания";
            }
            else if (QueryType == 3)
            {
                label1.Text = "c:";
                label1.Location = new Point(label1.Location.X + 25, label1.Location.Y);
                label2.Text = "по:";
                label2.Location = new Point(label2.Location.X + 25, label2.Location.Y);
                Header.Text = "Вывести все выставки, которые начались в период с – по";
            }
            else if (QueryType == 4)
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                Header.Text = "Вывести названия выставок и страну в которой они проходили";
            }
            else if (QueryType == 5)
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                Header.Text = "Вывести информацию об экспонате и тематику выставки в которой он участвует";
            }
            else if (QueryType == 6)
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                Header.Text = "Вывести информацию об экспонате, название музея, которому он принадлежит \nи форму собственности";
            }
            else if (QueryType == 7)
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                Header.Text = "Вывести информацию о городах, в которых нет музеев";
            }
            else if (QueryType == 8)
            {
                textBox2.Visible = false;
                label2.Visible = false;
                label1.Text = "Цена:";
                Header.Text = "Вывести информацию об экспонатах со страховой ценой, больше заданной и о музеях, \nкоторым они принадлежат";
            }
            else if (QueryType == 9)
            {
                label1.Text = "с:";
                label1.Location = new Point(label1.Location.X + 25, label1.Location.Y);
                label2.Text = "по:";
                label2.Location = new Point(label2.Location.X + 25, label2.Location.Y);
                Header.Text = "Вывести города и выставки, проходившие в них в период с-по";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QueryType == 0 || QueryType == 1 || QueryType == 2 || QueryType == 8)
                dataGridView1.DataSource = Queries.Execute(QueryType,textBox1.Text);
            else if(QueryType == 4 || QueryType == 5 || QueryType == 6 || QueryType == 7)
                dataGridView1.DataSource = Queries.Execute(QueryType);
            else
                dataGridView1.DataSource = Queries.Execute(QueryType,textBox1.Text, textBox2.Text);
            Count_label.Text = dataGridView1.Rows.Count.ToString();
        }
    }
}
