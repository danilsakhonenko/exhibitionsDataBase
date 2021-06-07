using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Exhibit_types : Tables
    {
        TextBox tb { get; set; }
        public override string tablename { get; set; } = "exhibit_types";
        public override string select { get; set; } = "SELECT exhibit_t_id AS \"Id\", type AS Тип FROM exhibit_types";
        public override string delete { get; set; } = "DELETE FROM exhibit_types WHERE exhibit_t_id={0};";
        public override string insert { get { return String.Format("insert into exhibit_types(type) values('{0}')", NullCheck(tb.Text)); } }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Экспонаты , Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Exhibit_types()
        {
        }

        public Exhibit_types(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
