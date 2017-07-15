using AutoMapper;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models.DTOs;
using CodoSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Services
{
    public class AdminService
    {
        private IUnitOfWork _context;
        private IMapper _mapper;
        public AdminService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<MenuItemDto> GetMenuItems()
        {
            IEnumerable<Section> sections = _context.Sections.GetAllSections();
            return _mapper.Map<IEnumerable<Section>, IEnumerable<MenuItemDto>>(sections);
        }
    }
}
