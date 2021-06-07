using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;


namespace BD
{
    static class Queries
    {
        private static NpgsqlCommand cmd { get; set; }
        static string[] QueriesList { get; set; } = new string[]
        {
            //1
           "SELECT e.exhibit_id \"Id\", e.exhibit Название , a.author Автор, e.agecreate \"Век создания\", " +
            "e.money \"Страховая цена\", e.restoration \"Последняя реставрация\" FROM exhibits e " +
            "JOIN authors a ON a.author_id = e.auth_id JOIN exhibit_types t ON t.exhibit_t_id=e.tp_id WHERE t.type = '{0}'",
           //2
           "SELECT p.part_id \"Id участия\", e.exhibit_id \"Id экспоната\" ,e.exhibit Название , a.author Автор ,en.title Выставка FROM participation p " +
            "JOIN exhibits e ON p.ext_id = e.exhibit_id JOIN exhibitions en ON p.exn_id=en.exhibition_id JOIN authors a ON a.author_id=e.auth_id " +
            "WHERE e.exhibit = '{0}'",
           //3
           "SELECT museum_id \"Id\", museum Название, c.city Город, phone Телефон, private \"Частный музей\" " +
            "FROM museums m JOIN cities c ON m.c_id= c.city_id WHERE m.year={0}",
           //4
           "SELECT e.exhibition_id \"Id\", e.title Название, e.account \"Количество посетителей\", e.interpreters " +
            "\"Подготовка персонала\", e.stand Оформление, e.transportation Транспортировка, e.marketing " +
            "\"Маркетинговые затраты\" FROM exhibitions e WHERE e.date_start >= '{0}' AND e.date_start <='{1}'",
           //5
           "SELECT e.title \"Название выставки\" , ct.country Страна FROM exhibitions e " +
            "JOIN cities c ON e.c_id= c.city_id JOIN countries ct ON ct.country_id=c.c_id",
           //6
           "SELECT e.exhibit Экспонат, a.aim \"Тематика выставки\"FROM participation p " +
            "JOIN exhibits e ON p.ext_id = e.exhibit_id JOIN exhibitions en ON p.exn_id = en.exhibition_id " +
            "JOIN exhibition_aims a ON en.a_id = a.aim_id ORDER BY a.aim",
           //7
           "SELECT e.exhibit_id \"Id экспоната\", e.exhibit Экспонат, m.museum Музей FROM exhibits e " +
            "JOIN museums m ON e.mus_id = m.museum_id",
           //8
           "SELECT c.city_id id, c.city Город FROM cities c LEFT JOIN museums m ON m.c_id = c.city_id " +
            "WHERE m.museum IS NULL ORDER BY c.city",
           //9
           "SELECT e.exhibit \"Название экспоната\", e.money \"Страховая цена\", m.museum Музей FROM  exhibits e " +
            "RIGHT OUTER JOIN museums m ON m.museum_id = e.mus_id Where e.money > {0} ORDER BY m.museum",
           //10
           "SELECT c.city Город, e.title Выставка FROM cities c LEFT JOIN exhibitions e ON c.city_id=e.c_id " +
            "WHERE c.city_id IN (SELECT c.city_id FROM exhibitions e JOIN cities c ON e.c_id = c.city_id " +
            "WHERE e.date_start >= '{0}' AND e.date_finish <= '{1}') ORDER BY c.city"
        };

        public static DataTable Execute(int i)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter(QueriesList[i], con);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return null;
                }
            }
        }
        public static DataTable Execute(int i,object value)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter(String.Format(QueriesList[i],value), con);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return null;
                }
            }
        }

        public static DataTable Execute(int i,object value1, object value2)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter(String.Format(QueriesList[i], value1,value2), con);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return null;
                }
            }
        }
    }
}
