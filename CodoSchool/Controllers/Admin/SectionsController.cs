using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using CodoSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodoSchool.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class SectionsController : Controller
    {
        AdminService _adminService;

        //Dictionary for mapping SectionType to View
        Dictionary<int, string> _sectionTypeToView;

        public SectionsController(AdminService adminService)
        {
            _adminService = adminService;
            _sectionTypeToView = new Dictionary<int, string>()
            {
                { SectionType.Category, "SectionForm" },
                { SectionType.Course, "SectionForm" },
                { SectionType.Theme, "SectionForm" },
                { SectionType.Lesson, "SectionForm" },
                { SectionType.Unknown, "SectionForm" },
                { SectionType.VideoLesson, "VideoLessonForm" },
                { SectionType.TextLesson, "TextLessonForm" },
                { SectionType.Quiz, "QuizForm" }
            };
        }
        public IActionResult Index()
        {
            IEnumerable<MenuItemDto> menuItems = _adminService.GetMenuItems(); 
            return View(menuItems);
        }

        //Create new section
        public IActionResult New(int sectionTypeId, int? parentId = null)
        {
            SectionViewModel viewModel = _adminService.CreateSectionViewModel(sectionTypeId, parentId);
            return View(_sectionTypeToView[viewModel.SectionDto.SectionTypeId], viewModel);
        }

        //Edit existing section
        public IActionResult Edit(int id)
        {
            SectionViewModel viewModel = _adminService.EditSectionViewModel(id);
            if (viewModel == null)
                return NotFound();
            return View(_sectionTypeToView[viewModel.SectionDto.SectionTypeId], viewModel);
        }

        //Save section
        [HttpPost]
        public IActionResult Save(SectionViewModel viewModel)
        {
            if (viewModel.SectionDto.SectionTypeId != SectionType.TextLesson)
                ModelState.Remove("SectionDto.Content");
            if (viewModel.SectionDto.SectionTypeId != SectionType.VideoLesson)
                ModelState.Remove("SectionDto.VideoUrl");

            //Return same form
            if (!ModelState.IsValid)
            {
                SectionViewModel sectionViewModel = _adminService.EditInvalidSectionViewModel(viewModel);
                if (sectionViewModel == null)
                    return NotFound();
                return View(_sectionTypeToView[viewModel.SectionDto.SectionTypeId], viewModel);
            }

            if (viewModel.SectionDto.SectionTypeId == SectionType.Quiz)
            {
                foreach (var question in viewModel.SectionDto.Questions)
                {
                    if (question.CorrectAnswerId < question.Answers.Count)
                    {
                        question.Answers[(int)question.CorrectAnswerId].IsCorrect = true;
                    }
                }
            }

            //Create new section
            if (viewModel.SectionDto.Id == 0)
            {
                _adminService.AddSection(viewModel.SectionDto);
            }

            //Edit existing section
            else
            {
                _adminService.EditSection(viewModel.SectionDto);
            }

            if (viewModel.SectionDto.ParentId == null)
                return RedirectToAction("Index", "Sections");
            return RedirectToAction("Edit", "Sections", new { id = viewModel.SectionDto.ParentId });
        }
    }
}