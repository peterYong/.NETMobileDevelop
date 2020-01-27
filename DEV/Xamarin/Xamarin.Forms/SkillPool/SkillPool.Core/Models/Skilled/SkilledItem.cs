using SkillPool.Core.Models.Enum;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.Models.Skilled
{
    /// <summary>
    /// 技能数据
    /// </summary>
    public class SkilledItem 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        //public SkillType Type { get; set; }
        public string Type { get; set; }

        public string Description { get; set; }

        public string PictureUri { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

    }
}
