using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models
{
    public class StudentProgress
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Section Section { get; set; }
        public string ApplicationUserId { get; set; }
        public int SectionId { get; set; }
    }
}
