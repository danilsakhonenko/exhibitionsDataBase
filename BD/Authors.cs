using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BD
{
    class Authors : Tables
    {
        TextBox tb { get; set; }
        public override string tablename { get; set; } = "authors";
        public override string select { get; set; } = "SELECT author_id AS \"Id\", author AS Автор FROM authors";
        public override string delete{get; set;} = "DELETE FROM authors WHERE author_id={0};";
        public override string insert { get { return String.Format("insert into authors(author) values('{0}')", NullCheck(tb.Text)); } }

        public override string warning { get; } = "Удаление выбранных элементов может привести к удалению записей в таблицах:" +
            " Экпонаты , Участие в выставках.\nВы уверены что хотите далить выбранные записи?";

        public Authors()
        {
        }

        public Authors(TextBox tb)
        {
            this.tb = tb;
        }
    }
}
