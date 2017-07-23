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
            HttpContext.Session.Clear();
            int answerIndex, questionIndex, questionsCount;
            questionsCount = _quizService.GetQuestionsCount(id);


            int.TryParse(Request.Query["answer"].FirstOrDefault(), out answerIndex);
            int.TryParse(Request.Query["question"].FirstOrDefault(), out questionIndex);
            var t = Request.Query["question"].FirstOrDefault();
            



            //var tmp = TempData.Peek("currentQuiz");

            //if (tmp == null)
            //{
            //    TempData.Add("currentQuiz", new Dictionary<int, int>());
            //    for (int i = 0; i < _quizService.GetQuestionsCount(id); i++)
            //    {
            //        (TempData["currentQuiz"] as Dictionary<int, int>).Add(i, 0);
            //    }

            //}
            //else
            //{
            //    (TempData["currentQuiz"] as Dictionary<int, int>)[questionIndex] = answerIndex;

            //}



            #region MyRegion

            //ViewBag.answerid = Index;
            //var _tempData = _cookieTempDataProvider.LoadTempData(HttpContext);
            //Dictionary<int, int> userAnswers = _tempData["currentQuiz"] as Dictionary<int, int>;
            //if (userAnswers == null)
            //{
            //    userAnswers = new Dictionary<int, int>();
            //    for (int i = 0; i < _quizService.GetQuestionsCount(id); i++)
            //    {
            //        userAnswers.Add(i, 0);
            //    }
            //    _tempData.Add("currentQuiz", userAnswers);
            //}
            //else
            //{
            //    userAnswers[questionIndex] = answerIndex;
            //    _tempData["currentQuiz"] = userAnswers;
            //}


            //ViewBag.answerid = userAnswers[questionIndex];
            //_cookieTempDataProvider.SaveTempData(HttpContext, _tempData);

            #endregion

            //ViewBag.questionIndex

            TempData["questionIndex"] = questionIndex >= 0 ?
            questionIndex <= questionsCount ? questionIndex : questionsCount
            : 0;

            //ViewBag.questionsCount 

            //TempData["questionIndex"] = questionIndex;
            TempData["questionsCount"] = questionsCount;

            QuestionDto currentQuestion = _quizService.ProvideQuestion(id, questionIndex);
            
            //TempData.Save();
            TempData.Keep();

            return ViewComponent("QuizQuestion", currentQuestion);
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

        //public Section GetQuiz(int? id = null)
        //{
        //    if (id == null) return null;

        //    Section quiz = _context.Sections.Include(x => x.SectionType).Include(x => x.Parent).SingleOrDefault(x => x.Id == id);

        //    if (quiz == null || quiz.SectionType.ParentId != SectionType.Theme)
        //        NotFound();


        //    var questions =
        //        _context.Question.
        //        Where(x => x.SectionId == id).
        //        Include(x => x.Answers);

        //    foreach (var question in questions)
        //    {
        //        quizDto.Questions.Add(question);
        //    }
        //    return quizDto;
        //}
    }
}
