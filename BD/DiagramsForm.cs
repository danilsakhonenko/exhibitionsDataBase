using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class DiagramsForm : Form
    {
        private int DiagramType { get; set; }
        public DiagramsForm(int i)
        {
            DiagramType = i;
            InitializeComponent();
        }

        private void DiagramsForm_Load(object sender, EventArgs e)
        {
            switch (DiagramType)
            {
                case 1:
                    PieDiagram();                 
                    break;
            }
        }

        private void PieDiagram()
        {
            chart1.Series[0].ChartType =System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series[0].Points.Clear();
            using (NpgsqlConnection con = Connection.GetConnection())
            {
                string Query = "SELECT t.type,(COUNT(t.exhibition_t_id) / (SELECT COUNT(*) FROM exhibitions)::float) " +
                    "FROM exhibition_types t JOIN exhibitions e ON t.exhibition_t_id = e.tp_id GROUP BY(t.type)";
                con.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(Query, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    { 
                        chart1.Series[0].Points.AddY(reader.GetDouble(1));
                        chart1.Series[0].Points[chart1.Series[0].Points.Count - 1].LegendText = reader.GetString(0) + ": " + Math.Round(reader.GetDouble(1) * 100, 2).ToString() + "%";
                    }
                    con.Close();
                }
                catch (Npgsql.PostgresException ex)
                {
                    MessageBox.Show("Error " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

    }
}
