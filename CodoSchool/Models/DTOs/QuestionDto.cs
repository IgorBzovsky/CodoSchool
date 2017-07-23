using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class AdminQuestionDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "question")]
        public string QuestionText { get; set; }

        [Required]
        [Display(Name = "correct answer")]
        public int? CorrectAnswerId { get; set; }
        public List<AnswerDto> Answers { get; set; }

        public AdminQuestionDto()
        {
            Answers = new List<AnswerDto>();
        }
    }
}
