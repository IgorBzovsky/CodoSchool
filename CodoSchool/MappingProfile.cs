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
            CreateMap<Question, QuestionDto>();

            //CreateMap<Section, CategoryDto>();
            //CreateMap<Section, LessonDto>();
            //CreateMap<Section, SectionDto>().PreserveReferences();
            //CreateMap<Question, QuestionDto>().PreserveReferences();
            //CreateMap<Answer, AnswerDto>().PreserveReferences();
            //CreateMap<SectionType, SectionTypeDto>().PreserveReferences();

            //CreateMap<QuestionDto, Question>().PreserveReferences();
            //CreateMap<AnswerDto, Answer>().PreserveReferences();
            //CreateMap<CategoryDto, Section>().PreserveReferences();
            //CreateMap<SectionDto, Section>().ForMember(x => x.SectionType, opt => opt.Ignore()).PreserveReferences();
            //CreateMap<SectionTypeDto, SectionType>();
        }
    }
}
