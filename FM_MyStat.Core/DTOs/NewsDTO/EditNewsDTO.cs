using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.NewsDTO
{
    public class EditNewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimePublication { get; set; } = DateTime.Now;
        public DateTime Time { get; set; }
    }
}
