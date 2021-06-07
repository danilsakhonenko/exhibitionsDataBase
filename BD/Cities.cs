using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Cities : Tables
    {
        ComboBox cb { get; set; }
        TextBox tb { get; set; }

        public override string tablename { get; set; } = "cities";
        public override string select { get; set; } = "SELECT city_id AS \"Id\", city AS Город, country " +
            "AS Страна FROM cities AS a ,countries AS b WHERE a.c_id = b.country_id";
        public override string delete { get; set; } = "DELETE FROM cities WHERE city_id={0};";
        public override string insert { 
            get 
            { 
                return String.Format("insert into cities(city, c_id) values('{0}',{1})", NullCheck(tb.Text), cb.SelectedValue); 
            }
        }

        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Музеи , Выставки, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Cities()
        {
        }

        public Cities(TextBox tb, ComboBox cb)
        {
            this.tb = tb;
            this.cb = cb;
        }

        public override void FillComboBox(ComboBox cb)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select * from countries", con);
                    adap.Fill(dt);
                    cb.DataSource = dt;
                    cb.DisplayMember = "country";
                    cb.ValueMember = "country_id";
                    cb.Visible = true;
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
