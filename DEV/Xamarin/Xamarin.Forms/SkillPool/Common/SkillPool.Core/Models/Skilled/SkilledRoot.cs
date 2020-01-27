using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.Models.Skilled
{
   public class SkilledRoot
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<SkilledItem> Data { get; set; }
    }
}
