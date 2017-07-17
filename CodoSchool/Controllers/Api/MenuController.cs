using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Data.Abstractions;
using CodoSchool.Services;
using CodoSchool.Models.DTOs;

namespace CodoSchool.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;
        private readonly AdminService _adminService;
        public MenuController(MenuService menuService, AdminService adminService)
        {
            _menuService = menuService;
            _adminService = adminService;
        }
        // GET: api/Menu
        [HttpGet]
        public IEnumerable<MenuItemDto> Get() => _menuService.GetCategoriesMenu();

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<MenuItemDto> Get(int id) => _menuService.GetCourseMenu(id);
        
        // POST: api/Menu
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Menu/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _adminService.DeleteSection(id);
        }
    }
}
