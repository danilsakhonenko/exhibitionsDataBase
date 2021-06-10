using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;


namespace BD
{
    static class Queries
    {
        private static NpgsqlCommand cmd { get; set; }
        static string[] QueriesList { get; set; } = new string[]
        {
            //1
           "SELECT e.exhibit_id \"Id\", e.exhibit Название , a.author Автор, e.agecreate \"Век создания\", e.money \"Страховая цена\", " +
            "e.restoration \"Последняя реставрация\" FROM exhibits e JOIN authors a ON a.author_id = e.auth_id " +
            "JOIN exhibit_types t ON t.exhibit_t_id=e.tp_id WHERE t.type = '{0}' ORDER BY (e.exhibit)",
           //2
           "SELECT p.part_id \"Id участия\", en.title Выставка, en.date_start \"Начало\", en.date_finish \"Окончание\" " +
            "FROM participation p JOIN exhibits e ON p.ext_id = e.exhibit_id JOIN exhibitions en ON p.exn_id=en.exhibition_id " +
            "WHERE e.exhibit = '{0}' ORDER BY (en.title, en.date_start)",
           //3
           "SELECT museum_id \"Id\", museum Название, c.city Город, phone Телефон, private \"Частный музей\" FROM museums m " +
            "JOIN cities c ON m.c_id= c.city_id WHERE m.year={0} ORDER BY (m.museum)",
           //4
           "SELECT e.exhibition_id \"Id\", e.title Название, c.city Город,e.account \"Количество посетителей\" FROM exhibitions e " +
            "JOIN cities c ON e.c_id=c.city_id WHERE e.date_start >= '{0}' AND e.date_start <= '{1}' ORDER BY (e.title,c.city)",
           //5
           "SELECT e.title \"Название выставки\" , ct.country Страна FROM exhibitions e JOIN cities c ON e.c_id= c.city_id " +
            "JOIN countries ct ON ct.country_id=c.c_id ORDER BY (e.title, ct.country)",
           //6
           "SELECT e.exhibit_id \"Id экспоната\", e.exhibit Название, au.author Автор, a.aim \"Тематика выставки\" " +
            "FROM participation p JOIN exhibits e ON p.ext_id = e.exhibit_id JOIN authors au ON au.author_id=e.auth_id  " +
            "JOIN exhibitions en ON p.exn_id = en.exhibition_id JOIN exhibition_aims a ON en.a_id = a.aim_id ORDER BY (e.exhibit, au.author)",
           //7
           "SELECT e.exhibit_id \"Id экспоната\", e.exhibit Экспонат,a.author Автор, m.museum Музей, m.private Приватный FROM exhibits e " +
            "JOIN museums m ON e.mus_id = m.museum_id JOIN authors a ON e.auth_id=a.author_id ORDER BY (e.exhibit, a.author, m.museum)",
           //8
           "SELECT c.city_id id, c.city Город FROM cities c LEFT OUTER JOIN museums m ON m.c_id = c.city_id " +
            "WHERE m.museum IS NULL ORDER BY (c.city)",
           //9
           "SELECT e.exhibit_id \"Id экспоната\", e.exhibit \"Название\", e.money \"Страховая цена\", m.museum Музей FROM  exhibits e " +
            "RIGHT OUTER JOIN museums m ON m.museum_id = e.mus_id WHERE e.money > {0} ORDER BY e.exhibit",
           //10
           "SELECT c.city Город, e.title Выставка FROM cities c LEFT JOIN exhibitions e ON c.city_id=e.c_id WHERE e.exhibition_id IN " +
            "(SELECT e.exhibition_id FROM exhibitions e WHERE e.date_start >= '{0}' AND e.date_finish <= '{1}') ORDER BY (c.city,e.title)",
           //11
           "SELECT c.city Город, COUNT(m.museum_id) \"Кол-во музеев\" FROM cities c LEFT JOIN museums m ON m.c_id = c.city_id " +
            "GROUP BY (c.city) ORDER BY c.city",
           //12
           "SELECT o.organizer Организатор, COUNT(e.exhibition_id) \"Кол-во выставок\" FROM organizers o JOIN exhibitions e ON o.organizer_id =e.org_id " +
            "WHERE e.date_start >= '{0}' AND e.date_finish <='{1}' GROUP BY (o.organizer) ORDER BY o.organizer",
           //13
           "SELECT e.title \"Название выставки\", SUM(e.account) \"Общее кол-во посетителей\" FROM exhibitions e GROUP BY (e.title) " +
            "HAVING SUM(e.account) > {0} ORDER BY e.title",
           //14
           "SELECT a.author Автор, SUM(e.money) \"Ценность экспонатов\", COUNT(e.exhibit_id) \"Кол-во экспонатов\" FROM authors a " +
            "JOIN exhibits e ON a.author_id = e.auth_id WHERE e.restoration< {0} GROUP BY (a.author) HAVING SUM(e.money) > {1} ORDER BY(a.author)",
           //15
           "SELECT e.exhibition_id \"Id выставки\", e.title \"Название\",e.date_start \"Дата начала\",e.date_finish \"Дата окончания\", " +
            "e.account \"Кол-во посетителей\", t.type \"Тип выставки\", o.organizer Организатор, a.aim Тематика, e.marketing \"Маркетинговые затраты\"" +
            "FROM exhibitions e JOIN organizers o ON o.organizer_id = e.org_id JOIN exhibition_types t ON e.tp_id = t.exhibition_t_id " +
            "JOIN exhibition_aims a ON a.aim_id=e.a_id WHERE e.marketing > (SELECT AVG(marketing) FROM exhibitions) " +
            "ORDER BY (e.title, e.date_start, e.date_finish)",
           //16
           "SELECT m.museum_id \"Id музея\", m.museum Название, c.city Город, m.year \"Год основания\", m.phone Телефон, m.private Приватный " +
            "FROM museums m JOIN cities c ON m.c_id = c.city_id WHERE m.year < (SELECT AVG(m.year) FROM museums m) ORDER BY(m.museum)",
            //b(17,18)
            "SELECT e.title \"Название выставки\", AVG(e.account)::integer \"Среднее кол-во посетителей\" FROM exhibitions e GROUP BY (e.title) " +
            "ORDER BY (e.title)",
            "SELECT c.country Страна, SUM(e.account)/COUNT(e.exhibition_id)::integer \"Среднее кол-во посетителей\" FROM countries c " +
            "JOIN cities ct ON c.country_id=ct.c_id JOIN exhibitions e ON e.c_id=ct.city_id GROUP BY (c.country) ORDER BY (c.country)",
            //c(19)
            "SELECT e.exhibition_id \"Id выставки\", e.title Название, e.interpreters + e.stand + e.transportation + e.marketing \"Общие расходы\" " +
            "FROM exhibitions e WHERE e.date_start>='{0}' AND e.date_finish<='{1}' ORDER BY (e.title)"

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
