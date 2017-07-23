using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Models.DTOs
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }

        public QuestionDto()
        {
            Answers = new List<Answer>();
        }
        public virtual IList<Answer> Answers { get; set; }
    }
}
