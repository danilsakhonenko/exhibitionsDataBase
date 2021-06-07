using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Countries : Tables
    {
        TextBox tb { get; set; }
        public override string tablename { get; set; } = "countries";
        public override string select { get; set; } = "SELECT country_id AS \"Id\" , country AS Страна FROM countries";
        public override string delete { get; set; } = "DELETE FROM countries WHERE country_id={0};";
        public override string insert { get { return String.Format("insert into countries(country) values('{0}')", NullCheck( tb.Text)); } }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Города, Музеи , Выставки, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Countries()
        {
        }

        public Countries(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
