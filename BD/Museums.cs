using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Museums : Tables
    {
        TextBox name,phone;
        ComboBox cities_cb,private_cb;
        NumericUpDown year;

        public override string tablename { get; set; } = "museums";
        public override string select { get; set; }= "SELECT museum_id AS \"Id\", museum AS Название, city " +
            "AS Город, year AS \"Год создания\", phone AS Телефон, private AS Частный FROM museums a JOIN " +
            "cities b ON a.c_id = b.city_id ORDER BY museum_id ASC";
        public override string view { get; set; } = "SELECT museum_id AS \"Id\", museum AS Название," +
            " year AS \"Год создания\", phone AS Телефон, private AS Частный FROM museums a WHERE a.c_id={0}";

        public override string delete { get; set; }= "DELETE FROM museums WHERE museum_id={0};";
        public override string insert
        {
            get
            {
                return String.Format("insert into museums(museum, c_id,year,phone,private) values('{0}',{1},{2},'{3}',{4})",
                    NullCheck(name.Text), cities_cb.SelectedValue,(int)year.Value, NullCheck(phone.Text),Convert.ToBoolean(private_cb.SelectedIndex));
            }
        }
        public override string update
        {
            get
            {
                return String.Format("UPDATE museums SET museum = '{0}', c_id = {1}, year={2}, phone = '{3}'," +
                    "private = {4} WHERE museums.museum_id =", NullCheck(name.Text), cities_cb.SelectedValue, 
                    (int)year.Value, NullCheck(phone.Text), Convert.ToBoolean(private_cb.SelectedIndex));
            }
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Экспонаты, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Museums()
        {
        }

        public Museums(TextBox name,ComboBox ct,NumericUpDown year,TextBox phone, ComboBox priv)
        {
            this.name = name;
            this.phone = phone;
            cities_cb = ct;
            private_cb = priv;
            this.year = year;
        }

        public override void FillComboBox()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select city_id,city  from cities", con);
                    adap.Fill(dt);
                    cities_cb.DataSource = dt;
                    cities_cb.DisplayMember = "city";
                    cities_cb.ValueMember = "city_id";
                    private_cb.Items.AddRange(new[] { "Нет", "Да" });
                    private_cb.SelectedIndex = 0;
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
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
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select city_id,city from cities", con);
                    adap.Fill(dt);
                    cb.DataSource = dt;
                    cb.DisplayMember = "city";
                    cb.ValueMember = "city_id";
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
                    return "museum_id";
                case "Название":
                    return "museum";
                case "Город":
                    return "c_id";
                case "Год создания":
                    return "year";
                case "Телефон":
                    return "phone";
                case "Частный":
                    return "private";
                default:
                    return null;
            }
        }

        public static string ConvertBool(string text)
        {
            if (text == "Да" || text == "да" || text == "+")
                return "True";
            else if (text == "Нет" || text == "нет" || text == "-")
                return "False";
            else
                return text;

        }

        public override string GetDeleteQuery(string column, object value)
        {
            if (column == "private")
            {
                value = ConvertBool((string)value);
            }
            if (column != "c_id")
                return String.Format("DELETE FROM {0} WHERE {1} = '{2}'", tablename, column, value);
            else
                return String.Format("DELETE FROM {0} WHERE {1} = (SELECT city_id FROM cities a WHERE a.city='{2}')", tablename, column, value);
        }
    }
}
