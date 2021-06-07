using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class ViewForm : Form
    {
        Tables currenttable { get; set; }
        bool start { get; set; }
        public ViewForm(Tables table)
        {
            currenttable = table;
            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            if (currenttable.tablename == "museums")
            {
                Header_label.Text = "Просмотр по городам";
                Select_label.Text = "Выбор города:";
                currenttable.FillComboBox(comboBox1);
            }
            else if(currenttable.tablename== "exhibitions")
            {
                Header_label.Text = "Просмотр по организаторам";
                Select_label.Location = new Point(Select_label.Location.X - 50, Select_label.Location.Y);
                Select_label.Text = "Выбор организатора:";
                currenttable.FillComboBox(comboBox1);
            }
            comboBox1.SelectedValue = 0;
            start = true;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (start)
            {
                dataGridView1.DataSource=currenttable.FillTable(comboBox1.SelectedValue);
                count_label.Text = dataGridView1.Rows.Count.ToString();
            }
        }
    }
}
