using AutoMapper;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Services
{
    public class MenuService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public MenuService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<MenuItemDto> GetCategoriesMenu()
        {
            IEnumerable<Section> menuItems = _context.Sections.GetCategoriesAndCourses();
            return _mapper.Map<IEnumerable<Section>, IEnumerable<MenuItemDto>>(menuItems);
        }

        public IEnumerable<MenuItemDto> GetCourseMenu(int id)
        {
            IEnumerable<Section> menuItems = _context.Sections.GetCourseLessons(id);
            return _mapper.Map<IEnumerable<Section>, IEnumerable<MenuItemDto>>(menuItems);
        }
    }
}
