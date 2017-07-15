using AutoMapper;
using CodoSchool.Data;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Services
{
    public class LessonsService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public LessonsService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public TextLessonDto ProvideTextLesson(int textLessonId)
        {
            Section textLesson = _context.Sections.Find(x => x.Id == textLessonId && x.SectionTypeId == SectionType.TextLesson).FirstOrDefault();
            return _mapper.Map<Section, TextLessonDto>(textLesson);
        }
        public VideoLessonDto ProvideVideoLesson(int videoLessonId)
        {
            Section videoLesson = _context.Sections.Find(x => x.Id == videoLessonId && x.SectionTypeId == SectionType.VideoLesson).FirstOrDefault();
            return _mapper.Map<Section, VideoLessonDto>(videoLesson);
        }
    }
}
