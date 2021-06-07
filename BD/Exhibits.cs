using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Exhibits : Tables
    {
        ComboBox type, author, museum;
        TextBox name, comm;
        NumericUpDown age, restor, transfer,cost;

        public override string tablename { get; set; } = "exhibits";
        public override string select { get; set; } = "SELECT exhibit_id AS \"Id\" , exhibit AS Название, type " +
            "AS Тип, author AS Автор, agecreate AS Век, transfer AS \"Год передачи музею\", money AS Цена," +
            "comment AS Описание, restoration AS Реставрация, museum AS Музей FROM exhibits a JOIN " +
            "exhibit_types b ON a.tp_id = b.exhibit_t_id JOIN authors c ON a.auth_id=c.author_id JOIN museums d ON a.mus_id=d.museum_id ORDER BY exhibit_id ASC";
        public override string delete { get; set; } = "DELETE FROM exhibits WHERE exhibit_id={0};";
        public override string insert
        {
            get
            {
                return String.Format("insert into exhibits(exhibit, tp_id, auth_id, agecreate, " +
                    "transfer, money, comment, restoration, mus_id) values('{0}',{1},{2},{3},{4},{5},'{6}',{7},{8})",
                    NullCheck(name.Text), type.SelectedValue,NullCheck((int)author.SelectedValue), age.Value, transfer.Value, cost.Value,
                    comm.Text ,restor.Value, museum.SelectedValue);
            }
        }
        public override string update
        {
            get
            {
                return String.Format("UPDATE exhibits SET exhibit='{0}', tp_id={1}, auth_id={2}, agecreate={3}, " +
                    "transfer={4}, money={5}, comment='{6}', restoration={7}, mus_id={8} WHERE exhibits.exhibit_id =", 
                    NullCheck(name.Text), type.SelectedValue, NullCheck((int)author.SelectedValue), age.Value, 
                    transfer.Value, cost.Value, NullCheck(comm.Text), restor.Value, museum.SelectedValue);
            }
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Exhibits()
        {

        }

        public Exhibits (ArrayList list)
        {
            name = (TextBox)list[0];
            type= (ComboBox)list[1];
            author = (ComboBox)list[2];
            age = (NumericUpDown)list[3];
            transfer = (NumericUpDown)list[4];
            cost = (NumericUpDown)list[5];
            comm = (TextBox)list[6];
            restor = (NumericUpDown)list[7];
            museum = (ComboBox)list[8];
        }
        public override void FillComboBox()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select * from exhibit_types", con);
                    adap.Fill(dt1);
                    type.DataSource = dt1;
                    type.DisplayMember = "type";
                    type.ValueMember = "exhibit_t_id";
                    adap= new NpgsqlDataAdapter("select * from authors", con);
                    adap.Fill(dt2);
                    author.DataSource = dt2;
                    author.DisplayMember = "author";
                    author.ValueMember = "author_id";
                    adap = new NpgsqlDataAdapter("select museum_id, museum from museums", con);
                    adap.Fill(dt3);
                    museum.DataSource = dt3;
                    museum.DisplayMember = "museum";
                    museum.ValueMember = "museum_id";
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        public override string GetColumnName(string title)
        {
            switch (title)
            {
                case "Id":
                    return "exhibit_id";
                case "Название":
                    return "exhibit";
                case "Тип":
                    return "tp_id";
                case "Автор":
                    return "auth_id";
                case "Век":
                    return "agecreate";
                case "Год передачи музею":
                    return "transfer";
                case "Цена":
                    return "money";
                case "Описание":
                    return "comment";
                case "Реставрация":
                    return "restoration";
                case "Музей":
                    return "mus_id";
                default:
                    return null;
            }
        }

        public override string GetDeleteQuery(string column, object value)
        {
            switch (column)
            {
                case "mus_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT museum_id FROM museums a WHERE a.museum='{2}')", tablename, column, value);
                case "tp_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT exhibit_t_id FROM exhibit_types a WHERE a.type='{2}')", tablename, column, value);
                case "auth_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT author_id FROM authors a WHERE a.author='{2}')", tablename, column, value);
                default:
                    return String.Format("DELETE FROM {0} WHERE {1} = '{2}'", tablename, column, value);
            }
        }
    }
}
