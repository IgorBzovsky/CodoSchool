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

            //ViewBag.questionIndex = TempData.Peek("questionIndex");
            //ViewBag.answerid = (TempData.Peek("currentQuiz") as Dictionary<int, int>)[ViewBag.questionIndex];
            //ViewBag.questionsCount = TempData.Peek("questionsCount");

            ViewBag.questionIndex = TempData.Peek("questionIndex");
            ViewBag.questionsCount = TempData.Peek("questionsCount");
            ViewBag.answerid = 0;
            TempData.Keep();


            return Task.FromResult<IViewComponentResult>(View("Default", _quizQuestion));

        }
    }
}
