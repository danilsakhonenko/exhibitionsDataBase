using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Participation : Tables
    {
        ComboBox exhibit, exhibition;
        public override string tablename { get; set; } = "participation";
        public override string select { get; set; }= "SELECT part_id AS \"Id\", exhibit AS Экспонат, title AS Выставка FROM " +
            "participation a JOIN exhibits b ON a.ext_id = b.exhibit_id JOIN exhibitions c ON a.exn_id=c.exhibition_id ORDER BY part_id ASC";
        public override string delete { get; set; } = "DELETE FROM participation WHERE part_id='{0}';";
        public override string insert
        {
            get
            {
                return String.Format("insert into participation(ext_id, exn_id) values({0},{1})",
                    exhibit.SelectedValue, exhibition.SelectedValue);
            }
        }
        public override string update
        {
            get
            {
                return String.Format("UPDATE participation SET ext_id = {0}, exn_id = {1} WHERE participation.part_id =", 
                    exhibit.SelectedValue, exhibition.SelectedValue);
            }
        }


        public override string warning { get; } = "Вы уверены что хотите далить выбранные записи?";

        public Participation()
        {

        }

        public Participation(ComboBox ext,ComboBox exn)
        {
            exhibit = ext;
            exhibition = exn;
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
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter("select exhibit_id, exhibit from exhibits", con); 
                    adap.Fill(dt1);
                    exhibit.DataSource = dt1;
                    exhibit.DisplayMember = "exhibit";
                    exhibit.ValueMember = "exhibit_id";
                    adap = new NpgsqlDataAdapter("select exhibition_id, title from exhibitions", con);
                    adap.Fill(dt2);
                    exhibition.DataSource = dt2;
                    exhibition.DisplayMember = "title";
                    exhibition.ValueMember = "exhibition_id";
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
                    return "part_id";
                case "Экспонат":
                    return "ext_id";
                case "Выставка":
                    return "exn_id";
                default:
                    return null;
            }
        }

        public override string GetDeleteQuery(string column, object value)
        {
            switch (column)
            {
                case "ext_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT exhibit_id FROM exhibits a WHERE a.exhibit ='{2}')", tablename, column, value);
                case "exn_id":
                    return String.Format("DELETE FROM {0} WHERE {1} = (SELECT exhibition_id FROM exhibitions a WHERE a.title ='{2}')", tablename, column, value);
                default:
                    return String.Format("DELETE FROM {0} WHERE {1} = '{2}'", tablename, column, value);
            }
        }
    }
}
