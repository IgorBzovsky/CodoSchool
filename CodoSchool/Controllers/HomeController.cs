using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Services;
using CodoSchool.Data.Abstractions;
using CodoSchool.Data;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using CodoSchool.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodoSchool.Controllers
{
    public class HomeController : Controller
    {
        LessonsService _lessonsService;
        QuizService _quizService;
        //CookieTempDataProvider _cookieTempDataProvider;


        public HomeController(
               LessonsService lessonsService,
               QuizService quizService)
        //,
        //CookieTempDataProvider cookieTempDataProvider)
        {
            _lessonsService = lessonsService;
            _quizService = quizService;
            //_cookieTempDataProvider = cookieTempDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        public IActionResult Quiz(int id)
        {


            /*
            quizStatus values
            0 - start quiz
            1 - in progress
            2 - complete
            */
            int quizStatus;
            int.TryParse(Request.Query["quizStatus"].FirstOrDefault(), out quizStatus);
            TempData["Status"] = quizStatus;

            switch (quizStatus)
            {
                case 0:
                    {
                        TempData["Dscription"] = _quizService.GetQuiz(id).Description;
                        TempData.Save();
                        return ViewComponent("QuizQuestion", null);
                    }
                case 1:
                    {
                        int answerIndex, questionIndex, questionsCount, previousQuestion;
                        questionsCount = _quizService.GetQuestionsCount(id);


                        int.TryParse(Request.Query["answer"].FirstOrDefault(), out answerIndex);
                        int.TryParse(Request.Query["question"].FirstOrDefault(), out questionIndex);
                        int.TryParse(Request.Query["previousQuestion"].FirstOrDefault(), out previousQuestion);

                        TempData["questionIndex"] = questionIndex >= 0 ?
                        questionIndex <= questionsCount ? questionIndex : questionsCount
                        : 0;


                        TempData["questionsCount"] = questionsCount;

                        QuestionDto currentQuestion = _quizService.ProvideQuestion(id, questionIndex);

                        if (TempData.Peek("answers") == null || ((int[])TempData.Peek("answers")).Length != questionsCount)
                        {
                            List<int> answers = new List<int>();
                            for (int i = 0; i < questionsCount; i++)
                            {
                                answers.Add(0);
                            }
                            TempData["answers"] = answers;
                        }
                        else
                        {
                            var answers = TempData["answers"] as int[];
                            answers[previousQuestion] = answerIndex;
                            TempData["answers"] = answers;
                        }


                        TempData.Save();

                        return ViewComponent("QuizQuestion", currentQuestion);
                    }
                case 2:
                    {
                        var answers = TempData["answers"] as int[];
                        //HttpContext.User.Identity.
                            
                       // _quizService.GetQuizResult(id, )


                        TempData.Save();
                        return ViewComponent("QuizQuestion", null);
                    }
                default:
                    return null;
            }


            
        }

        public IActionResult TextLesson(int id)
        {
            TextLessonDto textLesson = _lessonsService.ProvideTextLesson(id);
            if (textLesson == null)
                return NotFound();
            return PartialView("_TextLessonPartial", textLesson);
        }

        public IActionResult VideoLesson(int id)
        {
            VideoLessonDto videoLesson = _lessonsService.ProvideVideoLesson(id);
            if (videoLesson == null)
                return NotFound();
            return PartialView("_VideoLessonPartial", videoLesson);
        }

        public IActionResult Course(int id)
        {
            CourseDto course = _lessonsService.ProvideCourse(id);
            if (course == null)
                return NotFound();
            return PartialView("_CoursePartial", course);
        }

        public IActionResult Welcome() => PartialView("_Welcome");


    }
}
