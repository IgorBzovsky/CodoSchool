using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Services;
using CodoSchool.Models.DTOs;

namespace CodoSchool.Controllers.Admin
{
    [Route("Admin/Sections")]
    public class SectionsController : Controller
    {
        AdminService _adminService;
        public SectionsController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            IEnumerable<MenuItemDto> menuItems = _adminService.GetMenuItems(); 
            return View();
        }
    }
}