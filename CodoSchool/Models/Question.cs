using CodoSchool.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models
{
    public class Question : Entity
    {
        public string QuestionText { get; set; }

        public int SectionId { get; set; }
        public virtual Section Section { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
        public virtual IList<Answer> Answers { get; set; }
    }
}
