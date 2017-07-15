using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Services;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;

namespace CodoSchool.Controllers
{
    public class HomeController : Controller
    {
        LessonsService _lessonsService;
        public HomeController(LessonsService lessonsService)
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

        public string Test(int id)
        {
            return "test";
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
    }
}
