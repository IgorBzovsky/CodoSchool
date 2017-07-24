using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Models.DTOs;

namespace CodoSchool.ViewComponents
{
    [ViewComponent]
    public class QuizQuestionViewComponent : ViewComponent
    {
        //глянути
        //paging VC 
        //view state
        //msvs go to all

        //public IViewComponentResult InvokeAsync(
        //    Question currentQuestion, 
        //    int currentCount, 
        //    int totalCount,
        //    string themeName)
        public Task<IViewComponentResult> InvokeAsync(QuestionDto _quizQuestion)
        {




            switch ((int)TempData.Peek("Status"))
            {
                case 0:
                    ViewBag.description = TempData.Peek("Dscription");
                    HttpContext.Session.Clear();
                    return Task.FromResult<IViewComponentResult>(View("StartQuiz"));


                case 1:
                    ViewBag.questionIndex = TempData.Peek("questionIndex");
                    ViewBag.questionsCount = TempData.Peek("questionsCount");
                    ViewBag.asnwers = TempData.Peek("answers");
                    ViewBag.answerIndex = ViewBag.asnwers[ViewBag.questionIndex];

                    TempData.Save();

                    return Task.FromResult<IViewComponentResult>(View("Default", _quizQuestion));


                case 2:

                    HttpContext.Session.Clear();
                    return Task.FromResult<IViewComponentResult>(View("CompleteQuiz"));
            }

            return null;

        }




    }
}
