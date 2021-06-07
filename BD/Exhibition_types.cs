using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Exhibition_types : Tables
    {
        TextBox tb { get; set; }
        public override string tablename { get; set; } ="exhibition_types";
        public override string select { get; set; } = "SELECT exhibition_t_id AS \"Id\", type AS Тип FROM exhibition_types";
        public override string delete { get; set; } = "DELETE FROM exhibition_types WHERE exhibition_t_id={0};";
        public override string insert { 
            get 
            { 
                return String.Format("insert into exhibition_types(type) values('{0}')",  NullCheck(tb.Text)); 
            } 
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Выставки, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Exhibition_types()
        {
        }

        public Exhibition_types(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
