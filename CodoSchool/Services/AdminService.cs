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

        public void AddSection(SectionDto sectionDto)
        {
            Section section = _mapper.Map<Section>(sectionDto);
            _context.Sections.Add(section);
            _context.Complete();
        }

        public void EditSection(SectionDto sectionDto)
        {
            Section sectionInDb = _context.Sections.GetSectionIncluded(sectionDto.Id);
            if (sectionInDb != null)
                _mapper.Map(sectionDto, sectionInDb);
            _context.Complete();
        }

        public void DeleteSection(int id)
        {
            Section sectionInDb = _context.Sections.Get(id);
            if(sectionInDb != null)
            {
                _context.Sections.Remove(sectionInDb);
                _context.Complete();
            }
        }

        public SectionViewModel CreateSectionViewModel(int sectionTypeId, int? parentId = null)
        {
            Section parentSection = parentId == null ? null : _context.Sections.GetSectionIncluded((int)parentId);
            SectionViewModel viewModel = new SectionViewModel();
            viewModel.SectionDto.ParentId = parentId;
            var sectionType = _context.SectionTypes.Get(sectionTypeId);
            viewModel.SectionDto.SectionType = _mapper.Map<SectionTypeDto>(sectionType);
            viewModel.SectionDto.SectionTypeId = sectionTypeId;

            if (parentSection != null)
            {
                List<Section> parentSections = _context.Sections.GetParents(parentSection).ToList();
                viewModel.Parents = _mapper.Map<List<Section>, List<SectionDto>>(parentSections);
                viewModel.Parents.Add(_mapper.Map<SectionDto>(parentSection));
            }
            return viewModel;
        }

        public SectionViewModel EditSectionViewModel(int id)
        {
            Section section = _context.Sections.GetSectionIncluded(id);
            if (section == null)
                return null;

            foreach (var child in section.Children)
            {
                child.SectionType = _context.SectionTypes.Get(child.SectionTypeId);
            }

            SectionViewModel viewModel = new SectionViewModel();
            viewModel.SectionDto = _mapper.Map<SectionDto>(section);

            List<SectionType> parentForSectionTypes = GetAvailableSectionTypes(id).ToList();
            viewModel.ParentForSectionTypes = _mapper.Map<List<SectionType>, List<SectionTypeDto>>(parentForSectionTypes);

            List<Section> parentSections = _context.Sections.GetParents(section).ToList();
            viewModel.Parents = _mapper.Map<List<Section>, List<SectionDto>>(parentSections);
            return viewModel;
        }

        public SectionViewModel EditInvalidSectionViewModel(SectionViewModel sectionViewModel)
        {
            SectionViewModel viewModel = sectionViewModel;
            int? id = viewModel.SectionDto.ParentId;
            Section parentSection = id == null ? null : _context.Sections.GetSectionIncluded((int)id);

            if (parentSection != null)
            {
                List<Section> parentSections = _context.Sections.GetParents(parentSection).ToList();
                viewModel.Parents = _mapper.Map<List<Section>, List<SectionDto>>(parentSections);
                viewModel.Parents.Add(_mapper.Map<SectionDto>(parentSection));
            }
            return viewModel;
        }
        
        public IEnumerable<SectionType> GetAvailableSectionTypes(int? parentId)
        {
            List<SectionType> sectionTypes = new List<SectionType>();

            if (parentId == null)
            {
                SectionType sectionType = _context.SectionTypes.Get(SectionType.Category);
                sectionTypes.Add(sectionType);
            }
            else
            {
                Section parentSection = parentId == null ? null : _context.Sections.GetSectionIncluded((int)parentId);
                if (parentSection == null)
                    return null;
                sectionTypes = _context.SectionTypes.Find(x => x.ParentId == parentSection.SectionTypeId).ToList();
                if (parentSection?.SectionTypeId == SectionType.Category)
                    sectionTypes.Add(_context.SectionTypes.Get(SectionType.Category));
            }
            return sectionTypes;
        }
    }
}
