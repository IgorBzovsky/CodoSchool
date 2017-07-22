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

namespace CodoSchool.Controllers
{
    public class HomeController : Controller
    {
        LessonsService _lessonsService;

        public HomeController(
               LessonsService lessonsService)
        {
            _lessonsService = lessonsService;
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

        public PartialViewResult Quiz(int id)
        {
            Random rn = new Random();
            var t = new Question
            {
                Id = rn.Next()
            };

            int answerID, questionIndex, questionsCount;
            int.TryParse(Request.Query["answer"], out answerID);
            int.TryParse(Request.Query["question"], out questionIndex);
            ViewBag.answerid =  answerID;
            ViewBag.questionIndex = questionIndex >= 0 ? questionIndex : 0;
            return PartialView("_QuizPartial", t);
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
