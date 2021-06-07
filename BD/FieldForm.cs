using System;
using System.Data;
using System.Windows.Forms;


namespace BD
{
    public partial class FieldForm : Form
    {
        DataGridViewColumnCollection Columns { get; set; }
        DataGridView DataGrid { get; set; }
        DataTable dt { get; set; }
        private Tables Table { get; set; }
        Label count { get; set; }
        public FieldForm(Tables table, DataGridViewColumnCollection columns)
        {
            this.Table = table;
            Columns = columns;
            InitializeComponent();
            this.Text = "Удаление по полю";
        }

        public FieldForm(ref DataGridView datagrid, Label count)
        {
            DataGrid = datagrid;
            this.count = count;
            dt = (DataTable)datagrid.DataSource;
            Columns = datagrid.Columns;
            InitializeComponent();
            Action_button.Text = "Поиск";
            this.Text = "Поиск";
            label1.Text = "Поиск по полю";
        }

        private void FieldDelete_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                col_cb.Items.Add(Columns[i].Name);
            }
        }

        private void Action_Click(object sender, EventArgs e)
        {
            if (DataGrid == null)
            {
                if (Table.FieldDelete(Table.GetColumnName(col_cb.SelectedItem.ToString()), value.Text))
                    MessageBox.Show("Записи удалены", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataTable searchdt = dt.Clone();
                searchdt.Rows.Clear();
                int j = col_cb.SelectedIndex;
                string valuestr = null;
                if (dt.Columns[j].ColumnName == "Частный")
                    valuestr=Museums.ConvertBool(value.Text);
                else
                    valuestr = value.Text;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[j].ToString()==valuestr)
                        searchdt.Rows.Add(dt.Rows[i].ItemArray);
                }
                count.Text = searchdt.Rows.Count.ToString();
                DataGrid.DataSource = searchdt;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Action_button.Enabled)
                Action_button.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
