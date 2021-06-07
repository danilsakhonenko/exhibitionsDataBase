using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Organizers : Tables
    {
        TextBox tb;
        public override string tablename { get; set; } = "organizers";
        public override string select { get; set; } = "SELECT organizer_id AS \"Id\", organizer AS Организатор FROM organizers";
        public override string delete { get; set; } = "DELETE FROM organizers WHERE organizer_id={0};";
        public override string insert { 
            get 
            { 
                return String.Format("insert into organizers(organizer) values('{0}')", NullCheck(tb.Text)); 
            } 
        }
        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Выставки, Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Organizers()
        {
        }

        public Organizers(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
