using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace BD
{
    public partial class MainForm : Form
    {
        //int state;
        Tables currenttable { get; set; }
        AddForm MainAddForm { get; set; }
        DirAddForm DirAddForm { get; set; }
        FieldForm FieldForm { get; set; }
        ViewForm ViewForm { get; set; }
        QueryForm QueryForm { get; set; }
        DiagramsForm DiagramsForm { get; set; }
        public MainForm()
        {
            InitializeComponent();
            currenttable = new Exhibitions();
        }

        private void Exhibitions_item_Click(object sender, EventArgs e)
        {
            currenttable = new Exhibitions();
            Main_view();
            Heading_label.Text = "Выставки";
            View_button.Enabled = true;
        }

        private void Participation_item_Click(object sender, EventArgs e)
        {
            currenttable = new Participation();
            Main_view();
            Heading_label.Text = "Участие в выставках";
        }

        private void Exhibits_item_Click(object sender, EventArgs e)
        {
            currenttable = new Exhibits();
            Main_view();
            Heading_label.Text = "Экспонаты";
        }

        private void Museums_item_Click(object sender, EventArgs e)
        {
            currenttable = new Museums();
            Main_view();
            Heading_label.Text = "Музеи";
            View_button.Enabled = true;
        }

        private void Main_view()
        {
            Main_table.DataSource = currenttable.FillTable(count_label);
            Main_table.ClearSelection();
            if (Change_button.Visible == false)
            {
                Change_button.Visible = true;
                Main_table.Visible = true;
                Directories_table.Visible = false;
                FieldRB.Visible = true;
                StandartRB.Visible = true;
                SearchButton.Visible = true;
                View_button.Visible = true;
            }
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            if (Change_button.Visible)
            {
                if (MainAddForm == null || MainAddForm.IsDisposed)
                {
                    MainAddForm = new AddForm(Heading_label.Text);
                    MainAddForm.Show();
                    MainAddForm.FormClosed += (obj, args) => RefreshTable();
                }
            }
            else
            {
                if (DirAddForm == null || DirAddForm.IsDisposed)
                {
                    DirAddForm = new DirAddForm(Heading_label.Text);
                    DirAddForm.Show();
                    DirAddForm.FormClosed += (obj, args) => RefreshTable();
                }
            }
        }

        private void Change_button_Click(object sender, EventArgs e)
        {
            if (MainAddForm == null || MainAddForm.IsDisposed)
            {
                MainAddForm = new AddForm(Heading_label.Text, Main_table.SelectedRows.Cast<DataGridViewRow>().First());
                MainAddForm.Show();
                MainAddForm.FormClosed += (obj, args) => RefreshTable();
            }
        }

        private void Exhibition_types_item_Click(object sender, EventArgs e)
        {
            currenttable = new Exhibition_types();
            Directories_view();
            Heading_label.Text = "Типы выставок";
        }

        private void Exhibition_themes_item_Click(object sender, EventArgs e)
        {
            currenttable = new Exhibition_aims();
            Directories_view();
            Heading_label.Text = "Тематики выставок";
        }

        private void Organizers_item_Click(object sender, EventArgs e)
        {
            currenttable = new Organizers();
            Directories_view();
            Heading_label.Text = "Организаторы выставок";
        }

        private void Authors_item_Click(object sender, EventArgs e)
        {
            currenttable = new Authors();
            Directories_view();
            Heading_label.Text = "Авторы";
        }

        private void Exhibit_types_item_Click(object sender, EventArgs e)
        {
            currenttable = new Exhibit_types();
            Directories_view();
            Heading_label.Text = "Типы экспонатов";
        }

        private void Countries_item_Click(object sender, EventArgs e)
        {
            currenttable = new Countries();
            Directories_view();
            Heading_label.Text = "Страны";
        }

        private void Cities_item_Click(object sender, EventArgs e)
        {
            currenttable = new Cities();
            Directories_view();
            Heading_label.Text = "Города";
        }

        private void Directories_view()
        {
            Directories_table.DataSource = currenttable.FillTable(count_label);
            Directories_table.ClearSelection();
            if (Change_button.Visible == false)
                return;
            View_button.Visible = false;
            Change_button.Visible = false;
            Main_table.Visible = false;
            Directories_table.Visible = true;
            FieldRB.Visible = false;
            StandartRB.Visible = false;
            SearchButton.Visible = false;
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (StandartRB.Checked)  //
                StandartDelete();
            else
            {
                if (FieldForm == null || FieldForm.IsDisposed)
                {
                    FieldForm = new FieldForm(currenttable, Main_table.Columns);
                    FieldForm.Show();
                    FieldForm.FormClosed += (obj, args) => RefreshTable();
                }
            }

        }

        private void StandartDelete()
        {
            List<int> selectedItems;
            if (Main_table.Visible == true)
                selectedItems = Main_table.SelectedRows.Cast<DataGridViewRow>().
                    Select(x => x.Cells[0].Value).Cast<int>().ToList();
            else
                selectedItems = Directories_table.SelectedRows.Cast<DataGridViewRow>().
                    Select(x => x.Cells[0].Value).Cast<int>().ToList();

            DialogResult result = MessageBox.Show(currenttable.warning, "Подтверждение удаления", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            this.Activate();
            if (result == DialogResult.No)
                return;
            if (currenttable.Delete(selectedItems))
                MessageBox.Show("Записи удалены", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            delete_button.Enabled = false;
            RefreshTable();
        }


        private void RefreshTable()
        {
            if (currenttable == null)
                return;
            if (Main_table.Visible)
                Main_table.DataSource = currenttable.FillTable(count_label);
            else
                Directories_table.DataSource = currenttable.FillTable(count_label);
        }

        private void Main_table_SelectionChanged(object sender, EventArgs e)
        {
            int count = Main_table.SelectedRows.Count;
            if (count == 0)
            {
                Change_button.Enabled = false;
                if (!FieldRB.Checked)
                    delete_button.Enabled = false;
            }
            else if (count > 1)
                Change_button.Enabled = false;
            else
            {
                Change_button.Enabled = true;
                delete_button.Enabled = true;
            }

        }

        private void Directories_table_SelectionChanged(object sender, EventArgs e)
        {
            if (Directories_table.SelectedRows.Count == 0)
                delete_button.Enabled = false;
            else if (!delete_button.Enabled)
                delete_button.Enabled = true;
        }

        private void FieldRB_CheckedChanged(object sender, EventArgs e)
        {
            if (!delete_button.Enabled)
                delete_button.Enabled = true;
        }

        private void StandartRB_CheckedChanged(object sender, EventArgs e)
        {
            if (delete_button.Enabled && Main_table.SelectedRows.Count == 0)
                delete_button.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FieldForm != null && !FieldForm.IsDisposed)
                FieldForm.Close();
            if (MainAddForm != null && !MainAddForm.IsDisposed)
                MainAddForm.Close();
            if (DirAddForm != null && !DirAddForm.IsDisposed)
                DirAddForm.Close();
            Application.Exit();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            currenttable.DeleteAll();
            RefreshTable();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            currenttable.Generate();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (FieldForm == null || FieldForm.IsDisposed)
            {
                FieldForm = new FieldForm(ref Main_table,count_label);
                FieldForm.Show();
                FieldForm.FormClosed += (obj, args) => RefreshTable();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void View_button_Click(object sender, EventArgs e)
        {
            if (ViewForm == null || ViewForm.IsDisposed)
            {
                ViewForm = new ViewForm(currenttable);
                ViewForm.Show();
                //FieldForm.FormClosed += (obj, args) => RefreshTable();
            }
        }

        private void StartQueryForm(int i)
        {
            if (QueryForm == null || QueryForm.IsDisposed)
            {
                QueryForm = new QueryForm(i);
                QueryForm.Show();
                QueryForm.FormClosed += (obj, args) => RefreshTable();
            }
        }

        private void Query2_Click(object sender, EventArgs e)
        {
            StartQueryForm(1);
        }

        private void Query3_Click(object sender, EventArgs e)
        {
            StartQueryForm(2);
        }

        private void Query4_Click(object sender, EventArgs e)
        {
            StartQueryForm(3);
        }

        private void Query5_Click(object sender, EventArgs e)
        {
            StartQueryForm(4);
        }

        private void Query6_Click(object sender, EventArgs e)
        {
            StartQueryForm(5);
        }

        private void Query7_Click(object sender, EventArgs e)
        {
            StartQueryForm(6);
        }

        private void Query8_Click(object sender, EventArgs e)
        {
            StartQueryForm(7);
        }

        private void Query9_Click(object sender, EventArgs e)
        {
            StartQueryForm(8);
        }

        private void Query10_Click(object sender, EventArgs e)
        {
            StartQueryForm(9);
        }

        private void Query1_Click(object sender, EventArgs e)
        {
            StartQueryForm(0);
        }

        private void Query11_Click(object sender, EventArgs e)
        {
            StartQueryForm(10);
        }

        private void Query12_Click(object sender, EventArgs e)
        {
            StartQueryForm(11);
        }

        private void Query13_Click(object sender, EventArgs e)
        {
            StartQueryForm(12);
        }

        private void Query14_Click(object sender, EventArgs e)
        {
            StartQueryForm(13);
        }

        private void Query15_Click(object sender, EventArgs e)
        {
            StartQueryForm(14);
        }

        private void Query16_Click(object sender, EventArgs e)
        {
            StartQueryForm(15);
        }

        private void QueryB1_Click(object sender, EventArgs e)
        {
            StartQueryForm(16);
        }

        private void QueryB2_Click(object sender, EventArgs e)
        {
            StartQueryForm(17);
        }

        private void QueryC_Click(object sender, EventArgs e)
        {
            StartQueryForm(18);
        }

        private void StartDiagramsForm(int i)
        {
            if (DiagramsForm== null || DiagramsForm.IsDisposed)
            {
                DiagramsForm = new DiagramsForm(i);
                DiagramsForm.Show();
            }

        }

        private void ExTypesDiagram_item_Click(object sender, EventArgs e)
        {
            StartDiagramsForm(1);
        }
    }
}
