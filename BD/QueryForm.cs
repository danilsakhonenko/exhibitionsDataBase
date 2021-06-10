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
            if((QueryType>3 && QueryType < 8)||(QueryType>13 && QueryType < 18) || QueryType == 10)
            {
                textBox2.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                label1.Visible = false;
                switch (QueryType)
                {
                    case 4:
                        Header.Text = "Вывести названия выставок и страну в которой они проходили";
                        break;
                    case 5:
                        Header.Text = "Вывести информацию об экспонате и тематики выставок в которых он участвовал";
                        break;
                    case 6:
                        Header.Text = "Вывести информацию об экспонате, название музея, которому он принадлежит \nи форму собственности";
                        break;
                    case 7:
                        Header.Text = "Вывести информацию о городах, в которых нет музеев";
                        break;
                    case 10:
                        Header.Text = "Вывести количество музеев в каждом городе";
                        break;
                    case 14:
                        Header.Text = "Вывести выставки, маркетинговые затраты на которые выше среднего значения \nпо всем выставкам";
                        break;
                    case 15:
                        Header.Text = "Выдать информацию о музеях, у которых год создания меньше среднего года \nсоздания среди всех музеев";
                        break;
                    case 16:
                        Header.Text = "Определить среднее количество посетителей по каждой выставке";
                        break;
                    case 17:
                        Header.Text = "Определить среднее количество посетителей по каждой стране";
                        break;

                }
            }
            else if((QueryType > -1 && QueryType < 3) || QueryType==8 || QueryType == 12)
            {
                textBox2.Visible = false;
                label2.Visible = false;
                label1.Location = new Point(label1.Location.X - 105, label1.Location.Y);
                switch (QueryType)
                {
                    case 0:
                        label1.Text = "Тип экспоната:";
                        Header.Text = "Вывести все экспонаты заданного типа";
                        break;
                    case 1:
                        label1.Text = "Название:";
                        Header.Text = "Вывести все участия экспонатов в выставках по названию экспоната";
                        break;
                    case 2:
                        label1.Text = "Год создания:";
                        Header.Text = "Вывести информацию о музеях по вводимому году создания";
                        break;
                    case 8:
                        label1.Text = "Цена:";
                        Header.Text = "Вывести экспонаты со страховой ценой, больше заданной и музеи, которым они \nпринадлежат";
                        break;
                    case 12:
                        label1.Text = "Кол-во посетителей:";
                        label1.Location = new Point(label1.Location.X - 15, label1.Location.Y);
                        textBox1.Location = new Point(textBox1.Location.X + 35, textBox1.Location.Y);
                        Header.Text = "Вывести названия выставок с общим количеством посетителей, превышающим \nзаданное значение";
                        break;

                }
            }
            else
            {
                switch (QueryType) 
                {
                    case 3:
                        Header.Text = "Вывести все выставки, которые начались в период с – по";
                        break;
                    case 9:
                        Header.Text = "Вывести города и названия выставок, проходивших в них в период с-по";
                        break;
                    case 11:
                        Header.Text = "Вывести организаторов, которые проводили выставки в период с_ по_ и количество \nвыставок";
                        break;
                    case 13:
                        label1.Text = "Год:";
                        label1.Location = new Point(label1.Location.X - 15, textBox1.Location.Y);
                        label2.Text = "Цена:";
                        label2.Location = new Point(label2.Location.X - 15, textBox1.Location.Y);
                        Header.Text = "Вывести авторов и общую ценность их экспонатов, год реставрации которых \nменьше заданного года, а общая ценность больше заданной";
                        break;
                    case 18:
                        Header.Text = "Определить расходы на организацию каждой выставки за период времени с_ по_";
                        break;
                }
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
