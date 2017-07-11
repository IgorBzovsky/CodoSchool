using CodoSchool.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models
{
    public class StudentProgress : Entity
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        public bool Completed { get; set; }
    }
}
