using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    public class IM_MSG_CONTACT
    {
        public int ID { get; set; }
        public int OwnerUID { get; set; }
        public int OtherUID { get; set; }

        public int MID { get; set; }

        public int Type { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
