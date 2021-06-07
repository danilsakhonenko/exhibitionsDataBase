using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Connection
    {
        public static string user { get; set; }
        public static string pass { get; set; }
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;User Id=" + user + ";Password=" + pass + ";Database=exhibitionsdb;");
        }
    }
}
