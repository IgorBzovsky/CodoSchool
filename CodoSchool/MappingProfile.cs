using AutoMapper;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Section, MenuItemDto>();
            CreateMap<SectionType, SectionTypeDto>();
            CreateMap<Section, TextLessonDto>();
            CreateMap<Section, VideoLessonDto>();
            CreateMap<Section, CourseDto>();
            CreateMap<Section, SectionDto>().PreserveReferences();
            CreateMap<Question, QuestionDto>().PreserveReferences();
            CreateMap<Question, AdminQuestionDto>().PreserveReferences();
            CreateMap<Answer, AnswerDto>().PreserveReferences();
            
            CreateMap<SectionDto, Section>().ForMember(x => x.SectionType, opt => opt.Ignore()).ForMember(x=>x.Children, opt=>opt.Ignore()).ForMember(x=>x.Parent, opt=>opt.Ignore()).PreserveReferences();
            CreateMap<QuestionDto, Question>().PreserveReferences();
            CreateMap<AdminQuestionDto, Question>().PreserveReferences();
            CreateMap<AnswerDto, Answer>().PreserveReferences();
        }
    }
}
