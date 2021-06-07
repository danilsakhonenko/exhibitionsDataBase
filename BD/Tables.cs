using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public abstract class Tables
    {
        private NpgsqlCommand cmd { get; set; }
        StreamReader sr { get; set; }
        public abstract string tablename { get; set; }
        public abstract string select { get; set; }
        public abstract string delete { get; set; }
        public abstract string insert { get; }
        public abstract string warning { get; }
        public virtual string update { get; }
        public virtual string view { get; set; }

        public Tables()
        {
        }

        public DataTable FillTable(Label count_tb)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter(select, con);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    string count = String.Format("SELECT count(*) FROM {0}", this.tablename);
                    cmd = new NpgsqlCommand(count, con);
                    count_tb.Text = cmd.ExecuteScalar().ToString();
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

        public DataTable FillTable(object id)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    NpgsqlDataAdapter adap = new NpgsqlDataAdapter(String.Format(view,id), con);
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

        public bool Delete(List<int> list)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    foreach (int id in list)
                    {
                        cmd = new NpgsqlCommand(String.Format(delete, id), con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return false;
                }
            }
        }
        public bool Add()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    cmd = new NpgsqlCommand(insert, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Запись добавлена", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка ввода " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return false;
                }
            }
        }

        public bool FieldDelete(string column, object value)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    con.Open();
                    cmd = new NpgsqlCommand(GetDeleteQuery(column, value), con);
                    int count = cmd.ExecuteNonQuery();
                    con.Close();
                    if (count > 0)
                        return true;
                    else
                    {
                        MessageBox.Show("Нет подходящих записей для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return false;
                }
            }
        }

        public void Edit(int id)
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    string command = update + id.ToString();
                    con.Open();
                    cmd = new NpgsqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Запись изменена", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка ввода " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        public void DeleteAll()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                try
                {
                    string command = "TRUNCATE participation, exhibitions, exhibits, museums, cities, countries, " +
                        "exhibition_types, exhibition_aims, exhibit_types, organizers, authors RESTART IDENTITY";
                    con.Open();
                    cmd = new NpgsqlCommand(command, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Таблицы очищены", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        async public void Generate()
        {
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                await Task.Run(() =>
                {
                    try
                    {
                        con.Open();
                        GenerateTable("dir", con);
                        GenerateTable("museums", con);
                        GenerateTable("exhibits", con);
                        GenerateTable("exhibitions", con);
                        GenerateTable("participation", con);
                        MessageBox.Show("Генерация таблиц завершена", "Операция выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                });

            }

        }



        public void GenerateTable(string name, NpgsqlConnection con)
        {
            string query;
            sr = new StreamReader(String.Format("{0}.txt", name));
            query = sr.ReadToEnd();
            sr.Close();
            cmd = new NpgsqlCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        protected string NullCheck(string s)
        {
            if (s == "")
                return "'NULL'";
            else
                return s;
        }

        protected object NullCheck(int index)
        {
            if (index == 0)
                return "NULL";
            else
                return index;
        }

        public virtual string GetColumnName(string title)
        {
            return null;
        }

        public virtual string GetDeleteQuery(string column, object value)
        {
            return null;
        }

        public virtual void FillComboBox()
        {
        }

        public virtual void FillComboBox(ComboBox cb)
        {
        }
    }
}
