using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Exhibitions : Tables
    {
        TextBox title;
        ComboBox city, type, org, aim;
        NumericUpDown account, interpr, stand, transp, marketing;
        DateTimePicker start, finish;

        public override string tablename { get; set; } = "exhibitions";
        public override string select { get; set; } = "SELECT exhibition_id AS \"Id\" , title AS Название, city " +
            "AS Город, date_start AS Начало, date_finish AS Окончание, account AS \"Количество посетителей\", type AS Тип," +
            "organizer AS Организатор, aim AS Тематика, interpreters AS \"Подготовка персонала\", stand AS Оформление," +
            "transportation AS Транспортировка, marketing AS \"Маркетинговые затраты\" FROM exhibitions a JOIN " +
            "cities b ON a.c_id = b.city_id JOIN exhibition_types c ON a.tp_id=c.exhibition_t_id JOIN organizers d ON " +
            "a.org_id=d.organizer_id JOIN exhibition_aims e ON a.a_id=e.aim_id ORDER BY exhibition_id ASC";

        public override string view { get; set; } = "SELECT exhibition_id AS \"Id\" , title AS Название, city " +
            "AS Город, date_start AS Начало, date_finish AS Окончание, account AS Посетители, type AS Тип," +
            "aim AS Тематика, interpreters AS \"Подготовка персонала\", stand AS Оформление,transportation AS " +
            "Транспортировка, marketing AS \"Маркетинговые затраты\" FROM exhibitions a JOIN " +
            "cities b ON a.c_id = b.city_id JOIN exhibition_types c ON a.tp_id=c.exhibition_t_id" +
            " JOIN exhibition_aims e ON a.a_id=e.aim_id WHERE a.org_id={0}";
        public override string delete { get; set; } = "DELETE FROM exhibitions WHERE exhibition_id={0};";
        public override string insert
        {
            get
            {
                return String.Format("insert into exhibitions(title, c_id, date_start, date_finish, account, tp_id, org_id," +
                    "a_id, interpreters, stand, transportation, marketing) values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11})",
                    NullCheck(title.Text), city.SelectedValue, start.Value, finish.Value, account.Value, type.SelectedValue
                    , org.SelectedValue, aim.SelectedValue, interpr.Value, stand.Value, transp.Value, marketing.Value);
            }
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Участие в выставках.\nВы уверены что хотите далить выбранные записи?";
        public override string update { 
            get 
            {
                return String.Format("UPDATE exhibitions SET title = '{0}', c_id = {1}, date_start='{2}', date_finish = '{3}'," +
                    "account = {4}, tp_id = {5}, org_id = {6}, a_id= {7}, interpreters = {8}, stand = {9}," +
                    "transportation ={10}, marketing={11} WHERE exhibitions.exhibition_id =", NullCheck(title.Text), city.SelectedValue, start.Value, finish.Value, account.Value, type.SelectedValue
                    , org.SelectedValue, aim.SelectedValue, interpr.Value, stand.Value, transp.Value, marketing.Value);
            }
        }

        public Exhibitions()
        {

        }

        public Exhibitions(ArrayList list)
        {
            title = (TextBox)list[0];
            city = (ComboBox)list[1];
            start = (DateTimePicker)list[2];
            finish = (DateTimePicker)list[3];
            account = (NumericUpDown)list[4];
            type = (ComboBox)list[5];
            org = (ComboBox)list[6];
            aim = (ComboBox)list[7];
            interpr = (NumericUpDown)list[8];
            stand = (NumericUpDown)list[9];
            transp = (NumericUpDown)list[10];
            marketing = (NumericUpDown)list[11];
        }

        public override string GetColumnName(string title)
        {
            switch (title)
            {
                case "Id":
                    return "exhibition_id";
                case "Название":
                    return "title";
                case "Город":
                    return "c_id";
                case "Начало":
                    return "date_start";
                case "Окончание":
                    return "date_finish";
                case "Количество посетителей":
                    return "account";
                case "Тип":
                    return "tp_id";
                case "Организатор":
                    return "org_id";
                case "Тематика":
                    return "a_id";
                case "Подготовка персонала":
                    return "interpreters";
                case "Оформление":
                    return "stand";
                case "Транспортировка":
                    return "transportation";
                case "Маркетинговые затраты":
                    return "marketing";
                default:
                    return null;
            }
        }

        public override string GetDeleteQuery(string column,object value)
        {
            switch (column)
            {
                case "c_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT city_id FROM cities a WHERE a.city='{2}')", tablename, column, value);
                case "tp_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT exhibition_t_id FROM exhibition_types a WHERE a.type='{2}')", tablename, column, value);
                case "org_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT organizer_id FROM organizers a WHERE a.organizer='{2}')", tablename, column, value);
                case "a_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT aim_id FROM exhibition_aims a WHERE a.aim='{2}')", tablename, column, value);
                default:
                    return String.Format("DELETE FROM {0} WHERE {1} = '{2}'",tablename,column,value);
            }
        }

        public override void FillComboBox(ComboBox cb)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("SELECT * FROM organizers", con);
                    adap.Fill(dt);
                    cb.DataSource = dt;
                    cb.DisplayMember = "organizer";
                    cb.ValueMember = "organizer_id";
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        public override void FillComboBox()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    DataTable dt1 = new DataTable();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select city_id,city from cities", con);
                    adap.Fill(dt1);
                    city.DataSource = dt1;
                    city.DisplayMember = "city";
                    city.ValueMember = "city_id";
                    DataTable dt2 = new DataTable();
                    adap = new NpgsqlDataAdapter("select * from exhibition_types", con);
                    adap.Fill(dt2);
                    type.DataSource = dt2;
                    type.DisplayMember = "type";
                    type.ValueMember = "exhibition_t_id";
                    DataTable dt3 = new DataTable();
                    adap = new NpgsqlDataAdapter("select * from organizers", con);
                    adap.Fill(dt3);
                    org.DataSource = dt3;
                    org.DisplayMember = "organizer";
                    org.ValueMember = "organizer_id";
                    DataTable dt4 = new DataTable();
                    adap = new NpgsqlDataAdapter("select * from exhibition_aims", con);
                    adap.Fill(dt4);
                    aim.DataSource = dt4;
                    aim.DisplayMember = "aim";
                    aim.ValueMember = "aim_id";
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }
    }
}
