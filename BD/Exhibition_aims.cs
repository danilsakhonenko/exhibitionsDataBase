using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Exhibition_aims : Tables
    {
        TextBox tb { get; set; }
        public override string tablename { get; set; } = "exhibition_aims";
        public override string select { get; set; } = "SELECT aim_id AS \"Id\", aim AS Тематика FROM exhibition_aims";
        public override string delete { get; set; } = "DELETE FROM exhibition_aims WHERE aim_id={0};";
        public override string insert { 
            get 
            { 
                return String.Format("insert into exhibition_aims(aim) values('{0}')", NullCheck(tb.Text)); 
            } 
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " ВЫставки, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Exhibition_aims()
        {
        }

        public Exhibition_aims(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
