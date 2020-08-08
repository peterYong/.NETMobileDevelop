using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    /// <summary>
    /// 存在SQLite中的数据模型
    /// </summary>
   public class NoteData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        //public List<string> Hashs { get; set; }  //Sqlite类型不能是集合
    }
}
